using System;
using System.Collections.Generic;
using Photon.Pun;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Networking;
using UnityEngine;

namespace SpamCards.Cards
{
    class GunLootbag : CustomCard
    {
        private int[] indices;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`if (PhotonNetwork.IsMasterClient && indices.Length == 0) //dont randomise again if indices isnt empty, this ensures boosts persist if card is re-added
            {
                var indexList = new List<int> { 0, 1, 2, 3, 4 };
                var rng = new System.Random();
                int index1 = rng.Next(0, indexList.Count - 1);
                int index2 = rng.Next(0, indexList.Count - 2);

                indexList.RemoveAt(index1);
                indexList.RemoveAt(index2);

                if (PhotonNetwork.OfflineMode)
                {
                    RPCA_SetIndices(indexList.ToArray());
                }
                else
                {
                    NetworkingManager.RPC(typeof(GunLootbag), nameof(RPCA_SetIndices), indexList.ToArray());
                }
            }
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            void damage() { gun.damage += (10f / 55f); }
            void reloadTime() { gunAmmo.reloadTimeAdd -= 0.5f; }
            void ammo() { gunAmmo.maxAmmo += 3; }
            void attackSpeed() { gun.attackSpeed *= 0.8f; }
            void projectileSpeed() { gun.projectielSimulatonSpeed += 0.2f; }

            List<Action> actions = new List<Action> { damage, reloadTime, ammo, attackSpeed, projectileSpeed };

            for (var i = 0; i < indices.Length; i++)
            {
                actions[indices[i]].Invoke();
            }
        }

        [UnboundRPC]
        private void RPCA_SetIndices(int[] indices)
        {
            this.indices = indices;
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Gun Lootbag";
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