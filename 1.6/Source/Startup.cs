using System;
using System.Runtime.CompilerServices;
using Verse;

namespace BunkBeds
{
    [StaticConstructorOnStartup]
    public static class Utils
    {
        // No memoisation: these are called from the parallel pre-draw, where a cache split across two
        // static fields can be read half-updated (another bed's comp), and every thread writing the
        // same statics bounces the cache line between cores. An int-keyed lookup is cheap enough.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBunkBed(this ThingWithComps bed)
        {
            return bed != null && CompBunkBed.bunkBeds.ContainsKey(bed.thingIDNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBunkBed(this ThingWithComps bed, out CompBunkBed comp)
        {
            if (bed != null && CompBunkBed.bunkBeds.TryGetValue(bed.thingIDNumber, out comp))
            {
                return true;
            }
            comp = null;
            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class HotSwappableAttribute : Attribute
    {
    }
}
