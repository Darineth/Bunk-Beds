using HarmonyLib;
using RimWorld;

namespace BunkBeds
{
    [HarmonyPatch(typeof(Building_Bed), "GetSleepingSlotPos")]
    public static class Building_Bed_GetSleepingSlotPos_Patch
    {
        // Look the bed up in the spawned-bunk-bed index rather than GetComp: for every ordinary bed
        // GetComp<CompBunkBed> misses, and a 1.6 GetComp miss takes GenTypes' global lock and scans
        // every comp. This runs from the parallel pre-draw, so that lock is contended.
        public static void Prefix(Building_Bed __instance, out bool __state)
        {
            __state = CompBunkBed.bunkBeds.TryGetValue(__instance.thingIDNumber, out var comp);
            if (__state)
                BedUtility_GetSleepingSlotsCount_Patch.bunkBedComp = comp;
        }

        public static void Postfix(bool __state)
        {
            if (__state)
                BedUtility_GetSleepingSlotsCount_Patch.bunkBedComp = null;
        }
    }
}
