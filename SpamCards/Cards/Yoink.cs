using UnboundLib.Cards;
using UnityEngine;
using ModdingUtils.Utils;
using UnboundLib;

namespace SpamCards.Cards
{
    class Yoink : CustomCard
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

            Player enemy = PlayerStatus.GetRandomEnemyPlayer(player);
            CardInfo card = enemy.data.currentCards.GetRandom<CardInfo>();

            cards.RemoveCardFromPlayer(enemy, card, ModdingUtils.Utils.Cards.SelectionType.All);
            cards.AddCardToPlayer(player, card, false, "", 0, 0);
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Yoink";
        }

        protected override string GetDescription()
        {
            return "Steal a card from a random opponent";
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        public override string GetModName()
        {
            return SpamCards.ModInitials;
        }
    }
}