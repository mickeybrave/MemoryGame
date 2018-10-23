using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.Infra
{
    public static class Extensions
    {
        public static int[] FindAllIndexOf<T>(this T[] a, Predicate<T> match)
        {
            T[] subArray = Array.FindAll<T>(a, match);
            return (from T item in subArray select Array.IndexOf(a, item)).ToArray();
        }
    }
}
