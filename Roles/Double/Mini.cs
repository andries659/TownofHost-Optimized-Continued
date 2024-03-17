using Hazel;
using System;
using System.Collections.Generic;
using TOHE.Roles.Core;
using static TOHE.Translator;
using static TOHE.Utils;

namespace TOHE.Roles.Double;
internal class Mini : RoleBase
{
    //===========================SETUP================================\\
    private const int Id = 7000;
    private static bool On = false;
    public override bool IsEnable => On;
    public static bool HasEnabled => CustomRoles.NiceMini.IsClassEnable() || CustomRoles.EvilMini.IsClassEnable();
    public override CustomRoles ThisRoleBase => IsEvilMini ? CustomRoles.Impostor : CustomRoles.Crewmate;
    //==================================================================\\

    private static OptionItem GrowUpDuration;
    private static OptionItem EveryoneCanKnowMini;
    private static OptionItem CountMeetingTime;
    private static OptionItem EvilMiniSpawnChances;
    private static OptionItem CanBeEvil;
    private static OptionItem UpDateAge;
    private static OptionItem MinorCD;
    private static OptionItem MajorCD;

    private static List<byte> playerIdList = [];

    public static int Age = new();
    private static bool IsEvilMini = false;
    private static int GrowUpTime = new();
    private static int GrowUp = new();
    private static long LastFixedUpdate = new();
    private static bool misguessed = false;

    public static void SetupCustomOption()
    {
        Options.SetupSingleRoleOptions(Id, TabGroup.CrewmateRoles, CustomRoles.Mini, 1, zeroOne: false);
        GrowUpDuration = IntegerOptionItem.Create(Id + 100, "GrowUpDuration", new(200, 800, 25), 400, TabGroup.CrewmateRoles, false).SetParent(Options.CustomRoleSpawnChances[CustomRoles.Mini])
            .SetValueFormat(OptionFormat.Seconds);
        EveryoneCanKnowMini = BooleanOptionItem.Create(Id + 102, "EveryoneCanKnowMini", true, TabGroup.CrewmateRoles, false).SetParent(Options.CustomRoleSpawnChances[CustomRoles.Mini]);
        CanBeEvil = BooleanOptionItem.Create(Id + 106, "CanBeEvil", true, TabGroup.CrewmateRoles, false).SetParent(Options.CustomRoleSpawnChances[CustomRoles.Mini]);
        EvilMiniSpawnChances = IntegerOptionItem.Create(Id + 108, "EvilMiniSpawnChances", new(0, 100, 5), 50, TabGroup.CrewmateRoles, false).SetParent(CanBeEvil)
            .SetValueFormat(OptionFormat.Percent);
        MinorCD = FloatOptionItem.Create(Id + 110, "KillCooldown", new(0f, 180f, 2.5f), 45f, TabGroup.CrewmateRoles, false).SetParent(CanBeEvil)
            .SetValueFormat(OptionFormat.Seconds);
        MajorCD = FloatOptionItem.Create(Id + 112, "MajorCooldown", new(0f, 180f, 2.5f), 15f, TabGroup.CrewmateRoles, false).SetParent(CanBeEvil)
            .SetValueFormat(OptionFormat.Seconds);
        UpDateAge = BooleanOptionItem.Create(Id + 114, "UpDateAge", true, TabGroup.CrewmateRoles, false).SetParent(Options.CustomRoleSpawnChances[CustomRoles.Mini]);
        CountMeetingTime = BooleanOptionItem.Create(Id + 116, "CountMeetingTime", true, TabGroup.CrewmateRoles, false).SetParent(Options.CustomRoleSpawnChances[CustomRoles.Mini]);
    }
    public override void Init()
    {
        GrowUpTime = 0;
        playerIdList = [];
        GrowUp = GrowUpDuration.GetInt() / 18;
        On = false;
        Age = 0;
        misguessed = false;

        var rand = new Random();
        IsEvilMini = CanBeEvil.GetBool() && (rand.Next(0, 100) < EvilMiniSpawnChances.GetInt());
    }
    public override void Add(byte playerId)
    {
        playerIdList.Add(playerId);
        On = true;

        if (!AmongUsClient.Instance.AmHost) return;
        if (!Main.ResetCamPlayerList.Contains(playerId))
            Main.ResetCamPlayerList.Add(playerId);
    }
    public static void SendRPC()
    {
        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SyncMiniCrewAge, SendOption.Reliable, -1);
        writer.Write(Age);
        AmongUsClient.Instance.FinishRpcImmediately(writer);
    }
    public static void ReceiveRPC(MessageReader reader)
    {
        Age = reader.ReadInt32();
    }

    public static bool CheckSpawnEvilMini() => IsEvilMini;

    public override bool OnCheckMurderAsTarget(PlayerControl killer, PlayerControl target)
    {
        if (Age < 18)
        {
            killer.Notify(ColorString(GetRoleColor(CustomRoles.Mini), GetString("Cantkillkid")));
            return false;
        }
        return true;
    }
    public static void OnFixedUpdates(PlayerControl player)
    {
        if (!GameStates.IsInGame) return;
        if (Age >= 18) return;

        //Check if nice mini is dead
        if (!player.IsAlive() && player.Is(CustomRoles.NiceMini))
        {
            if (!CustomWinnerHolder.CheckForConvertedWinner(player.PlayerId))
            {
                CustomWinnerHolder.ResetAndSetWinner(CustomWinner.NiceMini);
                CustomWinnerHolder.WinnerIds.Add(player.PlayerId);
            }
            // ↑ This code will show the mini winning player on the checkout screen, Tommy you shouldn't comment it out!
        } //Is there any need to check this 30 times a second?

        if (GameStates.IsMeeting && !CountMeetingTime.GetBool()) return;

        if (LastFixedUpdate == GetTimeStamp()) return;
        LastFixedUpdate = GetTimeStamp();
        GrowUpTime++;

        if (GrowUpTime >= GrowUpDuration.GetInt() / 18)
        {
            Age += 1;
            GrowUpTime = 0;
            Logger.Info($"Mini grow up by 1", "Mini");
            if (player.Is(CustomRoles.EvilMini))
            {
                player.ResetKillCooldown();
                player.SyncSettings();
                //Only sync settings so evil mini's age updates upon next kill attempt or after meeting
            }

            if (player.Is(CustomRoles.NiceMini))
                player.RpcGuardAndKill();
            
            /*Dont show guard animation for evil mini,
            this would simply stop them from murdering.
            Imagine reseting kill cool down every 20 seconds
            and evil mini can never kill before age 18*/

            if (UpDateAge.GetBool())
            {
                SendRPC();
                player.Notify(GetString("MiniUp"));
                NotifyRoles();
            }
        }
    }

    public override void SetKillCooldown(byte id) => Main.AllPlayerKillCooldown[id] = GetKillCoolDown();
    private static float GetKillCoolDown()
    {
        if (MinorCD.GetFloat() <= MajorCD.GetFloat())
            return MinorCD.GetFloat();

        if (Age == 0) return MinorCD.GetFloat();
        if (Age == 18) return MajorCD.GetFloat();

        return MinorCD.GetFloat() + (MajorCD.GetFloat() - MinorCD.GetFloat()) / 18 * Age;
    }
    public override string GetProgressText(byte playerId, bool comms) => ColorString(GetRoleColor(CustomRoles.Mini), Age != 18 ? $"({Age})" : "");
    public override bool GuessCheck(bool isUI, PlayerControl guesser, PlayerControl target, CustomRoles role, ref bool guesserSuicide)
    {
        if (guesser.Is(CustomRoles.NiceMini) && Age < 18 && misguessed)
        {
            if (!isUI) SendMessage(GetString("MiniGuessMax"), guesser.PlayerId);
            else guesser.ShowPopUp(GetString("MiniGuessMax"));
            return true;
        }
        return false;
    }
    public override bool OnRoleGuess(bool isUI, PlayerControl target, PlayerControl guesser, CustomRoles role, ref bool guesserSuicide)
    {
        if (target.Is(CustomRoles.NiceMini) && Age < 18)
        {
            if (!isUI) SendMessage(GetString("GuessMini"), guesser.PlayerId);
            else guesser.ShowPopUp(GetString("GuessMini"));
            return true;
        }
        return false;
    }
    public override bool CheckMisGuessed(bool isUI, PlayerControl guesser, PlayerControl target, CustomRoles role, ref bool guesserSuicide)
    {
        if (Age < 18 && guesser.PlayerId == target.PlayerId)
        {
            misguessed = true;
            _ = new LateTask(() => { SendMessage(GetString("MiniMisGuessed"), target.PlayerId, ColorString(GetRoleColor(CustomRoles.NiceMini), GetString("GuessKillTitle")), true); }, 0.6f, "Mini MisGuess Msg");
            return true;
        }

        return false;
    }

    public override void CheckExileTarget(GameData.PlayerInfo exiled, ref bool DecidedWinner, bool isMeetingHud, ref string name)
    {
        if (GetPlayerById(exiled.PlayerId).Is(CustomRoles.NiceMini) && Age < 18)
        {
            if (isMeetingHud)
            {
                name = string.Format(GetString("ExiledNiceMini"), Main.LastVotedPlayer, GetDisplayRoleAndSubName(exiled.PlayerId, exiled.PlayerId, true));
                DecidedWinner = true;
            }
            else
            {
                if (!CustomWinnerHolder.CheckForConvertedWinner(exiled.PlayerId))
                {
                    CustomWinnerHolder.ResetAndSetWinner(CustomWinner.NiceMini);
                    CustomWinnerHolder.WinnerIds.Add(exiled.PlayerId);
                }
            }
        }
    }

    public override bool OthersKnowTargetRoleColor(PlayerControl seer, PlayerControl target)
        => (target.Is(CustomRoles.NiceMini) || target.Is(CustomRoles.EvilMini)) && EveryoneCanKnowMini.GetBool();

    public override string PlayerKnowTargetColor(PlayerControl seer, PlayerControl target)
            => !seer.GetCustomRole().IsImpostorTeam() && (target.Is(CustomRoles.NiceMini) || target.Is(CustomRoles.EvilMini)) ? Main.roleColors[CustomRoles.Mini] : string.Empty;
    
    public override string GetMark(PlayerControl seer, PlayerControl target = null, bool isForMeeting = false)
            => HasEnabled && EveryoneCanKnowMini.GetBool() && (target.Is(CustomRoles.NiceMini) || target.Is(CustomRoles.EvilMini)) ? ColorString(GetRoleColor(CustomRoles.Mini), Age != 18 && UpDateAge.GetBool() ? $"({Age})" : string.Empty) : string.Empty;
}