using System;
using System.Collections.Generic;
using System.Linq;

namespace SubtitlesConverter.Common
{
    public static class EnumerableExtremeValues
    {
        public static T WithMinimum<T, TKey>(
            this IEnumerable<T> sequence, Func<T, TKey> selector)
            where TKey : IComparable<TKey> =>
            sequence.WithExtreme(selector, LessOrEqual);

        public static T WithMaximum<T, TKey>(
            this IEnumerable<T> sequence, Func<T, TKey> selector)
            where TKey : IComparable<TKey> =>
            sequence.WithExtreme(selector, GreaterOrEqual);

        public static IEnumerable<T> WithMinimumOrEmpty<T, TKey>(
            this IEnumerable<T> sequence, Func<T, TKey> selector)
            where TKey : IComparable<TKey> =>
            sequence.WithExtremeOrEmpty(selector, LessOrEqual);

        public static IEnumerable<T> WithMaximumOrEmpty<T, TKey>(
            this IEnumerable<T> sequence, Func<T, TKey> selector)
            where TKey : IComparable<TKey> =>
            sequence.WithExtremeOrEmpty(selector, GreaterOrEqual);

        private static T WithExtreme<T, TKey>(
            this IEnumerable<T> sequence,
            Func<T, TKey> selector,
            Func<TKey, TKey, bool> isBetter)
            where TKey : IComparable<TKey> =>
            sequence.WithExtremeOrEmpty(selector, isBetter).First();

        private static IEnumerable<T> WithExtremeOrEmpty<T, TKey>(
            this IEnumerable<T> sequence, Func<T, TKey> selector,
            Func<TKey, TKey, bool> isBetter) 
            where TKey : IComparable<TKey> =>
            sequence
                .Select(obj => (element: obj, key: selector(obj)))
                .Aggregate(
                    Enumerable.Empty<(T element, TKey key)>(),
                    (optionalBest, cur) => optionalBest.Where(
                        best => isBetter(best.key, cur.key)).DefaultIfEmpty(cur))
                .Select(best => best.element);

        private static bool LessOrEqual<TKey>(TKey a, TKey b)
            where TKey : IComparable<TKey> =>
            a.CompareTo(b) <= 0;

        private static bool GreaterOrEqual<TKey>(TKey a, TKey b)
            where TKey : IComparable<TKey> =>
            a.CompareTo(b) >= 0;
    }
}
