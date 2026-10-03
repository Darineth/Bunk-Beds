using HarmonyLib;
using UnityEngine;
using Verse;

namespace BunkBeds
{
    public class BunkBedsMod : Mod
    {
        public static BunkBedsSettings settings;
        public BunkBedsMod(ModContentPack pack) : base(pack)
        {
            new Harmony("BunkBedsMod").PatchAll();
            // Vanilla Gravship Expanded bundles its own BunkBeds.dll with the same assembly identity, and
            // only one copy is loaded. Log which file won so it is clear which build is running.
            Log.Message("[BunkBeds] perf fork build loaded from " + typeof(BunkBedsMod).Assembly.Location);
            settings = GetSettings<BunkBedsSettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            if (ModLister.AllModsActiveNoSuffix(["Darknote.BunkBeds"]))
            {
                return "Bunk Beds";
            }
            return "";
        }
    }

}
