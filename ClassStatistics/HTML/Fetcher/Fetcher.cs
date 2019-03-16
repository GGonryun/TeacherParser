using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

public enum Source { Web, Local }

namespace ClassStatistics
{
    public class Fetcher
    {
        public string Location { get; private set; }
        public string Arguments { get; private set; }
        public Source Source { get; private set; }

        HtmlDocument document;
        public HtmlDocument Document
        {
            get
            {
                if (document == null)
                {
                    Fetch();
                }
                return document;
            }
        }

        public Fetcher(Source source, string location, string arguments = "")
        {
            this.Location = location;
            this.Arguments = arguments;
            this.Source = source;
        }

        void Fetch()
        {
            switch (Source)
            {
                case Source.Web:
                    HtmlWeb web = new HtmlWeb();
                    document = web.Load(Location + Arguments);
                    break;
                case Source.Local:
                    document = new HtmlDocument();
                    document.Load(Location);
                    break;
            }
        }

    }
}
