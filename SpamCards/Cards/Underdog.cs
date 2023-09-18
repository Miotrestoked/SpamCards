using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;

namespace SpamCards.Cards
{
    class Underdog : CustomCard
    {
        private int pointTotal;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            int myPoints = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points;
            int mostPoints = 0;

            foreach (Player p in PlayerManager.instance.players)
            {
                if (p.teamID != player.teamID)
                {
                    int points = GameModeManager.CurrentHandler.GetTeamScore(p.teamID).points;
                    if (points > mostPoints)
                    {
                        mostPoints = points;
                    }
                }
            }

            pointTotal = mostPoints - myPoints;
            if (pointTotal > 0)
            {
                gun.damage += ((pointTotal * 5f) / 55f);
            }
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            gun.damage -= ((pointTotal * 5f) / 55f);
        }

        protected override string GetTitle()
        {
            return "Underdog";
        }

        protected override string GetDescription()
        {
            return "Gain damage for each point you are behind the team with the most points.";
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
                    stat = "Flat damage per point",
                    amount = "+5",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
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