using System;
using System.Collections.Generic;
using System.Text;
using Utility.HTML;
using Class;
using HtmlAgilityPack;
using Utility;

namespace ClassStatistics
{
    public class SdsuMeetingsBuilder : IBuilder<Meetings>
    {
        private Meetings _meetings;
        private readonly string[] _codes;
        private readonly string _majorAbbreviation;

        public SdsuMeetingsBuilder(string majorAbbreviation, params string[] codes)
        {
            _codes = codes;
            _majorAbbreviation = majorAbbreviation;
        }

        public Meetings GetResult()
        {
            if(_meetings == null)
            {
                _meetings = new Meetings();
                foreach (string code in _codes)
                {
                    Fetcher f = new Fetcher(Source.Web, $@"https://sunspot.sdsu.edu/schedule/search?mode=search&period={code}&abbrev={_majorAbbreviation}");
                    Parser p = new Parser(f.Document, Parser.SelectByClassPattern("sectionMeeting"));

                    foreach(HtmlNode node in p.Nodes)
                    {
                        IBuilder<Meeting> b = new SdsuMeetingBuilder(code, node);
                        Meeting m = b.GetResult();
                        _meetings.Add(m);
                    }
                }
            }
            return _meetings;
        }
    }
}
