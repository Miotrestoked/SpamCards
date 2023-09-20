using System.Collections.Generic;
using UnboundLib.Cards;
using UnityEngine;


namespace SpamCards.Cards
{
    class Your600lbsLife : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<Player> opponents = SpamCards.GetOpponents(PlayerManager.instance.players, player);
            foreach (Player opponent in opponents)
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(opponent, SpamCards.FindCard("My 600 lbs Life"), false, "JC", 0, 0);
            }

            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Your 600 lbs life";
        }
        protected override string GetDescription()
        {
            return "Americanize your opponents";
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
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }
        public override string GetModName()
        {
            return SpamCards.ModInitials;
        }
    }
}
