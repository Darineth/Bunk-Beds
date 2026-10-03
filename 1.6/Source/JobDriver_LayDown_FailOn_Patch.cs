using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Reflection;
using Verse;

namespace BunkBeds
{
    [HarmonyPatch]
    public static class JobDriver_LayDown_FailOn_Patch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(JobDriver_LayDown).GetMethods(AccessTools.all).FirstOrDefault(x => x.Name.Contains("<MakeNewToils>b__16_0"));
        }

        // The GotoBed fail condition is "downed, can't crawl, and not inside the bed's rect". Bunk bed
        // occupants all share the bed's cell, so the condition never applies to them. Override the
        // result instead of moving the pawn onto the bed cell and back every tick, which re-registered
        // it in the thing, cover, gas and region grids twice per tick for every pawn walking to bed.
        public static void Postfix(JobDriver_LayDown __instance, ref bool __result)
        {
            if (__result && __instance.Bed.IsBunkBed())
            {
                __result = false;
            }
        }
    }
}
