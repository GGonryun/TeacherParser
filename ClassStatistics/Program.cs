using System;
using System.Collections.Generic;
using Class;
using Utility.HTML;

namespace ClassStatistics
{

    class Program
    {

        static void Main(string[] args)
        {
            Fetcher fetcher = new Fetcher(Source.Web, "https://sunspot.sdsu.edu/schedule/search?mode=search", "&period=20192");
            string pattern = "//div[contains(concat(' ', @class, ' '), ' sectionMeeting ')]";
            Parser parser = new Parser(fetcher.Document, pattern);

            
        }
    }

   
}
