using UnityEngine;

namespace SpamCards
{
    internal class Assets
    {
        private static readonly AssetBundle Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cardart", typeof(SpamCards).Assembly);

        //public static GameObject A_Flashbang = Bundle.LoadAsset<GameObject>("A_Flashbang");
    }
}