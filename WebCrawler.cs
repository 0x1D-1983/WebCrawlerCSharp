using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WebCrawlerCsharp
{
    public static class WebCrawler
    {
        public static Regex linkRegex = new Regex(@"(?<=href=('|""))https?://.*?(?=\1)");
        public static Regex titleRegex = new Regex("<title>(?<title>.*?)<\\/title>", RegexOptions.Compiled);

        public static IEnumerable<string> Crawler(string url)
        {
            var content = GetContent(url);
            yield return GetTitle(content);


            foreach (var link in GetLinks(content))
            {
                var morecontent = GetContent(link);
                yield return GetTitle(morecontent);
            }
        }

        public static string GetContent(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    var content = client.DownloadString(url);
                    return content;
                }
                catch
                {
                    Console.WriteLine($"Error fetching Url: {url}");
                    return "";
                }
            }
        }

        public static IEnumerable<string> GetLinks(string content)
        {
            foreach (var link in linkRegex.Matches(content))
            {
                yield return link.ToString();
            }
        }

        public static string GetTitle(string content)
        {
            if (titleRegex.IsMatch(content))
            {
                return titleRegex.Match(content).Value;
            }

            return null;
        }
    }
}
