using System;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI;

namespace SpamCards.MonoBehaviours
{
    public class FlashbangMono : MonoBehaviour
    {
        private Player player;
        private Block block;
        private Action<BlockTrigger.BlockTriggerType> flashbang;
        private Action<BlockTrigger.BlockTriggerType> previous;
        private readonly float range = 25f;

        private void Awake()
        {
            player = GetComponent<Player>();
            block = GetComponent<Block>();

            previous = block.BlockAction;

            flashbang = new Action<BlockTrigger.BlockTriggerType>(GetDoBlockAction(player, block).Invoke);
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(block.BlockAction, flashbang);
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        public void OnDestroy()
        {
            block.BlockAction = previous;
        }

        public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
        {
            return delegate(BlockTrigger.BlockTriggerType trigger)
            {
                var targets = PlayerManager.instance.players;

                foreach (Player p in targets)
                {
                    if (p.playerID == player.playerID) continue; //dont flashbang yourself lol

                    if (Vector2.Distance(block.transform.position, p.transform.position) < range)
                    {
                        p.gameObject.GetOrAddComponent<FlashbangEffect>();
                    }
                }
            };
        }
    }

    public class FlashbangEffect : ReversibleEffect
    {
        private readonly float duration = 2f;
        private float startTime;
        private GameObject fbOverlay;
        private GameObject fbCanvas;

        public override void OnAwake()
        {
            player = GetComponent<Player>();

            fbCanvas = new GameObject();
            fbCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            fbCanvas.layer = LayerMask.GetMask("Default Post");

            fbOverlay = new GameObject();
            fbOverlay.transform.SetParent(fbCanvas.transform, false);
            fbOverlay.transform.position = new Vector3(1, 1);
            fbOverlay.transform.localScale =
                Camera.main.ViewportToScreenPoint(fbOverlay.transform.localScale); //fill screen
        }

        public override void OnStart()
        {
            startTime = Time.time;
        }

        public override void OnOnDestroy()
        {
            Destroy(fbOverlay);
            Destroy(fbCanvas);
        }

        public override void OnUpdate()
        {
            if (Time.time > startTime + duration)
            {
                Destroy(this);
            }

            float timePassed = Time.time - startTime;

            float alpha = Mathf.Lerp(2, 0, timePassed);

            fbOverlay.GetOrAddComponent<Image>().SetAlpha(alpha);
        }
    }
}