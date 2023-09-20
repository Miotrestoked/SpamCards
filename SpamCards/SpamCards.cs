using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using SpamCards.Cards;
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

            void debuffCardInit(CardInfo c)
            {
                cards.AddHiddenCard(c);
                debuffCards.Add(c);
            }

            void buffCardInit(CardInfo c)
            {
                cards.AddHiddenCard(c);
                buffCards.Add(c);
            }

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

            //hidden cards (debuffs)
            CustomCard.BuildCard<HealthLoss>(debuffCardInit);
            CustomCard.BuildCard<My600lbsLife>(debuffCardInit);
            CustomCard.BuildCard<Goteem>(debuffCardInit);

            //hidden cards (buffs)
            CustomCard.BuildCard<DamageBuff>(buffCardInit);
            CustomCard.BuildCard<HeckingSmallBoi>(buffCardInit);
        }


        public static Player GetRandomOpponent(List<Player> players, Player currentPlayer)
        {
            List<Player> opponents = new List<Player>();
            foreach (Player player in players)
            {
                if (player.playerID != currentPlayer.playerID && player.teamID != currentPlayer.teamID)
                {
                    opponents.Add(player);
                }
            }

            Random random = new Random();
            int randomPlayer = random.Next(0, players.Count);

            return opponents[randomPlayer];
        }

        public static List<Player> GetOpponents(List<Player> players, Player currentPlayer)
        {
            List<Player> opponents = new List<Player>();
            foreach (Player player in players)
            {
                if (player.playerID != currentPlayer.playerID && player.teamID != currentPlayer.teamID)
                {
                    opponents.Add(player);
                }
            }

            return opponents;
        }

        public static CardInfo findCard(string cardName)
        {
            var cards = ModdingUtils.Utils.Cards.instance;

            return cards.GetCardWithObjectName($"__{ModInitials}__{cardName}");
        }
    }
}