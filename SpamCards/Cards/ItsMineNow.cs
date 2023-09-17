using UnboundLib.Cards;
using UnboundLib.Utils;
using ModdingUtils.Utils;
using UnityEngine;

namespace SpamCards.Cards
{
    class ItsMineNow : CustomCard
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

            float healthToAdd = 0;
            foreach (Player p in PlayerManager.instance.players)
            {
                if (p.teamID != player.teamID)
                {
                    healthToAdd += p.data.maxHealth * .1f;
                    var healthloss = cards.GetCardWithObjectName("Health loss");
                    cards.AddCardToPlayer(p, healthloss, false, "HL", 1, 0);
                }
            }

            player.data.health = 1f + (healthToAdd / player.data.health);
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "It's mine now";
        }

        protected override string GetDescription()
        {
            return "Take 10% of each enemy's hp and add it to your own.";
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
                new CardInfoStat()
                {
                    positive = true,
                    stat = "of each enemy's hp",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
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