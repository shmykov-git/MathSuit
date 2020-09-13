using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Suit.Extensions;

namespace MathSuit.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> list, T item)
        {
            return list.Concat(new T[] {item});
        }

        public static IEnumerable<int> SelectNum(this int n, int start = 0)
        {
            for (var i = start; i < n; i++)
                yield return i;
        }

        public static void ForEachNum(this int n, Action<int> action)
        {
            Enumerable.Range(0, n).ForEach(action);
        }

        public static void ForEachNum(this int n, int from, Action<int> action)
        {
            Enumerable.Range(from, n).ForEach(action);
        }

        public static void ForEachModify<T>(this T[] list, Action<T> action)
        {
            for(var i=0; i<list.Length; i++)
                action(list[i]);
        }

        public static void ForEachPair<T>(this IEnumerable<T> list, Action<T, T> action)
        {
            list.SelectPair((a, b) =>
                {
                    action(a, b);
                    return true;
                })
                .ToArray();
        }

        public static void ForEachCirclePair<T>(this IEnumerable<T> list, Action<T, T> action)
        {
            list.SelectCirclePair((a, b) =>
                {
                    action(a, b);
                    return true;
                })
                .ToArray();
        }

        public static IEnumerable<TRes> SelectTopMatrixPair<T, TRes>(this T[] a, Func<T, T, TRes> func)
        {
            for (var i = 0; i < a.Length; i++)
            for (var j = i + 1; i < a.Length; j++)
            {
                yield return func(a[i], a[j]);
            }
        }

        public static void ForEachTopMatrixPair<T>(this T[] a, Action<T, T> action)
        {
            a.SelectTopMatrixPair((t1, t2) =>
            {
                action(t1, t2);
                return true;
            });
        }

        public static IEnumerable<TRes> SelectCirclePair<T, TRes>(this IEnumerable<T> list, Func<T, T, TRes> func)
        {
            var i = 0;
            var prevT = default(T);
            var firstT = default(T);
            foreach (var t in list)
            {
                if (i == 0)
                    firstT = t;
                else
                    yield return func(prevT, t);

                prevT = t;
                i++;
            }

            yield return func(prevT, firstT);
        }

        public static void ForEachCircleTriple<T>(this IEnumerable<T> list, Action<T, T, T> action)
        {
            list.SelectCircleTriple((a, b, c) =>
                {
                    action(a, b, c);
                    return true;
                })
                .ToArray();
        }


        public static IEnumerable<TRes> SelectCircleTriple<T, TRes>(this IEnumerable<T> list, Func<T, T, T, TRes> func)
        {
            var i = 0;
            var prevPrevT = default(T);
            var prevT = default(T);
            var firstT = default(T);
            var secondT = default(T);
            foreach (var t in list)
            {
                if (i == 0)
                    firstT = t;
                else if (i == 1)
                    secondT = t;
                else
                    yield return func(prevPrevT, prevT, t);

                prevPrevT = prevT;
                prevT = t;
                i++;
            }

            yield return func(prevPrevT, prevT, firstT);
            yield return func(prevT, firstT, secondT);
        }

        public static IEnumerable<TRes> SelectCircleFarTriple<T, TRes>(this IEnumerable<T> list, Func<T, T, T, TRes> func)
        {
            var i = 0;
            var prevPrevPrevPrevT = default(T);
            var prevPrevPrevT = default(T);
            var prevPrevT = default(T);
            var prevT = default(T);
            var firstT = default(T);
            var secondT = default(T);
            var thirdT = default(T);
            var forthT = default(T);
            foreach (var t in list)
            {
                if (i == 0)
                    firstT = t;
                else if (i == 1)
                    secondT = t;
                else if (i == 2)
                    thirdT = t;
                else if (i == 3)
                    forthT = t;
                else
                    yield return func(prevPrevPrevPrevT, prevPrevT, t);

                prevPrevPrevPrevT = prevPrevPrevT;
                prevPrevPrevT = prevPrevT;
                prevPrevT = prevT;
                prevT = t;
                i++;
            }

            yield return func(prevPrevPrevPrevT, prevPrevT, firstT);
            yield return func(prevPrevPrevT, prevT, secondT);
            yield return func(prevPrevT, firstT, thirdT);
            yield return func(prevT, secondT, forthT);
        }

        public static void ForEachStrightPair<T>(this IEnumerable<T> list, Action<T, T> action)
        {
            list.SelectStrightPair((a, b) =>
                {
                    action(a, b);
                    return true;
                })
                .ToArray();
        }

        public static IEnumerable<TRes> SelectStrightPair<T, TRes>(this IEnumerable<T> list, Func<T, T, TRes> func)
        {
            var i = 0;
            var prevT = default(T);
            var firstT = default(T);
            foreach (var t in list)
            {
                if (i == 0)
                    firstT = t;
                else
                    yield return func(prevT, t);

                prevT = t;
                i++;
            }
        }

        public static IEnumerable<int> ByIndex<T>(this IEnumerable<T> list)
        {
            var i = 0;
            foreach (var t in list)
            {
                yield return i++;
            }
        }

        public static IEnumerable<int> ByIndexWhere<T>(this IEnumerable<T> list, Func<int, T, bool> conditionFn)
        {
            var i = 0;
            foreach (var t in list)
            {
                if (conditionFn(i, t))
                    yield return i;
                i++;
            }
        }

        public static int[] ToIndexArray<T>(this IEnumerable<T> list)
        {
            return list.ByIndex().ToArray();
        }

        public static IEnumerable<TRes> Select<T, TRes>(this IEnumerable list, Func<T, TRes> func)
        {
            foreach (object item in list)
            {
                yield return func((T)item);
            }
        }
    }
}
