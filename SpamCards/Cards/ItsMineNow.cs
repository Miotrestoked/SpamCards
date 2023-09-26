using UnboundLib.Cards;
using UnityEngine;

namespace SpamCards.Cards
{
    class ItsMineNow : CustomCard
    {
        private float hpTotal;

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

            if (hpTotal != 0f)
            {
                foreach (Player p in PlayerManager.instance.players)
                {
                    if (p.teamID != player.teamID)
                    {
                        hpTotal += p.data.maxHealth * .1f;
                        var healthloss = SpamCards.debuffCards[0];
                        //cards.AddCardToPlayer(p, healthloss, addToCardBar:true);
                        cards.AddCardToPlayer(p, healthloss, false, "HL", 0, 0);
                    }
                }
            }

            player.data.maxHealth += hpTotal;
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
            return "Take 10% of each enemy's HP and add it to your own.";
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
                    stat = "Of each enemy's HP",
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