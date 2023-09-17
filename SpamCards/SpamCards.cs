using BepInEx;
using HarmonyLib;
using SpamCards.Cards;
using UnboundLib.Cards;

namespace SpamCards
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("pykess.rounds.plugins.moddingutils")]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class SpamCards : BaseUnityPlugin
    {
        private const string ModId = "turtle.mods.spamcards";
        private const string ModName = "Spam Cardpack";
        public const string Version = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "SCP";

        public static SpamCards Instance { get; private set; } = default!;

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            Instance = this;
            CustomCard.BuildCard<SmallTweak>();
        }
    }
}