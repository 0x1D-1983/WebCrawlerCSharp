﻿using System;
using System.Collections.Generic;
using System.Linq;
using static WebCrawlerCsharp.MemoizeExtensions;
using static WebCrawlerCsharp.WebCrawler;

namespace WebCrawlerCsharp
{
    class Program
    {
        // static Func<string, IEnumerable<string>> MemoizedCrawler = Memoize<string, IEnumerable<string>>(Crawler);
        static Func<string, IEnumerable<string>> ThreadSafeMemoizedCrawler = ThreadSafeMemoized<string, IEnumerable<string>>(Crawler);

        static void Main(string[] args)
        {
            var links = new string[] { "http://www.google.com", "http://www.microsoft.com" };

            var titles = from l in links.AsParallel()
            from t in ThreadSafeMemoizedCrawler(l)
            select t;

            foreach(var title in titles)
            {
                Console.WriteLine($"Page title is: {title}");
            }

            Console.ReadLine();
        }
    }
}
