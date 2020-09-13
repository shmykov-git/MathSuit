using System;
using System.Collections.Generic;
using System.Linq;

namespace MathSuit.Extensions
{
    public static class TupleExtensions
    {
        public static string ToStr(this (int, int) ij)
        {
            return $"{ij.Item1};{ij.Item2}";
        }

        public static IEnumerable<int> Range(this int ii)
        {
            return Enumerable.Range(0, ii);
        }

        public static IEnumerable<T> Range<T>(this int ii, Func<int, T> fn)
        {
            return Enumerable.Select(Enumerable.Range(0, ii), fn);
        }

        public static IEnumerable<T> Range<T>(this (int, int) ij, Func<int, int, T> fn)
        {
            return Enumerable.Range(0, ij.Item1).SelectMany(i => Enumerable.Range(0, ij.Item2).Select(j => fn(i, j)));
        }

        public static IEnumerable<T> SelectValues<T>(this (int, int)[] ij, Func<int, int, T> fn)
        {
            return ij.Select(v => fn(v.Item1, v.Item2));
        }

        public static void ForEachRange(this (int, int) ij, Action<int, int> Act)
        {
            ij.Range((i, j) =>
            {
                Act(i, j);
                return 0;
            }).ToArray();
        }

        public static IEnumerable<T> Range<T>(this (int, int, int) ijk, Func<int, int, int, T> fn)
        {
            return Enumerable.Range(0, ijk.Item1)
                .SelectMany(i => Enumerable.Range(0, ijk.Item2)
                    .SelectMany(j => Enumerable.Range(0, ijk.Item3).Select(k => fn(i, j, k))));
        }

        public static (T, T) Reverse<T>(this (T, T) pair)
        {
            return (pair.Item2, pair.Item1);
        }
    }
}