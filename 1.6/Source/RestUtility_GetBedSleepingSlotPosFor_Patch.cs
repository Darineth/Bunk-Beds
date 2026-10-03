using HarmonyLib;
using RimWorld;
using Verse;

namespace BunkBeds
{
    [HarmonyPatch(typeof(RestUtility), "GetBedSleepingSlotPosFor")]
    public static class RestUtility_GetBedSleepingSlotPosFor_Patch
    {
        // Every bunk bed occupant sleeps on the bed's own cell. Answer before vanilla runs its slot
        // search, which loops GetCurOccupant over every slot and can log a spurious error.
        public static bool Prefix(ref IntVec3 __result, Building_Bed bed)
        {
            if (bed.IsBunkBed())
            {
                __result = bed.Position;
                return false;
            }
            return true;
        }
    }

}
