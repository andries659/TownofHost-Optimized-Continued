using AmongUs.GameOptions;
using TOHE.Roles.AddOns.Common;
using TOHE.Roles.AddOns.Crewmate;
using TOHE.Roles.Crewmate;
using TOHE.Roles.Impostor;
using TOHE.Roles.Neutral;
using TOHE.Roles.AddOns.Impostor;

namespace TOHE;

public static class CustomRolesHelper
{
    public static readonly CustomRoles[] AllRoles = EnumHelper.GetAllValues<CustomRoles>();
    public static readonly CustomRoleTypes[] AllRoleTypes = EnumHelper.GetAllValues<CustomRoleTypes>();
    public static CustomRoles GetVNRole(this CustomRoles role) // RoleBase: Impostor, Shapeshifter, Crewmate, Engineer, Scientist
    {
        // Vanilla roles
        if (role.IsVanilla()) return role;

        // Role base
        if (role.IsRoleClass() is not VanillaRole) return role.IsRoleClass().ThisRoleBase;

        //Default
        return role switch
        {
            CustomRoles.ShapeshifterTOHE => CustomRoles.Shapeshifter,
            CustomRoles.ScientistTOHE => CustomRoles.Scientist,
            CustomRoles.EngineerTOHE => CustomRoles.Engineer,
            _ => role.IsImpostor() ? CustomRoles.Impostor : CustomRoles.Crewmate,
        };
    }

    public static RoleTypes GetDYRole(this CustomRoles role) // Role has a kill button (Non-Impostor)
        => (role.IsRoleClass().ThisRoleBase is CustomRoles.Impostor) && !role.IsImpostor()
            ? RoleTypes.Impostor
            : RoleTypes.GuardianAngel;


    public static bool HasImpKillButton(this PlayerControl player, bool considerVanillaShift = false)
    {
        if (player == null) return false;
        var customRole = player.GetCustomRole();
        bool ModSideHasKillButton = customRole.GetDYRole() == RoleTypes.Impostor || customRole.GetVNRole() == CustomRoles.Impostor || customRole.GetVNRole() == CustomRoles.Shapeshifter;

        if (player.IsModClient() || (!considerVanillaShift && !player.IsModClient()))
            return ModSideHasKillButton;

        bool vanillaSideHasKillButton = EAC.OriginalRoles.TryGetValue(player.PlayerId, out var OriginalRole) ?
                                         (OriginalRole.GetDYRole() == RoleTypes.Impostor || OriginalRole.GetVNRole() == CustomRoles.Impostor || OriginalRole.GetVNRole() == CustomRoles.Shapeshifter) : ModSideHasKillButton;

        return vanillaSideHasKillButton;
    }
    //This is a overall check for vanilla clients to see if they are imp basis 
    public static bool IsGhostRole(this CustomRoles role)
    {
        return role is
            CustomRoles.GuardianAngelTOHE or
            CustomRoles.EvilSpirit or
            CustomRoles.Warden or
            CustomRoles.Hawk or
            CustomRoles.Bloodmoon or
            CustomRoles.Minion;

    }
    public static bool IsAdditionRole(this CustomRoles role)
    {
        return role is
            CustomRoles.Lovers or
            CustomRoles.LastImpostor or
            CustomRoles.Ntr or
            CustomRoles.Cyber or
            CustomRoles.Madmate or
            CustomRoles.Watcher or
            CustomRoles.Admired or
            CustomRoles.Flash or
            CustomRoles.Torch or
            CustomRoles.Seer or
            CustomRoles.Bait or
            CustomRoles.Burst or
            CustomRoles.Diseased or
            CustomRoles.Antidote or
            CustomRoles.Fragile or
            CustomRoles.VoidBallot or
            CustomRoles.Aware or
            CustomRoles.Swift or
            CustomRoles.Cleansed or
            CustomRoles.Gravestone or
            CustomRoles.Trapper or
            CustomRoles.Mare or
            CustomRoles.Tiebreaker or
            CustomRoles.Oblivious or
            CustomRoles.Bewilder or
            CustomRoles.Knighted or
            CustomRoles.Workhorse or
            CustomRoles.Fool or
            CustomRoles.Autopsy or
            CustomRoles.Necroview or
            CustomRoles.Avanger or
            CustomRoles.Sleuth or
            CustomRoles.Clumsy or
            CustomRoles.Nimble or
            CustomRoles.Circumvent or
            CustomRoles.Youtuber or
            CustomRoles.Soulless or
            CustomRoles.Loyal or
            CustomRoles.Egoist or
            CustomRoles.Recruit or
            CustomRoles.TicketsStealer or
            CustomRoles.Tricky or
            CustomRoles.Schizophrenic or
            CustomRoles.Mimic or
            CustomRoles.Reach or
            CustomRoles.Charmed or
            CustomRoles.Infected or
            CustomRoles.Onbound or
            CustomRoles.Rebound or
            CustomRoles.Mundane or
            CustomRoles.Lazy or
            CustomRoles.Rascal or
            CustomRoles.Contagious or
            CustomRoles.Guesser or
            CustomRoles.Unreportable or
            CustomRoles.Lucky or
            CustomRoles.Unlucky or
            CustomRoles.DoubleShot or
            CustomRoles.Ghoul or
            CustomRoles.Bloodlust or
            CustomRoles.Overclocked or
            CustomRoles.Stubborn or
            CustomRoles.EvilSpirit or
            CustomRoles.Hurried or
            CustomRoles.Oiiai or
            CustomRoles.Influenced or
            CustomRoles.Silent or
            CustomRoles.Rainbow or
            CustomRoles.Susceptible or
            CustomRoles.Statue or
            CustomRoles.Tired;
    }
    public static bool IsAmneMaverick(this CustomRoles role) // ROLE ASSIGNING, NOT NEUTRAL TYPE
    {
        return role is
            CustomRoles.Jester or
            CustomRoles.Terrorist or
            CustomRoles.Opportunist or
            CustomRoles.Masochist or
            CustomRoles.Huntsman or
            CustomRoles.Executioner or
            CustomRoles.Vector or
            CustomRoles.Shaman or
            CustomRoles.Crewpostor or
            CustomRoles.Lawyer or
            CustomRoles.God or
            CustomRoles.Amnesiac or
            CustomRoles.Glitch or
            CustomRoles.Imitator or
            CustomRoles.Bandit or
            CustomRoles.Pestilence or
            CustomRoles.PlagueBearer or
            CustomRoles.Agitater or
            CustomRoles.Innocent or
            CustomRoles.Vulture or
            CustomRoles.Taskinator or
            CustomRoles.Pursuer or
            CustomRoles.Revolutionist or
            CustomRoles.Provocateur or
            CustomRoles.Demon or
            CustomRoles.Hater or
            CustomRoles.Workaholic or
            CustomRoles.Solsticer or
            CustomRoles.Collector or
            CustomRoles.Sunnyboy or
            CustomRoles.Arsonist or
            CustomRoles.Maverick or
            CustomRoles.CursedSoul or
            CustomRoles.Phantom or
            CustomRoles.Stalker or
            CustomRoles.Doomsayer or
            CustomRoles.SoulCollector or
            CustomRoles.Pirate or
            CustomRoles.Seeker or
            CustomRoles.Pixie or
            CustomRoles.Romantic or
            CustomRoles.RuthlessRomantic or
            CustomRoles.VengefulRomantic or
            CustomRoles.Doppelganger or
            CustomRoles.SchrodingersCat or
            CustomRoles.Follower;
    }
    public static bool IsAmneCrew(this PlayerControl target)
    {
        return target.IsCrewVenter()
                || target.GetCustomRole() is
                CustomRoles.Sheriff or
                CustomRoles.LazyGuy or
                CustomRoles.SuperStar or
                CustomRoles.Celebrity or
                CustomRoles.Mayor or
                CustomRoles.Paranoia or
                CustomRoles.Paranoia or
                CustomRoles.Dictator or
                CustomRoles.NiceGuesser or
                CustomRoles.Bodyguard or
                CustomRoles.Observer or
                CustomRoles.Retributionist or
                CustomRoles.Lookout or
                CustomRoles.Admirer or
                CustomRoles.Cleanser or
                CustomRoles.CopyCat or
                CustomRoles.Deceiver or
                CustomRoles.Crusader or
                CustomRoles.Overseer or
                CustomRoles.Jailer or
                CustomRoles.Judge or
                CustomRoles.Medic or
                CustomRoles.Medium or
                CustomRoles.Monarch or
                CustomRoles.Telecommunication or
                CustomRoles.Swapper or
                CustomRoles.Mechanic;
    }
    public static bool IsAmneNK(this CustomRoles role)
    {
        return role is
            CustomRoles.Sidekick or
            CustomRoles.Infectious or
            CustomRoles.Pyromaniac or
            CustomRoles.Medusa or
            CustomRoles.Necromancer or
            CustomRoles.Wraith or
            CustomRoles.Shroud or
            CustomRoles.Pelican or
            CustomRoles.Refugee or
            CustomRoles.Parasite or
            CustomRoles.PlagueDoctor or
            CustomRoles.SerialKiller or
            CustomRoles.Werewolf or
            CustomRoles.Pickpocket or
            CustomRoles.Traitor or
            CustomRoles.Virus or
            CustomRoles.Spiritcaller or
            CustomRoles.Jackal or
            CustomRoles.Juggernaut or
            CustomRoles.BloodKnight or
            CustomRoles.Cultist;
    }
    public static bool IsCrewVenter(this PlayerControl target)
    {
        return target.Is(CustomRoles.EngineerTOHE)
            || target.Is(CustomRoles.Mechanic)
            || target.Is(CustomRoles.CopyCat)
            || target.Is(CustomRoles.Telecommunication) && Telecommunication.CanUseVent()
            || Knight.CheckCanUseVent(target)
            || target.Is(CustomRoles.Nimble);
    }
    public static bool IsTasklessCrewmate(this CustomRoles role)
    {
        // Based on Imp but counted as crewmate
        return role.GetVNRole() is CustomRoles.Impostor && role.IsCrewmate();
    }
    public static bool IsTaskBasedCrewmate(this CustomRoles role)
    {
        return role is
            CustomRoles.Snitch or
            CustomRoles.FortuneTeller or
            CustomRoles.Marshall or
            CustomRoles.TimeManager or
            CustomRoles.Guardian or
            CustomRoles.Merchant or
            CustomRoles.Mayor or
            CustomRoles.Captain or
            CustomRoles.Transporter or
            CustomRoles.Retributionist or
            CustomRoles.Benefactor or
            CustomRoles.Alchemist;
    }
    public static bool IsCK(this CustomRoles role)
    {
        return role is
            CustomRoles.Knight or
            CustomRoles.Veteran or
            CustomRoles.Judge or
            CustomRoles.Bodyguard or
            CustomRoles.Bastion or
            CustomRoles.Reverie or
            CustomRoles.Crusader or
            CustomRoles.NiceGuesser or
            CustomRoles.Deceiver or
            CustomRoles.Retributionist or
            CustomRoles.Sheriff or
            CustomRoles.Vigilante or
            CustomRoles.Jailer;
    }
    public static bool IsNeutral(this CustomRoles role)
    {
        if (role is
            //FFA
            CustomRoles.Killer) return true;

        return role.IsNK() || role.IsNonNK();
    }
    public static bool IsNK(this CustomRoles role)
    {
        if (role == CustomRoles.Arsonist && Arsonist.CanIgniteAnytime()) return true;
        else if (role == CustomRoles.Quizmaster && Quizmaster.CanKillAfterMark.GetBool()) return true;

        return role is
            CustomRoles.Jackal or
            CustomRoles.Doppelganger or
            CustomRoles.Bandit or
            CustomRoles.Glitch or
            CustomRoles.Sidekick or
            CustomRoles.Huntsman or
            CustomRoles.Infectious or
            CustomRoles.Medusa or
            CustomRoles.Pelican or
            CustomRoles.Stalker or
            CustomRoles.Juggernaut or
            CustomRoles.Jinx or
            CustomRoles.Poisoner or
            CustomRoles.Wraith or
            CustomRoles.HexMaster or
            CustomRoles.Refugee or
            CustomRoles.Parasite or
            CustomRoles.PlagueDoctor or
            CustomRoles.SerialKiller or
            CustomRoles.Pyromaniac or
            CustomRoles.Werewolf or
            CustomRoles.PotionMaster or
            CustomRoles.Demon or
            CustomRoles.Pickpocket or
            CustomRoles.Necromancer or
            CustomRoles.Traitor or
            CustomRoles.Shroud or
            CustomRoles.Virus or
            CustomRoles.BloodKnight or
            CustomRoles.Spiritcaller or
            CustomRoles.PlagueBearer or
            CustomRoles.Agitater or
            CustomRoles.RuthlessRomantic or
            CustomRoles.Pestilence;
    }
    public static bool IsNonNK(this CustomRoles role) // ROLE ASSIGNING, NOT NEUTRAL TYPE
    {
        if (role == CustomRoles.Arsonist && !Arsonist.CanIgniteAnytime()) return true;
        else if (role == CustomRoles.Quizmaster && !Quizmaster.CanKillAfterMark.GetBool()) return true; 

        return role is
            CustomRoles.Amnesiac or
            CustomRoles.Follower or
            CustomRoles.Hater or
            CustomRoles.Lawyer or
            CustomRoles.Imitator or
            CustomRoles.Maverick or
            CustomRoles.Opportunist or
            CustomRoles.Pursuer or
            CustomRoles.Shaman or
            CustomRoles.SoulCollector or
            CustomRoles.CursedSoul or
            CustomRoles.Doomsayer or
            CustomRoles.Executioner or
            CustomRoles.Innocent or
            CustomRoles.Jester or
            CustomRoles.Sunnyboy or
            CustomRoles.Masochist or
            CustomRoles.Seeker or
            CustomRoles.Pixie or
            CustomRoles.Collector or
            CustomRoles.Cultist or
            CustomRoles.Phantom or
            CustomRoles.Pirate or
            CustomRoles.Terrorist or
            CustomRoles.Vulture or
            CustomRoles.Taskinator or
            CustomRoles.Workaholic or
            CustomRoles.Solsticer or
            CustomRoles.God or
            CustomRoles.Vector or
            CustomRoles.Revolutionist or
            CustomRoles.Romantic or
            CustomRoles.VengefulRomantic or
            CustomRoles.SchrodingersCat or
            CustomRoles.Provocateur;
    }
    public static bool IsNB(this CustomRoles role)
    {
        return role is
            CustomRoles.Amnesiac or
            CustomRoles.Follower or
            CustomRoles.Hater or
            CustomRoles.Imitator or
            CustomRoles.Lawyer or
            CustomRoles.Maverick or
            CustomRoles.Opportunist or
            CustomRoles.Pursuer or
            CustomRoles.Shaman or
            CustomRoles.Taskinator or
            CustomRoles.God or
            CustomRoles.Romantic or
            CustomRoles.VengefulRomantic or
            CustomRoles.Pixie or
            CustomRoles.SchrodingersCat or
            CustomRoles.Sunnyboy;
    }
    public static bool IsNE(this CustomRoles role)
    {
        return role is
            CustomRoles.CursedSoul or
            CustomRoles.Doomsayer or
            CustomRoles.Executioner or
            CustomRoles.Innocent or
            CustomRoles.Jester or
            CustomRoles.Masochist or
            CustomRoles.Seeker;
    }
    public static bool IsNC(this CustomRoles role)
    {
        return role is
            CustomRoles.Collector or
            CustomRoles.Cultist or
            CustomRoles.Phantom or
            CustomRoles.Vector or
            CustomRoles.SoulCollector or
            CustomRoles.Pirate or
            CustomRoles.Terrorist or
            CustomRoles.Vulture or
            CustomRoles.Workaholic or
            CustomRoles.Solsticer or
            CustomRoles.Revolutionist or
            CustomRoles.Provocateur;
    }
    public static bool IsImpostor(this CustomRoles role) // IsImp
    {
        return role is
            CustomRoles.Impostor or
            CustomRoles.Shapeshifter or
            CustomRoles.ShapeshifterTOHE or
            CustomRoles.ImpostorTOHE or
            CustomRoles.Consigliere or
            CustomRoles.Wildling or
            CustomRoles.Morphling or
            CustomRoles.BountyHunter or
            CustomRoles.Vampire or
            CustomRoles.Vampiress or
            CustomRoles.Witch or
            CustomRoles.Vindicator or
            CustomRoles.ShapeMaster or
            CustomRoles.Zombie or
            CustomRoles.Warlock or
            CustomRoles.Undertaker or
            CustomRoles.RiftMaker or
            CustomRoles.Ninja or
            CustomRoles.Berserker or
            CustomRoles.Bloodmoon or
            CustomRoles.Anonymous or
            CustomRoles.Visionary or
            CustomRoles.Miner or
            CustomRoles.Escapist or
            CustomRoles.Mercenary or
            CustomRoles.Underdog or
            CustomRoles.Inhibitor or
            CustomRoles.Councillor or
            CustomRoles.Saboteur or
            CustomRoles.Puppeteer or
            CustomRoles.TimeThief or
            CustomRoles.Trickster or
            CustomRoles.Nemesis or
            CustomRoles.Mastermind or
            CustomRoles.Chronomancer or
            CustomRoles.Stealth or
            CustomRoles.Penguin or
            CustomRoles.KillingMachine or
            CustomRoles.Fireworker or
            CustomRoles.Sniper or
            CustomRoles.EvilTracker or
            CustomRoles.EvilGuesser or
            CustomRoles.AntiAdminer or
            CustomRoles.Arrogance or
            CustomRoles.Bomber or
            CustomRoles.Nuker or
            CustomRoles.Kamikaze or
            CustomRoles.Scavenger or
            CustomRoles.Trapster or
            CustomRoles.Gangster or
            CustomRoles.Cleaner or
            CustomRoles.Lightning or
            CustomRoles.Greedy or
            CustomRoles.Ludopath or
            CustomRoles.Godfather or
            CustomRoles.CursedWolf or
            CustomRoles.SoulCatcher or
            CustomRoles.QuickShooter or
            CustomRoles.Eraser or
            CustomRoles.Butcher or
            CustomRoles.Hangman or
            CustomRoles.Bard or
            CustomRoles.Swooper or
            CustomRoles.Disperser or
            CustomRoles.Dazzler or
            CustomRoles.Deathpact or
            CustomRoles.Devourer or
            CustomRoles.Camouflager or
            CustomRoles.Twister or
            CustomRoles.Lurker or
            CustomRoles.EvilMini or
            CustomRoles.Blackmailer or
            CustomRoles.Pitfall or
            CustomRoles.Instigator or
            CustomRoles.Minion;
    }

    public static bool IsAbleToBeSidekicked(this CustomRoles role) 
        => role.GetDYRole() == RoleTypes.Impostor && !role.IsImpostor() && !role.IsRecruitingRole();

    public static bool IsRecruitingRole(this CustomRoles role) 
        => role is
            CustomRoles.Jackal or
            CustomRoles.Cultist or
            CustomRoles.Necromancer or
            CustomRoles.Virus or
            CustomRoles.Spiritcaller;

    public static bool IsMadmate(this CustomRoles role)
    {
        return role is
            CustomRoles.Crewpostor or
            CustomRoles.Refugee or
            CustomRoles.Parasite;
    }
    /// <summary>
    /// Role Changes the Crewmates Team, Including changing to Impostor.
    /// </summary>
    public static bool IsConverted(this CustomRoles role)
    {
        return (role is CustomRoles.Charmed
                or CustomRoles.Recruit
                or CustomRoles.Infected
                or CustomRoles.Contagious
                or CustomRoles.Soulless
                or CustomRoles.Lovers
                or CustomRoles.Madmate ||
                (role is CustomRoles.Egoist && Egoist.EgoistCountAsConverted.GetBool()));
    }
    public static bool IsNotKnightable(this CustomRoles role)
    {
        return role is
            CustomRoles.Mayor or
            CustomRoles.Vindicator or
            CustomRoles.Dictator or
            CustomRoles.Knighted or
            CustomRoles.Glitch or
            CustomRoles.Pickpocket or
            CustomRoles.Stubborn or
            CustomRoles.TicketsStealer;
    }
    public static bool IsSpeedRole(this CustomRoles role)
    {
        return role is
            CustomRoles.Flash or
            CustomRoles.Alchemist or
            CustomRoles.Tired;
    }
    public static bool IsRevealingRole(this CustomRoles role, PlayerControl target)
    {
        return (role is CustomRoles.Mayor && Mayor.VisibleToEveryone(target))
            || (role is CustomRoles.SuperStar && SuperStar.VisibleToEveryone(target))
            || (role is CustomRoles.Marshall && target.AllTasksCompleted())
            || (role is CustomRoles.Workaholic && Workaholic.WorkaholicVisibleToEveryone.GetBool())
            || (role is CustomRoles.Doctor && Doctor.VisibleToEveryone(target))
            || (role is CustomRoles.Bait && Bait.BaitNotification.GetBool() && Inspector.CheckBaitCountType)
            || (role is CustomRoles.President && President.CheckReveal(target.PlayerId))
            || (role is CustomRoles.Captain && Captain.CrewCanFindCaptain());
    }
    public static bool IsBetrayalAddon(this CustomRoles role)
    {
        return role is CustomRoles.Madmate
            or CustomRoles.Egoist
            or CustomRoles.Charmed
            or CustomRoles.Recruit
            or CustomRoles.Infected
            or CustomRoles.Contagious
            or CustomRoles.Rascal
            or CustomRoles.Soulless;
    }

    public static bool IsImpOnlyAddon(this CustomRoles role)
    {
        return role is CustomRoles.Mare or
            CustomRoles.LastImpostor or
            CustomRoles.Tricky or
            CustomRoles.Mare or
            CustomRoles.Clumsy or
            CustomRoles.Mimic or
            CustomRoles.TicketsStealer or
            CustomRoles.Circumvent or
            CustomRoles.Swift;
    }
    public static bool CheckAddonConfilct(CustomRoles role, PlayerControl pc, bool checkLimitAddons = true)
    {
        // Only add-ons
        if (!role.IsAdditionRole()) return false;

        // if player already has this addon
        else if (pc.Is(role)) return false;

        // Checking Lovers and Romantics
        else if ((pc.Is(CustomRoles.RuthlessRomantic) || pc.Is(CustomRoles.Romantic) || pc.Is(CustomRoles.VengefulRomantic)) && role is CustomRoles.Lovers) return false;

        // Checking for conflicts with roles
        else if (pc.Is(CustomRoles.GM) || role is CustomRoles.Lovers || pc.Is(CustomRoles.LazyGuy)) return false;

        if (checkLimitAddons)
            if (pc.HasSubRole() && pc.GetCustomSubRoles().Count >= Options.NoLimitAddonsNumMax.GetInt()) return false;


        // Checking for conflicts with roles and other add-ons
        switch (role)
        {
            case CustomRoles.Stubborn:
                if ((pc.GetCustomRole().IsCrewmate() && !Stubborn.CrewCanBeStubborn.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Stubborn.NeutralCanBeStubborn.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Stubborn.ImpCanBeStubborn.GetBool()))
                    return false;
                break;
            case CustomRoles.Autopsy:
                if (pc.Is(CustomRoles.Doctor)
                    || pc.Is(CustomRoles.Tracefinder)
                    || pc.Is(CustomRoles.ScientistTOHE)
                    || pc.Is(CustomRoles.Sunnyboy))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Autopsy.CrewCanBeAutopsy.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Autopsy.NeutralCanBeAutopsy.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Autopsy.ImpCanBeAutopsy.GetBool()))
                    return false;
                break;

            case CustomRoles.Bait:
                if (pc.Is(CustomRoles.Trapper)
                    || pc.Is(CustomRoles.Provocateur)
                    || pc.Is(CustomRoles.Unreportable)
                    || pc.Is(CustomRoles.Burst)
                    || pc.Is(CustomRoles.NiceMini)
                    || pc.Is(CustomRoles.Randomizer)
                    || pc.Is(CustomRoles.Solsticer)
                    || (pc.Is(CustomRoles.Onbound) && Bait.BaitNotification.GetBool())
                    || (pc.Is(CustomRoles.Rebound) && Bait.BaitNotification.GetBool())
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Bait.CrewCanBeBait.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Bait.NeutralCanBeBait.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Bait.ImpCanBeBait.GetBool()))
                    return false;
                break;

            case CustomRoles.Trapper:
                if (pc.Is(CustomRoles.Bait)
                    || pc.Is(CustomRoles.Burst)
                    || pc.Is(CustomRoles.Randomizer)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Trapper.CrewCanBeTrapper.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Trapper.NeutralCanBeTrapper.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Trapper.ImpCanBeTrapper.GetBool()))
                    return false;
                break;

            case CustomRoles.Guesser:
                if (Options.GuesserMode.GetBool() && ((pc.GetCustomRole().IsCrewmate() && !Guesser.CrewCanBeGuesser.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Guesser.NeutralCanBeGuesser.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Guesser.ImpCanBeGuesser.GetBool())))
                    return false;
                if (pc.Is(CustomRoles.EvilGuesser)
                    || pc.Is(CustomRoles.NiceGuesser)
                    || pc.Is(CustomRoles.Judge)
                    || pc.Is(CustomRoles.CopyCat)
                    || pc.Is(CustomRoles.Doomsayer)
                    || pc.Is(CustomRoles.Nemesis)
                    || pc.Is(CustomRoles.Councillor)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.Is(CustomRoles.Phantom) && !Phantom.PhantomCanGuess.GetBool())
                    || (pc.Is(CustomRoles.Terrorist) && (!Terrorist.TerroristCanGuess.GetBool() || Terrorist.CanTerroristSuicideWin.GetBool()))
                    || (pc.Is(CustomRoles.Workaholic) && !Workaholic.WorkaholicCanGuess.GetBool())
                    || (pc.Is(CustomRoles.Solsticer) && !Solsticer.SolsticerCanGuess.GetBool())
                    || (pc.Is(CustomRoles.God) && !God.CanGuess.GetBool()))
                    return false; //Based on guess manager
                if ((pc.GetCustomRole().IsCrewmate() && !Guesser.CrewCanBeGuesser.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Guesser.NeutralCanBeGuesser.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Guesser.ImpCanBeGuesser.GetBool()))
                    return false;
                break;

            case CustomRoles.Mundane:
                if (pc.CanUseKillButton() || pc.GetCustomRole().IsTasklessCrewmate() || pc.Is(CustomRoleTypes.Impostor))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Mundane.CanBeOnCrew.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Mundane.CanBeOnNeutral.GetBool()))
                    return false;
                if (pc.Is(CustomRoles.CopyCat)
                    || pc.Is(CustomRoles.Doomsayer)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.Is(CustomRoles.Phantom) && !Phantom.PhantomCanGuess.GetBool())
                    || (pc.Is(CustomRoles.Terrorist) && (!Terrorist.TerroristCanGuess.GetBool() || Terrorist.CanTerroristSuicideWin.GetBool()))
                    || (pc.Is(CustomRoles.Workaholic) && !Workaholic.WorkaholicCanGuess.GetBool())
                    || (pc.Is(CustomRoles.Solsticer) && !Solsticer.SolsticerCanGuess.GetBool())
                    || (pc.Is(CustomRoles.God) && !God.CanGuess.GetBool()))
                    return false; //Based on guess manager

                // return true only when its a guesser, NG, guesser mode on with crew can guess (if crew role) and nnk can guess (if nnk)
                if (pc.Is(CustomRoles.Guesser) || pc.Is(CustomRoles.NiceGuesser)) return true;
                if (Options.GuesserMode.GetBool())
                {
                    if (pc.GetCustomRole().IsNonNK() && Options.PassiveNeutralsCanGuess.GetBool())
                        return true;
                    if (pc.GetCustomRole().IsCrewmate() && Options.CrewmatesCanGuess.GetBool())
                        return true;
                    else return false;
                }
                else return false;

            case CustomRoles.Onbound:
                if (pc.Is(CustomRoles.SuperStar)
                    || Doctor.VisibleToEveryone(pc)
                    || (pc.Is(CustomRoles.Bait) && Bait.BaitNotification.GetBool())
                    || pc.Is(CustomRoles.LastImpostor)
                    || pc.Is(CustomRoles.NiceMini)
                    || pc.Is(CustomRoles.Mare)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Rebound)
                    || pc.Is(CustomRoles.Workaholic) && !Workaholic.WorkaholicVisibleToEveryone.GetBool())
                    return false; //Based on guess manager
                if ((pc.GetCustomRole().IsCrewmate() && !Onbound.CrewCanBeOnbound.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Onbound.NeutralCanBeOnbound.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Onbound.ImpCanBeOnbound.GetBool()))
                    return false;
                break;

            case CustomRoles.Rebound:
                if (pc.Is(CustomRoles.SuperStar)
                    || Doctor.VisibleToEveryone(pc)
                    || (pc.Is(CustomRoles.Bait) && Bait.BaitNotification.GetBool())
                    || pc.Is(CustomRoles.LastImpostor)
                    || pc.Is(CustomRoles.NiceMini)
                    || pc.Is(CustomRoles.Mare)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Onbound)
                    || pc.Is(CustomRoles.Workaholic) && !Workaholic.WorkaholicVisibleToEveryone.GetBool())
                {
                    return false;
                } //Based on guess manager
                if ((pc.GetCustomRole().IsCrewmate() && !Rebound.CrewCanBeRebound.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Rebound.NeutralCanBeRebound.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Rebound.ImpCanBeRebound.GetBool()))
                    return false;
                break;

            case CustomRoles.DoubleShot:
                if (!Options.GuesserMode.GetBool() && !pc.Is(CustomRoles.EvilGuesser) && !pc.Is(CustomRoles.NiceGuesser) && !pc.Is(CustomRoles.Doomsayer) && !pc.Is(CustomRoles.Guesser))
                    return false;
                if (pc.Is(CustomRoles.CopyCat) 
                    || pc.Is(CustomRoles.Workaholic) && !Workaholic.WorkaholicCanGuess.GetBool()
                    || (pc.Is(CustomRoles.Terrorist) && (!Terrorist.TerroristCanGuess.GetBool() || Terrorist.CanTerroristSuicideWin.GetBool())
                    || (pc.Is(CustomRoles.Phantom) && !Phantom.PhantomCanGuess.GetBool()))
                    || (pc.Is(CustomRoles.Solsticer) && !Solsticer.SolsticerCanGuess.GetBool())
                    || (pc.Is(CustomRoles.God) && !God.CanGuess.GetBool()))
                    return false;
                if (Options.GuesserMode.GetBool())
                {
                    if (DoubleShot.ImpCanBeDoubleShot.GetBool() && !pc.Is(CustomRoles.Guesser) && !pc.Is(CustomRoles.EvilGuesser) && (pc.Is(CustomRoleTypes.Impostor) && !Options.ImpostorsCanGuess.GetBool()))
                        return false;
                    if (DoubleShot.CrewCanBeDoubleShot.GetBool() && !pc.Is(CustomRoles.Guesser) && !pc.Is(CustomRoles.NiceGuesser) && (pc.Is(CustomRoleTypes.Crewmate) && !Options.CrewmatesCanGuess.GetBool()))
                        return false;
                    if (DoubleShot.NeutralCanBeDoubleShot.GetBool() && !pc.Is(CustomRoles.Guesser) && !pc.Is(CustomRoles.Doomsayer) && ((pc.GetCustomRole().IsNonNK() && !Options.PassiveNeutralsCanGuess.GetBool()) || (pc.GetCustomRole().IsNK() && !Options.NeutralKillersCanGuess.GetBool())))
                        return false;
                }
                if ((pc.Is(CustomRoleTypes.Impostor) && !DoubleShot.ImpCanBeDoubleShot.GetBool()) || (pc.Is(CustomRoleTypes.Crewmate) && !DoubleShot.CrewCanBeDoubleShot.GetBool()) || (pc.Is(CustomRoleTypes.Neutral) && !DoubleShot.NeutralCanBeDoubleShot.GetBool()))
                    return false;
                break;

            case CustomRoles.Cyber:
                if (pc.Is(CustomRoles.Doppelganger) || pc.Is(CustomRoles.Celebrity) || pc.Is(CustomRoles.SuperStar))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Cyber.CrewCanBeCyber.GetBool())
                    || (pc.GetCustomRole().IsNeutral() && !Cyber.NeutralCanBeCyber.GetBool())
                    || (pc.GetCustomRole().IsImpostor() && !Cyber.ImpCanBeCyber.GetBool()))
                    return false;
                break;

            case CustomRoles.Reach:
                if (!pc.CanUseKillButton())
                    return false;
                break;

            case CustomRoles.Overclocked:
                if (!pc.CanUseKillButton())
                    return false;
                break;

            case CustomRoles.Lazy:
                if (!Lazy.CheckConflicts(pc))
                    return false;
                break;

            case CustomRoles.Ghoul:
                if (pc.Is(CustomRoles.Lazy)
                    || pc.Is(CustomRoles.LazyGuy))
                    return false;
                if (pc.GetCustomRole().IsNeutral() || pc.GetCustomRole().IsImpostor() || pc.GetCustomRole().IsTasklessCrewmate() || pc.GetCustomRole().IsTaskBasedCrewmate())
                    return false;
                break;

            case CustomRoles.Bloodlust:
                if (pc.Is(CustomRoles.Lazy)
                    || pc.Is(CustomRoles.Merchant)
                    || pc.Is(CustomRoles.Alchemist)
                    || pc.Is(CustomRoles.LazyGuy))
                    return false;
                if (pc.GetCustomRole().IsNeutral() || pc.GetCustomRole().IsImpostor() || pc.GetCustomRole().IsTasklessCrewmate())
                    return false;
                break;

            case CustomRoles.Torch:
                if (pc.Is(CustomRoles.Bewilder)
                    || pc.Is(CustomRoles.Lighter)
                    || pc.Is(CustomRoles.Tired)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if (!pc.GetCustomRole().IsCrewmate())
                    return false;
                break;

            case CustomRoles.Watcher:
                if ((pc.GetCustomRole().IsCrewmate() && !Watcher.CrewCanBeWatcher.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Watcher.NeutralCanBeWatcher.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Watcher.ImpCanBeWatcher.GetBool()))
                    return false;
                break;

            case CustomRoles.Aware:
                if ((pc.GetCustomRole().IsCrewmate() && !Aware.CrewCanBeAware.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Aware.NeutralCanBeAware.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Aware.ImpCanBeAware.GetBool()))
                    return false;
                break;

            case CustomRoles.Fragile:
                if (pc.Is(CustomRoles.Lucky)
                  //  || pc.Is(CustomRoles.Luckey)
                    || pc.Is(CustomRoles.Guardian)
                    || pc.Is(CustomRoles.Medic)
                    || pc.Is(CustomRoles.Bomber)
                    || pc.Is(CustomRoles.Nuker)
                    || pc.Is(CustomRoles.Jinx)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.CursedWolf)
                    || pc.Is(CustomRoles.Masochist)
                    || pc.Is(CustomRoles.PlagueBearer)
                    || pc.Is(CustomRoles.Pestilence))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Fragile.CrewCanBeFragile.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Fragile.NeutralCanBeFragile.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Fragile.ImpCanBeFragile.GetBool()))
                    return false;
                break;

            case CustomRoles.VoidBallot:
                if (pc.Is(CustomRoles.Mayor)
                    || pc.Is(CustomRoles.Vindicator)
                    || pc.Is(CustomRoles.TicketsStealer)
                    || pc.Is(CustomRoles.Pickpocket)
                    || pc.Is(CustomRoles.Dictator)
                    || pc.Is(CustomRoles.Influenced)
                    || pc.Is(CustomRoles.Silent)
                    || pc.Is(CustomRoles.Tiebreaker))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !VoidBallot.CrewCanBeVoidBallot.GetBool()) || (pc.GetCustomRole().IsNeutral() && !VoidBallot.NeutralCanBeVoidBallot.GetBool()) || (pc.GetCustomRole().IsImpostor() && !VoidBallot.ImpCanBeVoidBallot.GetBool()))
                    return false;
                break;

            case CustomRoles.Antidote:
                if (pc.Is(CustomRoles.Diseased) || pc.Is(CustomRoles.Solsticer))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Antidote.CrewCanBeAntidote.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Antidote.NeutralCanBeAntidote.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Antidote.ImpCanBeAntidote.GetBool()))
                    return false;
                break;

            case CustomRoles.Diseased:
                if (pc.Is(CustomRoles.Antidote) || pc.Is(CustomRoles.Solsticer))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Diseased.CrewCanBeDiseased.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Diseased.NeutralCanBeDiseased.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Diseased.ImpCanBeDiseased.GetBool()))
                    return false;
                break;

            case CustomRoles.Seer:
                if (pc.Is(CustomRoles.Mortician)
                    || pc.Is(CustomRoles.EvilTracker)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Seer.CrewCanBeSeer.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Seer.NeutralCanBeSeer.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Seer.ImpCanBeSeer.GetBool()))
                    return false;
                break;

            case CustomRoles.Sleuth:
                if (pc.Is(CustomRoles.Oblivious)
                    || pc.Is(CustomRoles.Detective)
                    || pc.Is(CustomRoles.Mortician)
                    || pc.Is(CustomRoles.Cleaner)
                    || pc.Is(CustomRoles.Medusa)
                    || pc.Is(CustomRoles.Vulture)
                    || pc.Is(CustomRoles.Coroner))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Sleuth.CrewCanBeSleuth.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Sleuth.NeutralCanBeSleuth.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Sleuth.ImpCanBeSleuth.GetBool()))
                    return false;
                break;

            case CustomRoles.Necroview:
                if (pc.Is(CustomRoles.Doctor)
                    || pc.Is(CustomRoles.God)
                    || pc.Is(CustomRoles.Visionary)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Necroview.CrewCanBeNecroview.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Necroview.NeutralCanBeNecroview.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Necroview.ImpCanBeNecroview.GetBool()))
                    return false;
                break;

            case CustomRoles.Bewilder:
                if (pc.Is(CustomRoles.Torch)
                    //|| pc.Is(CustomRoles.Sunglasses)
                    || pc.Is(CustomRoles.Randomizer)
                    || pc.Is(CustomRoles.Lighter)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Tired)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Bewilder.CrewCanBeBewilder.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Bewilder.NeutralCanBeBewilder.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Bewilder.ImpCanBeBewilder.GetBool()))
                    return false;
                break;

            case CustomRoles.Lucky:
                if (pc.Is(CustomRoles.Guardian)
                 //   || pc.Is(CustomRoles.Luckey)
                    || pc.Is(CustomRoles.Unlucky)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Fragile))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Lucky.CrewCanBeLucky.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Lucky.NeutralCanBeLucky.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Lucky.ImpCanBeLucky.GetBool()))
                    return false;
                break;

            case CustomRoles.Unlucky:
                if (//pc.Is(CustomRoles.Luckey)
                    pc.Is(CustomRoles.Vector)
                    || pc.Is(CustomRoles.Lucky)
                    || pc.Is(CustomRoles.Lucky)
                    || pc.Is(CustomRoles.Vector)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Taskinator))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Unlucky.CrewCanBeUnlucky.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Unlucky.NeutralCanBeUnlucky.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Unlucky.ImpCanBeUnlucky.GetBool()))
                    return false;
                break;

            case CustomRoles.Ntr:
                if (pc.Is(CustomRoles.Lovers)
                    || pc.Is(CustomRoles.Hater)
                    || pc.Is(CustomRoles.GuardianAngelTOHE)
                    || pc.Is(CustomRoles.RuthlessRomantic)
                    || pc.Is(CustomRoles.Romantic)
                    || pc.Is(CustomRoles.VengefulRomantic))
                    return false;
                break;

            case CustomRoles.Madmate:
                if (pc.Is(CustomRoles.Sidekick)
                    || pc.Is(CustomRoles.SuperStar)
                    || pc.Is(CustomRoles.Egoist)
                    || pc.Is(CustomRoles.Rascal)
                    || pc.Is(CustomRoles.NiceMini))
                    return false;
                if (!pc.CanBeMadmate(inGame: false))
                    return false;
                break;

            case CustomRoles.Oblivious:
                if (pc.Is(CustomRoles.Detective)
                    || pc.Is(CustomRoles.Vulture)
                    || pc.Is(CustomRoles.Sleuth)
                    || pc.Is(CustomRoles.Cleaner)
                    || pc.Is(CustomRoles.Amnesiac)
                    || pc.Is(CustomRoles.Coroner)
                    || pc.Is(CustomRoles.Medusa)
                    || pc.Is(CustomRoles.Mortician)
                    || pc.Is(CustomRoles.Medium)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Oblivious.CrewCanBeOblivious.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Oblivious.NeutralCanBeOblivious.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Oblivious.ImpCanBeOblivious.GetBool()))
                    return false;
                break;

            case CustomRoles.Tiebreaker:
                if (pc.Is(CustomRoles.Dictator)
                    || pc.Is(CustomRoles.VoidBallot)
                    || pc.Is(CustomRoles.Influenced)
                    || pc.Is(CustomRoles.GuardianAngelTOHE)) return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Tiebreaker.CrewCanBeTiebreaker.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Tiebreaker.NeutralCanBeTiebreaker.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Tiebreaker.ImpCanBeTiebreaker.GetBool())) return false;
                break;

            case CustomRoles.Youtuber:
                if (pc.Is(CustomRoles.Madmate)
                    || pc.Is(CustomRoles.NiceMini)
                    || pc.Is(CustomRoles.Randomizer)
                    || pc.Is(CustomRoles.Sheriff)
                    || pc.Is(CustomRoles.Hurried)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if (!pc.GetCustomRole().IsCrewmate())
                    return false;
                break;

            case CustomRoles.Egoist:
                if (pc.Is(CustomRoles.Sidekick)
                    || pc.Is(CustomRoles.Madmate)
                    || pc.Is(CustomRoles.Hurried)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if (pc.GetCustomRole().IsNeutral())
                    return false;
                if ((pc.GetCustomRole().IsImpostor() && !Egoist.ImpCanBeEgoist.GetBool()) || (pc.GetCustomRole().IsCrewmate() && !Egoist.CrewCanBeEgoist.GetBool()))
                    return false;
                break;

            case CustomRoles.Mimic:
                if (pc.Is(CustomRoles.Nemesis))
                    return false;
                if (!pc.GetCustomRole().IsImpostor())
                    return false;
                break;

            case CustomRoles.Rascal:
                if (pc.Is(CustomRoles.SuperStar)
                    || pc.Is(CustomRoles.NiceMini)
                    || pc.Is(CustomRoles.Madmate))
                    return false;
                if (!pc.GetCustomRole().IsCrewmate())
                    return false;
                break;

            case CustomRoles.TicketsStealer:
                if (pc.Is(CustomRoles.Vindicator)
                    || pc.Is(CustomRoles.Bomber)
                    || pc.Is(CustomRoles.Nuker)
                    || pc.Is(CustomRoles.VoidBallot))
                    return false;
                if (!pc.GetCustomRole().IsImpostor())
                    return false;
                break;
            case CustomRoles.Tricky:
                if (!pc.GetCustomRole().IsImpostor())
                    return false;
                break;
            case CustomRoles.Mare:
                if (pc.Is(CustomRoles.Underdog)
                    || pc.Is(CustomRoles.Berserker)
                    || pc.Is(CustomRoles.Inhibitor)
                    || pc.Is(CustomRoles.Saboteur)
                    || pc.Is(CustomRoles.Swift)
                    || pc.Is(CustomRoles.Nemesis)
                    || pc.Is(CustomRoles.Sniper)
                    || pc.Is(CustomRoles.Fireworker)
                    || pc.Is(CustomRoles.Ludopath)
                    || pc.Is(CustomRoles.Swooper)
                    || pc.Is(CustomRoles.Vampire)
                    || pc.Is(CustomRoles.Vampiress)
                    || pc.Is(CustomRoles.Arrogance)
                    || pc.Is(CustomRoles.LastImpostor)
                    || pc.Is(CustomRoles.Bomber)
                    || pc.Is(CustomRoles.Nuker)
                    || pc.Is(CustomRoles.Trapster)
                    || pc.Is(CustomRoles.Onbound)
                    || pc.Is(CustomRoles.Rebound)
                    || pc.Is(CustomRoles.Tired))
                    return false;
                if (!pc.GetCustomRole().IsImpostor())
                    return false;
                break;

            case CustomRoles.Swift:
                if (pc.Is(CustomRoles.Bomber)
                    || pc.Is(CustomRoles.Nuker)
                    || pc.Is(CustomRoles.Trapster)
                    || pc.Is(CustomRoles.Kamikaze)
                    || pc.Is(CustomRoles.Swooper)
                    || pc.Is(CustomRoles.Vampire)
                    || pc.Is(CustomRoles.Vampiress)
                    || pc.Is(CustomRoles.Scavenger)
                    || pc.Is(CustomRoles.Puppeteer)
                    || pc.Is(CustomRoles.Mastermind)
                    || pc.Is(CustomRoles.Warlock)
                    || pc.Is(CustomRoles.Witch)
                    || pc.Is(CustomRoles.Nemesis)
                    || pc.Is(CustomRoles.Mare)
                    || pc.Is(CustomRoles.Clumsy)
                    || pc.Is(CustomRoles.Wildling)
                    || pc.Is(CustomRoles.Consigliere)
                    || pc.Is(CustomRoles.Butcher)
                    || pc.Is(CustomRoles.KillingMachine))
                    return false;
                if (!pc.GetCustomRole().IsImpostor())
                    return false;
                break;

            case CustomRoles.Nimble:
                if (Knight.CheckCanUseVent(pc)
                    || pc.Is(CustomRoles.CopyCat))
                    return false;
                if (!pc.GetCustomRole().IsTasklessCrewmate())
                    return false;
                break;

            case CustomRoles.Circumvent:
                if (pc.GetCustomRole() is CustomRoles.Vampire or CustomRoles.Vampiress && !Vampire.CheckCanUseVent()
                    || pc.Is(CustomRoles.Witch) && Witch.ModeSwitchActionOpt.GetValue() == 1
                    || pc.Is(CustomRoles.Swooper)
                    || pc.Is(CustomRoles.Wildling)
                    || pc.Is(CustomRoles.KillingMachine)
                    || pc.Is(CustomRoles.Lurker))
                    return false;
                if (!pc.GetCustomRole().IsImpostor())
                    return false;
                break;

            case CustomRoles.Clumsy:
                if (pc.Is(CustomRoles.Swift)
                    || pc.Is(CustomRoles.Bomber)
                    || pc.Is(CustomRoles.Nuker)
                    || pc.Is(CustomRoles.KillingMachine))
                    return false;
                if (!pc.GetCustomRole().IsImpostor())
                    return false;
                break;

            case CustomRoles.Burst:
                if (pc.Is(CustomRoles.Avanger)
                    || pc.Is(CustomRoles.Trapper)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Bait))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Burst.CrewCanBeBurst.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Burst.NeutralCanBeBurst.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Burst.ImpCanBeBurst.GetBool()))
                    return false;
                break;

            case CustomRoles.Avanger:
                if (pc.Is(CustomRoles.Burst)
                    || pc.Is(CustomRoles.Randomizer)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.NiceMini))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Avanger.CrewCanBeAvanger.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Avanger.NeutralCanBeAvanger.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Avanger.ImpCanBeAvanger.GetBool()))
                    return false;
                break;

            case CustomRoles.Schizophrenic:
                if (pc.Is(CustomRoles.Dictator)
                    || pc.Is(CustomRoles.Madmate)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if (!pc.GetCustomRole().IsImpostor() && !pc.GetCustomRole().IsCrewmate())
                    return false;
                if ((pc.GetCustomRole().IsImpostor() && !Schizophrenic.CanBeImp.GetBool()) || (pc.GetCustomRole().IsCrewmate() && !Schizophrenic.CanBeCrew.GetBool()))
                    return false;
                if (pc.GetCustomRole().IsNotKnightable() && Schizophrenic.DualVotes.GetBool())
                    return false;
                break;

            case CustomRoles.Loyal:
                if (pc.Is(CustomRoles.Madmate)
                    || pc.Is(CustomRoles.Oiiai)
                    || pc.Is(CustomRoles.GuardianAngelTOHE)
                    || pc.Is(CustomRoles.Influenced)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.NiceMini)
                    || pc.Is(CustomRoles.EvilMini)
                    || (pc.Is(CustomRoles.CopyCat) && CopyCat.CanCopyTeamChangingAddon()))
                    return false;
                if (!pc.GetCustomRole().IsImpostor() && !pc.GetCustomRole().IsCrewmate())
                    return false;
                if ((pc.GetCustomRole().IsImpostor() && !Loyal.ImpCanBeLoyal.GetBool()) || (pc.GetCustomRole().IsCrewmate() && !Loyal.CrewCanBeLoyal.GetBool()))
                    return false;
                break;

            case CustomRoles.Gravestone:
                if (pc.Is(CustomRoles.SuperStar)
                    || pc.Is(CustomRoles.Innocent)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.NiceMini))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Gravestone.CrewCanBeGravestone.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Gravestone.NeutralCanBeGravestone.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Gravestone.ImpCanBeGravestone.GetBool()))
                    return false;
                break;

            case CustomRoles.Unreportable:
                if (pc.Is(CustomRoles.Randomizer)
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Bait))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Unreportable.CrewCanBeUnreportable.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Unreportable.NeutralCanBeUnreportable.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Unreportable.ImpCanBeUnreportable.GetBool()))
                    return false;
                break;

            case CustomRoles.Flash:
                if (pc.Is(CustomRoles.Swooper) 
                    || pc.Is(CustomRoles.Solsticer)
                    || pc.Is(CustomRoles.Tired)
                    || pc.Is(CustomRoles.Statue))
                    return false;
                break;

            case CustomRoles.Fool:
                if (pc.Is(CustomRoles.Mechanic)
                    || pc.Is(CustomRoles.GuardianAngelTOHE))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Fool.CrewCanBeFool.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Fool.NeutralCanBeFool.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Fool.ImpCanBeFool.GetBool()))
                    return false;
                break;

            case CustomRoles.Influenced:
                if (pc.Is(CustomRoles.Dictator)
                    || pc.Is(CustomRoles.Loyal)
                    || pc.Is(CustomRoles.VoidBallot)
                    || pc.Is(CustomRoles.Tiebreaker)
                    || pc.Is(CustomRoles.Collector)
                    || pc.Is(CustomRoles.Keeper))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Influenced.CanBeOnCrew.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Influenced.CanBeOnNeutral.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Influenced.CanBeOnImp.GetBool()))
                    return false;
                break;

            case CustomRoles.Oiiai:
                if (pc.Is(CustomRoles.Loyal) || pc.Is(CustomRoles.Solsticer)) return false;
                if ((pc.GetCustomRole().IsNeutral() && !Oiiai.CanBeOnNeutral.GetBool()) || (pc.GetCustomRole().IsCrewmate() && !Oiiai.CanBeOnCrew.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Oiiai.CanBeOnImp.GetBool()))
                    return false;
                break;

            case CustomRoles.Hurried:
                if (pc.Is(CustomRoles.Youtuber) || pc.Is(CustomRoles.Egoist) || pc.Is(CustomRoles.Solsticer)) return false;
                if (pc.Is(CustomRoles.Madmate) && !Hurried.CanBeOnMadMate.GetBool()) return false;
                if (!pc.GetCustomRole().IsCrewmate() && !pc.Is(CustomRoles.Madmate)) return false;
                if (pc.GetCustomRole().IsTasklessCrewmate()) return false;
                if (pc.GetCustomRole().IsTaskBasedCrewmate() && !Hurried.CanBeOnTaskBasedCrew.GetBool()) return false;
                break;

            case CustomRoles.Silent:
                if (pc.Is(CustomRoles.Dictator) || pc.Is(CustomRoles.VoidBallot)) return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Silent.CanBeOnCrew.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Silent.CanBeOnNeutral.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Silent.CanBeOnImp.GetBool()))
                    return false;
                break;

            case CustomRoles.Rainbow:
                if ((pc.GetCustomRole().IsCrewmate() && !Rainbow.CrewCanBeRainbow.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Rainbow.NeutralCanBeRainbow.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Rainbow.ImpCanBeRainbow.GetBool()))
                    return false;
                break;
            
            case CustomRoles.Susceptible:
                if ((pc.GetCustomRole().IsCrewmate() && !Susceptible.CanBeOnCrew.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Susceptible.CanBeOnNeutral.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Susceptible.CanBeOnImp.GetBool()))
                  return false;
                break;

            case CustomRoles.Tired:
                if (pc.Is(CustomRoles.Overseer)
                  || pc.Is(CustomRoles.Alchemist)
                  || pc.Is(CustomRoles.Torch)
                  || pc.Is(CustomRoles.Bewilder)
                  || pc.Is(CustomRoles.Lighter)
                  || pc.Is(CustomRoles.Flash)
                  || pc.Is(CustomRoles.Mare)) 
                  return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Tired.CanBeOnCrew.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Tired.CanBeOnNeutral.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Tired.CanBeOnImp.GetBool())) 
                  return false;
            break;

            case CustomRoles.Statue:
                if (pc.Is(CustomRoles.Alchemist)
                    || pc.Is(CustomRoles.Flash)
                    || pc.Is(CustomRoles.Tired))
                    return false;
                if ((pc.GetCustomRole().IsCrewmate() && !Statue.CanBeOnCrew.GetBool()) || (pc.GetCustomRole().IsNeutral() && !Statue.CanBeOnNeutral.GetBool()) || (pc.GetCustomRole().IsImpostor() && !Statue.CanBeOnImp.GetBool()))
                    return false;
                break;
        }

        return true;
    }
    public static RoleTypes GetRoleTypes(this CustomRoles role)
        => GetVNRole(role) switch
        {
            CustomRoles.Impostor => RoleTypes.Impostor,
            CustomRoles.Scientist => RoleTypes.Scientist,
            CustomRoles.Engineer => RoleTypes.Engineer,
            CustomRoles.GuardianAngel => RoleTypes.GuardianAngel,
            CustomRoles.Shapeshifter => RoleTypes.Shapeshifter,
            CustomRoles.Crewmate => RoleTypes.Crewmate,
            _ => role.IsImpostor() ? RoleTypes.Impostor : RoleTypes.Crewmate,
        };
    public static bool IsDesyncRole(this CustomRoles role) => role.GetDYRole() != RoleTypes.GuardianAngel;
    /// <summary>
    /// Role is Madmate Or Impostor
    /// </summary>
    public static bool IsImpostorTeam(this CustomRoles role) => role.IsImpostor() || role == CustomRoles.Madmate;
    /// <summary>
    /// Role Is Not Impostor nor Madmate Nor Neutral.
    /// </summary>
    public static bool IsCrewmate(this CustomRoles role) => !role.IsImpostor() && !role.IsNeutral() && !role.IsMadmate();
    /// <summary>
    /// Role is Rascal or Madmate and not trickster.
    /// </summary>
    public static bool IsImpostorTeamV2(this CustomRoles role) => role == CustomRoles.Rascal || role == CustomRoles.Madmate || (role.IsImpostorTeamV3() && role != CustomRoles.Trickster && (!role.IsConverted() || role is CustomRoles.Madmate));
    /// <summary>
    /// Role Is Converting or neutral.
    /// </summary>
    public static bool IsNeutralTeamV2(this CustomRoles role) => (role.IsConverted() && role != CustomRoles.Madmate || role.IsNeutral()) && role != CustomRoles.Madmate;
    /// <summary>
    /// Role is not impostor nor rascal nor madmate nor converting nor neutral or role is trickster.
    /// </summary>
    public static bool IsCrewmateTeamV2(this CustomRoles role) => !(role.IsImpostorTeamV2() || role.IsNeutralTeamV2()) || role == CustomRoles.Trickster;
    public static bool IsImpostorTeamV3(this CustomRoles role) => role.IsImpostor() || role.IsMadmate();
    public static bool IsNeutralKillerTeam(this CustomRoles role) => role.IsNK() && !role.IsMadmate();
    public static bool IsPassiveNeutralTeam(this CustomRoles role) => role.IsNonNK() && !role.IsMadmate();
    public static bool IsNNK(this CustomRoles role) => role.IsNeutral() && !role.IsNK();
    public static bool IsVanilla(this CustomRoles role)
    {
        return role is
            CustomRoles.Crewmate or
            CustomRoles.Engineer or
            CustomRoles.Scientist or
            CustomRoles.GuardianAngel or
            CustomRoles.Impostor or
            CustomRoles.Shapeshifter;
    }
    public static CustomRoleTypes GetCustomRoleTypes(this CustomRoles role)
    {
        CustomRoleTypes type = CustomRoleTypes.Crewmate;
        if (role.IsImpostor()) type = CustomRoleTypes.Impostor;
        if (role.IsNeutral()) type = CustomRoleTypes.Neutral;
        if (role.IsAdditionRole()) type = CustomRoleTypes.Addon;
        return type;
    }
    public static bool RoleExist(this CustomRoles role, bool countDead = false) => Main.AllPlayerControls.Any(x => x.Is(role) && (x.IsAlive() || countDead));
    public static int GetCount(this CustomRoles role)
    {
        if (role.IsVanilla())
        {
            if (Options.DisableVanillaRoles.GetBool()) return 0;
            var roleOpt = Main.NormalOptions.RoleOptions;
            return role switch
            {
                CustomRoles.Engineer => roleOpt.GetNumPerGame(RoleTypes.Engineer),
                CustomRoles.Scientist => roleOpt.GetNumPerGame(RoleTypes.Scientist),
                CustomRoles.Shapeshifter => roleOpt.GetNumPerGame(RoleTypes.Shapeshifter),
                CustomRoles.GuardianAngel => roleOpt.GetNumPerGame(RoleTypes.GuardianAngel),
                CustomRoles.Crewmate => roleOpt.GetNumPerGame(RoleTypes.Crewmate),
                _ => 0
            };
        }
        else
        {
            return Options.GetRoleCount(role);
        }
    }
    public static int GetMode(this CustomRoles role) => Options.GetRoleSpawnMode(role);
    public static float GetChance(this CustomRoles role)
    {
        if (role.IsVanilla())
        {
            var roleOpt = Main.NormalOptions.RoleOptions;
            return role switch
            {
                CustomRoles.Engineer => roleOpt.GetChancePerGame(RoleTypes.Engineer),
                CustomRoles.Scientist => roleOpt.GetChancePerGame(RoleTypes.Scientist),
                CustomRoles.Shapeshifter => roleOpt.GetChancePerGame(RoleTypes.Shapeshifter),
                CustomRoles.GuardianAngel => roleOpt.GetChancePerGame(RoleTypes.GuardianAngel),
                CustomRoles.Crewmate => roleOpt.GetChancePerGame(RoleTypes.Crewmate),
                _ => 0
            } / 100f;
        }
        else
        {
            return Options.GetRoleChance(role);
        }
    }
    public static bool IsEnable(this CustomRoles role) => role.GetCount() > 0;
    public static CountTypes GetCountTypes(this CustomRoles role)
       => role switch
       {
           CustomRoles.GM => CountTypes.OutOfGame,
           CustomRoles.Jackal => CountTypes.Jackal,
           CustomRoles.Sidekick => CountTypes.Jackal,
           CustomRoles.Doppelganger => CountTypes.Doppelganger,
           CustomRoles.Bandit => CountTypes.Bandit,
           CustomRoles.Poisoner => CountTypes.Poisoner,
           CustomRoles.Pelican => CountTypes.Pelican,
           CustomRoles.Minion => CountTypes.Impostor,
           CustomRoles.Bloodmoon => CountTypes.Impostor,
           CustomRoles.Demon => CountTypes.Demon,
           CustomRoles.BloodKnight => CountTypes.BloodKnight,
           CustomRoles.Cultist => CountTypes.Cultist,
           CustomRoles.HexMaster => CountTypes.HexMaster,
           CustomRoles.Necromancer => CountTypes.Necromancer,
           CustomRoles.Stalker => !Stalker.SnatchesWin.GetBool() ? CountTypes.Stalker : CountTypes.Crew,
           CustomRoles.Arsonist => Arsonist.CanIgniteAnytime() ? CountTypes.Arsonist : CountTypes.Crew,
           CustomRoles.Shroud => CountTypes.Shroud,
           CustomRoles.Werewolf => CountTypes.Werewolf,
           CustomRoles.Wraith => CountTypes.Wraith,
           CustomRoles.Pestilence => CountTypes.Pestilence,
           CustomRoles.PlagueBearer => CountTypes.PlagueBearer,
           CustomRoles.Agitater => CountTypes.Agitater,
           CustomRoles.Parasite => CountTypes.Impostor,
           CustomRoles.SerialKiller => CountTypes.SerialKiller,
           CustomRoles.Quizmaster => CountTypes.Quizmaster,
           CustomRoles.Juggernaut => CountTypes.Juggernaut,
           CustomRoles.Jinx => CountTypes.Jinx,
           CustomRoles.Infectious or CustomRoles.Infected => CountTypes.Infectious,
           CustomRoles.Crewpostor => CountTypes.Impostor,
           CustomRoles.Pyromaniac => CountTypes.Pyromaniac,
           CustomRoles.PlagueDoctor => CountTypes.PlagueDoctor,
           CustomRoles.Virus => CountTypes.Virus,
           CustomRoles.PotionMaster => CountTypes.PotionMaster,
           CustomRoles.Pickpocket => CountTypes.Pickpocket,
           CustomRoles.Traitor => CountTypes.Traitor,
           CustomRoles.Medusa => CountTypes.Medusa,
           CustomRoles.Refugee => CountTypes.Impostor,
           CustomRoles.Huntsman => CountTypes.Huntsman,
           CustomRoles.Glitch => CountTypes.Glitch,       
           CustomRoles.Spiritcaller => CountTypes.Spiritcaller,
           CustomRoles.RuthlessRomantic => CountTypes.RuthlessRomantic,
           CustomRoles.SchrodingersCat => CountTypes.None,
           CustomRoles.Solsticer => CountTypes.None,
           _ => role.IsImpostorTeam() ? CountTypes.Impostor : CountTypes.Crew,

           // CustomRoles.Phantom => CountTypes.OutOfGame,
           //   CustomRoles.CursedSoul => CountTypes.OutOfGame, // if they count as OutOfGame, it prevents them from winning lmao
       };
    public static CustomWinner GetNeutralCustomWinnerFromRole(this CustomRoles role) // only to be used for Neutrals
        => role switch
        {
            CustomRoles.Jester => CustomWinner.Jester,
            CustomRoles.Terrorist => CustomWinner.Terrorist,
            CustomRoles.Lovers => CustomWinner.Lovers,
            CustomRoles.Executioner => CustomWinner.Executioner,
            CustomRoles.Arsonist => CustomWinner.Arsonist,
            CustomRoles.Pyromaniac => CustomWinner.Pyromaniac,
            CustomRoles.Agitater => CustomWinner.Agitater,
            CustomRoles.Revolutionist => CustomWinner.Revolutionist,
            CustomRoles.Jackal => CustomWinner.Jackal,
            CustomRoles.Sidekick => CustomWinner.Sidekick,
            CustomRoles.God => CustomWinner.God,
            CustomRoles.Vector => CustomWinner.Vector,
            CustomRoles.Innocent => CustomWinner.Innocent,
            CustomRoles.Pelican => CustomWinner.Pelican,
            CustomRoles.Youtuber => CustomWinner.Youtuber,
            CustomRoles.Egoist => CustomWinner.Egoist,
            CustomRoles.Demon => CustomWinner.Demon,
            CustomRoles.Stalker => CustomWinner.Stalker,
            CustomRoles.Workaholic => CustomWinner.Workaholic,
            CustomRoles.Solsticer => CustomWinner.Solsticer,
            CustomRoles.Collector => CustomWinner.Collector,
            CustomRoles.BloodKnight => CustomWinner.BloodKnight,
            CustomRoles.Poisoner => CustomWinner.Poisoner,
            CustomRoles.HexMaster => CustomWinner.HexMaster,
            CustomRoles.Cultist => CustomWinner.Cultist,
            CustomRoles.Wraith => CustomWinner.Wraith,
            CustomRoles.Bandit => CustomWinner.Bandit,
            CustomRoles.Pirate => CustomWinner.Pirate,
            CustomRoles.SerialKiller => CustomWinner.SerialKiller,
            CustomRoles.Quizmaster => CustomWinner.Quizmaster,
            CustomRoles.Werewolf => CustomWinner.Werewolf,
            CustomRoles.Necromancer => CustomWinner.Necromancer,
            CustomRoles.Huntsman => CustomWinner.Huntsman,
            CustomRoles.Juggernaut => CustomWinner.Juggernaut,
            CustomRoles.Infectious => CustomWinner.Infectious,
            CustomRoles.Virus => CustomWinner.Virus,
            CustomRoles.Phantom => CustomWinner.Phantom,
            CustomRoles.Jinx => CustomWinner.Jinx,
            CustomRoles.CursedSoul => CustomWinner.CursedSoul,
            CustomRoles.PotionMaster => CustomWinner.PotionMaster,
            CustomRoles.Pickpocket => CustomWinner.Pickpocket,
            CustomRoles.Traitor => CustomWinner.Traitor,
            CustomRoles.Vulture => CustomWinner.Vulture,
            CustomRoles.Pestilence => CustomWinner.Pestilence,
            CustomRoles.Medusa => CustomWinner.Medusa,
            CustomRoles.Spiritcaller => CustomWinner.Spiritcaller,
            CustomRoles.Glitch => CustomWinner.Glitch,
            CustomRoles.PlagueBearer => CustomWinner.Plaguebearer,
            CustomRoles.Masochist => CustomWinner.Masochist,
            CustomRoles.Doomsayer => CustomWinner.Doomsayer,
            CustomRoles.Shroud => CustomWinner.Shroud,
            CustomRoles.Seeker => CustomWinner.Seeker,
            CustomRoles.SoulCollector => CustomWinner.SoulCollector,
            CustomRoles.RuthlessRomantic => CustomWinner.RuthlessRomantic,
            CustomRoles.Mini => CustomWinner.NiceMini,
            CustomRoles.Doppelganger => CustomWinner.Doppelganger,
            _ => throw new System.NotImplementedException()

        };
    public static CustomRoles GetNeutralCustomRoleFromCountType(this CountTypes type) //only to be used for NKs
        => type switch
        {
            CountTypes.OutOfGame => CustomRoles.GM,
            CountTypes.Jackal => CustomRoles.Jackal,
            CountTypes.Doppelganger => CustomRoles.Doppelganger,
            CountTypes.Bandit => CustomRoles.Bandit,
            CountTypes.Poisoner => CustomRoles.Poisoner,
            CountTypes.Pelican => CustomRoles.Pelican,
            CountTypes.Demon => CustomRoles.Demon,
            CountTypes.BloodKnight => CustomRoles.BloodKnight,
            CountTypes.Cultist => CustomRoles.Cultist,
            CountTypes.HexMaster => CustomRoles.HexMaster,
            CountTypes.Necromancer => CustomRoles.Necromancer,
            CountTypes.Shroud => CustomRoles.Shroud,
            CountTypes.Werewolf => CustomRoles.Werewolf,
            CountTypes.Wraith => CustomRoles.Wraith,
            CountTypes.Pestilence => CustomRoles.Pestilence,
            CountTypes.PlagueBearer => CustomRoles.PlagueBearer,
            CountTypes.Agitater => CustomRoles.Agitater,
            CountTypes.SerialKiller => CustomRoles.SerialKiller,
            CountTypes.Quizmaster => CustomRoles.Quizmaster,
            CountTypes.Juggernaut => CustomRoles.Juggernaut,
            CountTypes.Jinx => CustomRoles.Jinx,
            CountTypes.Infectious => CustomRoles.Infectious,
            CountTypes.Pyromaniac => CustomRoles.Pyromaniac,
            CountTypes.Virus => CustomRoles.Virus,
            CountTypes.PotionMaster => CustomRoles.PotionMaster,
            CountTypes.Pickpocket => CustomRoles.Pickpocket,
            CountTypes.Traitor => CustomRoles.Traitor,
            CountTypes.Medusa => CustomRoles.Medusa,
            CountTypes.Huntsman => CustomRoles.Huntsman,
            CountTypes.Glitch => CustomRoles.Glitch,
            CountTypes.Stalker => CustomRoles.Stalker,
            CountTypes.Spiritcaller => CustomRoles.Spiritcaller,
            CountTypes.Arsonist => CustomRoles.Arsonist,
            CountTypes.RuthlessRomantic => CustomRoles.RuthlessRomantic,
            _ => throw new System.NotImplementedException()
        };
    public static bool HasSubRole(this PlayerControl pc) => Main.PlayerStates[pc.PlayerId].SubRoles.Any();
}
public enum CustomRoleTypes
{
    Crewmate,
    Impostor,
    Neutral,
    Addon,
}
public enum CountTypes
{
    OutOfGame,
    None,
    Crew,
    Impostor,
    Jackal,
    Doppelganger,
    Bandit,
    Pelican,
    PlagueDoctor,
    Demon,
    BloodKnight,
    Poisoner,
    Charmed,
    Cultist,
    HexMaster,
    Wraith,
    SerialKiller,
    Juggernaut,
    Infectious,
    Virus,
    Stalker,
    Jinx,
    PotionMaster,
    Pickpocket,
    Traitor,
    Medusa,
    Spiritcaller,
    Pestilence,
    Quizmaster,
    PlagueBearer,
    Glitch,
    Arsonist,
    Huntsman,
    Pyromaniac,
    Shroud,
    Werewolf,
    Agitater,
    RuthlessRomantic,
    Necromancer
}
