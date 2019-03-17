using System;
using System.Collections.Generic;
using Class;
using Utility.HTML;
using HtmlAgilityPack;

namespace ClassStatistics
{

    class Program
    {

        static void Main(string[] args)
        {
            Fetcher fetcher = new Fetcher(Source.Web, "https://sunspot.sdsu.edu/schedule/search?mode=search", "&period=20192&abbrev=CS");
            string pattern = "//*[contains(concat(' ', @class, ' '), ' sectionMeeting ')]";
            Parser parser = new Parser(fetcher.Document, pattern);
            List<Meeting> meetings = new List<Meeting>();

            foreach(var nodes in parser.Nodes)
            {
                Builder b = new SdsuMeetingBuilder((Semester)2, 2019, nodes);
                Meeting m = b.GetResult();
                meetings.Add(m);
            }

            foreach(var meeting in meetings)
            {
                Console.WriteLine(meeting.ToString());
            }
            
        }
    }

   
}
