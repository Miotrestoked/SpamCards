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
        internal static List<CardInfo> Allcards = new List<CardInfo>();

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
                Allcards.Add(c);
                debuffCards.Add(c);
            }

            void buffCardInit(CardInfo c)
            {
                cards.AddHiddenCard(c);
                Allcards.Add(c);
                buffCards.Add(c);
            }
            
            void loadCard(CardInfo c)
            {
                Allcards.Add(c);
            }

            //regular cards
            CustomCard.BuildCard<ItsMineNow>(loadCard);
            CustomCard.BuildCard<SmallTweak>(loadCard);
            CustomCard.BuildCard<LessIsMore>(loadCard);
            CustomCard.BuildCard<FruitySmoothie>(loadCard);
            CustomCard.BuildCard<HighRiskHighReward>(loadCard);
            CustomCard.BuildCard<SpeedTransfer>(loadCard);
            CustomCard.BuildCard<RussianRoulette>(loadCard);
            CustomCard.BuildCard<AttackOfTheDwarves>(loadCard);
            CustomCard.BuildCard<EqualPlayingGrounds>(loadCard);
            CustomCard.BuildCard<PLSNerf>(loadCard);
            CustomCard.BuildCard<ResetHP>(loadCard);
            CustomCard.BuildCard<Your600lbsLife>(loadCard);
            CustomCard.BuildCard<AmmoHeist>(loadCard);

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
            foreach (CardInfo cardInfo in Allcards)
            {
                if (cardInfo.cardName.Equals(cardName))
                {
                    return cardInfo;
                }
            }

            return null;
        }
    }
}