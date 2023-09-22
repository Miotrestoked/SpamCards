using System;
using System.Collections.Generic;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;

namespace SpamCards.Cards
{
    class Lootbag : CustomCard
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
            Action damage = () => { gun.damage += (10f / 55f); };
            Action reloadTime = () => { gun.reloadTimeAdd -= 0.5f; };
            Action ammo = () => { gunAmmo.maxAmmo += 3; };
            Action attackSpeed = () => { gun.attackSpeedMultiplier += .2f; };
            Action projectileSpeed = () => { gun.projectielSimulatonSpeed += 0.2f; };

            Action[] actions = { damage, reloadTime, ammo, attackSpeed, projectileSpeed };

            actions.Shuffle(); //shuffle so three random actions can be picked

            UnityEngine.Debug.Log($"Before actions are invoked stats are: \n" +
                                  $"Damage: {gun.damage}\n" +
                                  $"Reload time add: {gun.reloadTimeAdd}\n" +
                                  $"Ammo: {gunAmmo.maxAmmo}\n" +
                                  $"Attack speed multiplier: {gun.attackSpeedMultiplier}\n" +
                                  $"Projectile speed: {gun.projectielSimulatonSpeed}");

            for (var i = 0; i < 3; i++)
            {
                actions[i].Invoke();
            }

            UnityEngine.Debug.Log($"After actions are invoked stats are: \n" +
                                  $"Damage: {gun.damage}\n" +
                                  $"Reload time add: {gun.reloadTimeAdd}\n" +
                                  $"Ammo: {gunAmmo.maxAmmo}\n" +
                                  $"Attack speed multiplier: {gun.attackSpeedMultiplier}\n" +
                                  $"Projectile speed: {gun.projectielSimulatonSpeed}");
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Lootbag";
        }

        protected override string GetDescription()
        {
            return "Randomly gain three of the following boosts:";
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
                    stat = "Flat damage",
                    amount = "+10",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload time",
                    amount = "-0.5s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectile speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
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