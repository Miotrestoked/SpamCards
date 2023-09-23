using UnboundLib.Cards;
using UnityEngine;
using ModdingUtils.Utils;
using UnboundLib;

namespace SpamCards.Cards
{
    class Yeet : CustomCard
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
            CardInfo lastCard = player.data.currentCards[player.data.currentCards.Count - 1];

            cards.RemoveCardFromPlayer(player, lastCard, ModdingUtils.Utils.Cards.SelectionType.Newest);
            cards.AddCardToPlayer(enemy, lastCard, false, "??", 0, 0);
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Yeet";
        }

        protected override string GetDescription()
        {
            return "Give your last card to an opponent";
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