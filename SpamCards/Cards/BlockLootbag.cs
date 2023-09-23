using System;
using System.Collections.Generic;
using Photon.Pun;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Networking;
using UnityEngine;

namespace SpamCards.Cards
{
    class BlockLootbag : CustomCard
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
            Action blocks = () => { block.additionalBlocks += 1; };
            Action hpOnBlock = () => { block.healing += 20; };
            Action cooldown = () => { block.cdAdd -= 0.2f; };

            List<Action> actions = new List<Action> { blocks, hpOnBlock, cooldown };

            if (PhotonNetwork.IsMasterClient)
            {
                var rng = new System.Random();
                int index1 = rng.Next(0, actions.Count - 1);
                int index2 = rng.Next(0, actions.Count - 2);

                actions.RemoveAt(index1);
                actions.RemoveAt(index2);

                NetworkingManager.RPC(typeof(BlockLootbag), nameof(InvokeActions), actions);
            }
        }

        [UnboundRPC]
        private static void InvokeActions(List<Action> actions)
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Block Lootbag";
        }

        protected override string GetDescription()
        {
            return "Randomly gain two of the following boosts:";
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
                    stat = "Blocks",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "HP on block",
                    amount = "20",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block cooldown",
                    amount = "-0.2s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
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