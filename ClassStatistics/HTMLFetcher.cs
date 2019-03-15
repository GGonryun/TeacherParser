using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassStatistics
{
    public class HTMLFetcher
    {
        public string BaseUri { get; private set; }
        public string Arguments { get; private set; }
        public HtmlDocument Content { get; private set; }

        public HTMLFetcher(string uri, string arguments)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(uri + arguments);
        }
    }
}
