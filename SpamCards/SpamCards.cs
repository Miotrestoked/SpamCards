using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using HarmonyLib;
using SpamCards.Cards;
using UnboundLib;
using UnboundLib.Cards;

namespace SpamCards
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("pykess.rounds.plugins.moddingutils")]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class SpamCards : BaseUnityPlugin
    {
        private const string ModId = "turtle.mods.spamcards";
        private const string ModName = "Spam Cardpack";
        public const string Version = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "SCP";

        internal static List<CardInfo> debuffCards = new List<CardInfo>();
        internal static List<CardInfo> buffCards = new List<CardInfo>();

        public static SpamCards instance { get; private set; }

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            instance = this;
            var cards = ModdingUtils.Utils.Cards.instance;

            //regular cards
            CustomCard.BuildCard<ItsMineNow>();
            CustomCard.BuildCard<SmallTweak>();
            CustomCard.BuildCard<LessIsMore>();
            CustomCard.BuildCard<FruitySmoothie>();
            CustomCard.BuildCard<HighRiskHighReward>();
            CustomCard.BuildCard<SpeedTransfer>();
            CustomCard.BuildCard<RussianRoulette>();
            CustomCard.BuildCard<AttackOfTheDwarves>();
            CustomCard.BuildCard<EqualPlayingGrounds>();
            CustomCard.BuildCard<PLSNerf>();
            CustomCard.BuildCard<ResetHP>();
            CustomCard.BuildCard<Your600lbsLife>();
            CustomCard.BuildCard<AmmoHeist>();
            CustomCard.BuildCard<FasterBullets>();
            CustomCard.BuildCard<FasterBulletsPlus>();
            CustomCard.BuildCard<FasterBulletsPlusPlus>();
            CustomCard.BuildCard<FasterBulletsPlusPlusPlus>();
            CustomCard.BuildCard<EquivalentExchange>();
            CustomCard.BuildCard<DoYouDare>();
            CustomCard.BuildCard<Restrategize>();
            CustomCard.BuildCard<BurstOfAmmo>();
            CustomCard.BuildCard<Lootbag>();

            //hidden cards (debuffs)
            CustomCard.BuildCard<HealthLoss>(DebuffCardInit);
            CustomCard.BuildCard<My600lbsLife>(DebuffCardInit);
            CustomCard.BuildCard<Goteem>(DebuffCardInit);
            CustomCard.BuildCard<InfiniBurst>(DebuffCardInit);

            //hidden cards (buffs)
            CustomCard.BuildCard<DamageBuff>(BuffCardInit);
            CustomCard.BuildCard<HeckingSmallBoi>(BuffCardInit);
            return;

            void BuffCardInit(CardInfo c)
            {
                cards.AddHiddenCard(c);
                buffCards.Add(c);
            }

            void DebuffCardInit(CardInfo c)
            {
                cards.AddHiddenCard(c);
                debuffCards.Add(c);
            }
        }

        public static Player GetRandomOpponent(Player player)
        {
            List<Player> opponents = GetOpponents(player);

            return opponents.GetRandom<Player>();
        }

        public static List<Player> GetOpponents(Player player)
        {
            return PlayerManager.instance.players.Where(plr => plr.teamID != player.teamID).ToList();
        }

        public static CardInfo FindCard(string cardName)
        {
            var cards = ModdingUtils.Utils.Cards.instance;

            return cards.GetCardWithObjectName($"__{ModInitials}__{cardName}");
        }
    }
}