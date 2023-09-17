using System;
using UnboundLib.Cards;
using UnityEngine;

namespace SpamCards.Cards
{
    class SmallTweak : CustomCard
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
            UnityEngine.Debug.Log($"[{SpamCards.ModInitials}][Log] The player's current damage is {player.data.weaponHandler.gun.damage}.");
            
            gun.damage += (15f / 55f);
            
            UnityEngine.Debug.Log($"[{SpamCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            UnityEngine.Debug.Log($"[{SpamCards.ModInitials}][Log] The player's new damage is {player.data.weaponHandler.gun.damage}.");
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Small tweak";
        }

        protected override string GetDescription()
        {
            return "A small, flat boost to your damage.";
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
                    positive = true,
                    stat = "flat damage",
                    amount = "+15",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }

        public override string GetModName()
        {
            return SpamCards.ModInitials;
        }
    }
}