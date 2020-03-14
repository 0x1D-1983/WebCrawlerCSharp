using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WebCrawlerCsharp
{
    public class MemoizeExtensions
    {
        public static Func<T, R> Memoize<T, R>(Func<T, R> func) where T:IComparable
        {
            var cache = new Dictionary<T, R>();

            return x =>
            {
                return cache.ContainsKey(x) ? cache[x] : cache[x] = func(x);
            };
        }

        public static Func<T, R> ThreadSafeMemoized<T, R>(Func<T, R> func) where T:IComparable
        {
            var cache = new ConcurrentDictionary<T, R>();

            return x => cache.GetOrAdd(x, func(x));
        }
    }
}