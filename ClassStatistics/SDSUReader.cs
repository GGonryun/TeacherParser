using System;
using System.Collections.Generic;
using System.Text;
using Utility.HTML;
using Class;
using HtmlAgilityPack;

namespace ClassStatistics
{
    public class Reader
    {
        private readonly HtmlNodeCollection _nodes;
        List<Meeting> meetings;

        public Reader(Semester semester, int year, HtmlNodeCollection nodes)
        {
            meetings = new List<Meeting>();
            _nodes = nodes;
        }

        
    }
}
