using UnboundLib.Cards;
using UnityEngine;

namespace SpamCards.Cards
{
    class Goteem : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.ammo -= 3;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        public override bool GetEnabled()
        {
            return false;
        }

        protected override string GetTitle()
        {
            return "Goteem";
        }

        protected override string GetDescription()
        {
            return "There goes 3 of your ammo";
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Ammo",
                    amount = "-3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }

        public override string GetModName()
        {
            return SpamCards.ModInitials;
        }
    }
}