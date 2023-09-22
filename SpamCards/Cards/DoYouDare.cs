using UnboundLib.Cards;
using UnityEngine;
using System;

namespace SpamCards.Cards
{
    class DoYouDare : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            var cards = ModdingUtils.Utils.Cards.instance;

            var random = new System.Random();
            int roll = random.Next(0, 4);

            if (roll == 0) //too bad
            {
                for (var i = 0; i < 2; i++)
                {
                    roll = random.Next(0, SpamCards.debuffCards.Count);
                    cards.AddCardToPlayer(player, SpamCards.debuffCards[roll], false, "??", 0, 0);
                }
            }
            else //lucky you
            {
                roll = new System.Random().Next(0, SpamCards.debuffCards.Count);
                foreach (var enemy in SpamCards.GetOpponents(player))
                {
                    cards.AddCardToPlayer(enemy, SpamCards.debuffCards[roll], false, "??", 0, 0);
                }
            }
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Do you dare?";
        }

        protected override string GetDescription()
        {
            return "25% chance to give you two random curses, 75% chance to give all of your opponents a random curse";
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.PoisonGreen;
        }

        public override string GetModName()
        {
            return SpamCards.ModInitials;
        }
    }
}