﻿using AmongUs.GameOptions;
using System.Collections.Generic;
using static TOHE.Options;

namespace TOHE.Roles.Neutral;

internal class Jester : RoleBase
{
    //===========================SETUP================================\\
    private const int Id = 14400;
    private static HashSet<byte> PlayerIds = [];
    public static bool HasEnabled => PlayerIds.Count > 0;
    public override bool IsEnable => HasEnabled;
    public override CustomRoles ThisRoleBase => JesterCanVent.GetBool() ? CustomRoles.Engineer : CustomRoles.Crewmate;
    //==================================================================\\

    private static OptionItem JesterCanUseButton;
    private static OptionItem JesterHasImpostorVision;
    public static OptionItem JesterCanVent;
    private static OptionItem MeetingsNeededForJesterWin;
    private static OptionItem HideJesterVote;
    private static OptionItem SunnyboyChance;

    public static void SetupCustomOptions()
    {
        SetupRoleOptions(Id, TabGroup.NeutralRoles, CustomRoles.Jester);
        JesterCanUseButton = BooleanOptionItem.Create(Id + 2, "JesterCanUseButton", false, TabGroup.NeutralRoles, false)
            .SetParent(CustomRoleSpawnChances[CustomRoles.Jester]);
        JesterCanVent = BooleanOptionItem.Create(Id + 3, "CanVent", true, TabGroup.NeutralRoles, false)
            .SetParent(CustomRoleSpawnChances[CustomRoles.Jester]);
        JesterHasImpostorVision = BooleanOptionItem.Create(Id + 4, "ImpostorVision", true, TabGroup.NeutralRoles, false)
            .SetParent(CustomRoleSpawnChances[CustomRoles.Jester]);
        HideJesterVote = BooleanOptionItem.Create(Id + 5, "HideJesterVote", true, TabGroup.NeutralRoles, false)
            .SetParent(CustomRoleSpawnChances[CustomRoles.Jester]);
        MeetingsNeededForJesterWin = IntegerOptionItem.Create(Id + 6, "MeetingsNeededForWin", new(0, 10, 1), 0, TabGroup.NeutralRoles, false)
            .SetParent(CustomRoleSpawnChances[CustomRoles.Jester])
            .SetValueFormat(OptionFormat.Times);
        SunnyboyChance = IntegerOptionItem.Create(Id + 7, "SunnyboyChance", new(0, 100, 5), 0, TabGroup.NeutralRoles, false)
            .SetParent(CustomRoleSpawnChances[CustomRoles.Jester])
            .SetValueFormat(OptionFormat.Percent);
    }
    public override void Init()
    {
        PlayerIds = [];
    }
    public override void Add(byte playerId)
    {
        PlayerIds.Add(playerId);
    }
    public override void ApplyGameOptions(IGameOptions opt, byte playerId)
    {
        //Jester
        AURoleOptions.EngineerCooldown = 0f;
        AURoleOptions.EngineerInVentMaxTime = 0f;

        //SunnyBoy
        AURoleOptions.ScientistCooldown = 0f;
        AURoleOptions.ScientistBatteryCharge = 60f;
        
        if (Utils.GetPlayerById(playerId).Is(CustomRoles.Jester))
            opt.SetVision(JesterHasImpostorVision.GetBool());
    }
    public static bool CheckSpawnSunnyboy()
    {
        var Rand = IRandom.Instance;
        return Rand.Next(0, 100) < SunnyboyChance.GetInt();
    }
    public override bool HideVote(PlayerVoteArea votedPlayer) => HideJesterVote.GetBool();
    public override bool OnCheckStartMeeting(PlayerControl reporter) => JesterCanUseButton.GetBool();

    public override void CheckExileTarget(GameData.PlayerInfo exiled, ref bool DecidedWinner, bool isMeetingHud, ref string name)
    {
        if (MeetingsNeededForJesterWin.GetInt() <= Main.MeetingsPassed)
        {
            if (isMeetingHud)
            {
                name = string.Format(Translator.GetString("ExiledJester"), Main.LastVotedPlayer, Utils.GetDisplayRoleAndSubName(exiled.PlayerId, exiled.PlayerId, true));
                DecidedWinner = true;
            }
            else
            {
                if (!CustomWinnerHolder.CheckForConvertedWinner(exiled.PlayerId))
                {
                    CustomWinnerHolder.ResetAndSetWinner(CustomWinner.Jester);
                    CustomWinnerHolder.WinnerIds.Add(exiled.PlayerId);
                }

                // Check exile target Executioner
                foreach (var executioner in Executioner.playerIdList)
                {
                    if (Executioner.IsTarget(executioner, exiled.PlayerId))
                    {
                        CustomWinnerHolder.AdditionalWinnerTeams.Add(AdditionalWinners.Executioner);
                        CustomWinnerHolder.WinnerIds.Add(executioner);
                    }
                }
                DecidedWinner = true;
            }
        }
        else if (CEMode.GetInt() == 2 && isMeetingHud)
            name += string.Format(Translator.GetString("JesterMeetingLoose"), MeetingsNeededForJesterWin.GetInt() + 1);
    }
}
