using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WebCrawlerCsharp
{
    public class MemoizeExtensions
    {
        public static Func<T, R> Memoize<T, R>(Func<T, R> func) where T : IComparable
        {
            var cache = new Dictionary<T, R>();

            return x =>
            {
                return cache.ContainsKey(x) ? cache[x] : cache[x] = func(x);
            };
        }

        public static Func<T, R> ThreadSafeMemoize<T, R>(Func<T, R> func) where T : IComparable
        {
            var cache = new ConcurrentDictionary<T, R>();

            return arg => cache.GetOrAdd(arg, x => func(x));
        }

        public static Func<T, R> ThreadSafeLazyMemoize<T, R>(Func<T, R> func) where T : IComparable
        {
            var cache = new ConcurrentDictionary<T, Lazy<R>>();
            return arg => cache.GetOrAdd(arg, x => new Lazy<R>(() => func(x))).Value;
        }
    }
}