using System;
using System.Collections.Generic;
using Photon.Pun;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Networking;
using UnityEngine;

namespace SpamCards.Cards
{
    class PlayerLootbag : CustomCard
    {
        private int[] indices = Array.Empty<int>();

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            if (PhotonNetwork.IsMasterClient && indices.Length == 0) //dont randomise again if indices isnt empty, this ensures boosts persist if card is re-added
            {
                var indexList = new List<int> { 0, 1, 2, 3, 4 };
                var rng = new System.Random();
                int index1 = rng.Next(0, indexList.Count - 1);
                int index2 = rng.Next(0, indexList.Count - 2);

                indexList.RemoveAt(index1);
                indexList.RemoveAt(index2);

                NetworkingManager.RPC(typeof(PlayerLootbag), nameof(RPCA_SetIndices), indexList.ToArray());
            }
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            void hp() { player.data.maxHealth += 50; }
            void regen() { health.regeneration += 3; }
            void movementSpeed() { characterStats.movementSpeed *= 1.25f; }
            void size() { characterStats.sizeMultiplier *= 0.9f; }
            void jumps() { data.jumps += 1; }

            List<Action> actions = new List<Action> { hp, regen, movementSpeed, size, jumps };

            for (var i = 0; i < indices.Length; i++)
            {
                actions[indices[i]].Invoke();
            }
        }

        [UnboundRPC]
        private void RPCA_SetIndices(int[] indices)
        {
            UnityEngine.Debug.Log("\nRPC called\n");
            this.indices = indices;
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Player Lootbag";
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
                    stat = "HP",
                    amount = "+50",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Regen",
                    amount = "+3 HP/s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Movement speed",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Size",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Jump",
                    amount = "+1",
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