using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BunkBeds
{
    [HotSwappable]
    [HarmonyPatch(typeof(PawnRenderer), "GetBodyPos")]
    public static class PawnRenderer_GetBodyPos_Patch
    {
        public static void Postfix(PawnRenderer __instance, PawnPosture posture, ref Vector3 __result)
        {
            // Vanilla returns early for standing pawns too; skip CurrentBed's own posture check.
            if (posture == PawnPosture.Standing)
            {
                return;
            }
            if (__instance.pawn.CurrentBed(out var slotInd).IsBunkBed(out var bunkBed) && slotInd.HasValue)
            {
                __result.y = bunkBed.parent.DrawPos.y + slotInd.Value;
                __result = bunkBed.GetDrawOffsetForPawns(slotInd.Value, __result);
            }
        }
    }
}
