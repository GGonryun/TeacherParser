using Class;
using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using Utility;
using Utility.HTML;

namespace Builder
{
    public class SdsuMeetingBuilder : IBuilder<Meeting>
    {
        HtmlNode node;
        Period period;
        
        public SdsuMeetingBuilder(Period period, HtmlNode node)
        {
            this.period = period;
            this.node = node;
        }

        public SdsuMeetingBuilder(Semester semester, int year, HtmlNode node)
        {
            this.node = node;
            period = new Period(semester, year);
        }

        public SdsuMeetingBuilder(string yearSemesterCode, HtmlNode node)
        {
            this.node = node;
            
            period = new Period(yearSemesterCode);
        }

        public Period BuildPeriod()
        {
            return period;
        }

        public Course BuildCourse()
        {
            string title = NodeInnerText("sectionFieldTitle");
            string code = NodeInnerText("sectionFieldCourse");
            try
            {
                return new Course(title, code);
            }
            catch
            {
                return null;
            }
        }

        public Day BuildDay()
        {
            string code = NodeInnerText("sectionFieldDay");
            return new Day(code);
        }

        public Format BuildFormat()
        {
            string format = NodeInnerText("sectionFieldType");
            return format.ParseEnum<Format>();
        }

        public string BuildInstructor()
        {
            string instructor = NodeInnerText("sectionFieldInstructor");
            string newer = instructor.Replace(System.Environment.NewLine, "");
            string newest = Regex.Replace(newer, " {2,}", " & ");
            return newest;
        }

        public Location BuildLocation()
        {
            HtmlNode  n = Parser.FindChildByClass(node, "sectionFieldSeats");
            string ratio;
            int waitlist;

            try
            {
                ratio = n.SelectSingleNode("./text()[normalize-space()]").InnerText.ToString().Trim();
            }
            catch
            {
                ratio = "0/0";
            }
            try
            {
                waitlist = Convert.ToInt32((NodeInnerText("sectionFieldSeats").Split(':'))[1]);
            }
            catch
            {
                waitlist = 0;
            }
            string code = NodeInnerText("sectionFieldLocation");
            return new Location(ratio, waitlist, new Room(code));
        }

        public int BuildSchedule()
        {
            int schedule;
            string scheduleAsString = NodeInnerText("sectionFieldSched");
            if(scheduleAsString == "")
            {
                return -1;
            }
            try
            {
                schedule = Convert.ToInt32(scheduleAsString);
            }
            catch
            {
                schedule = 0;
            }
            return schedule;
        }

        public int BuildSection()
        {
            try
            {
                return Convert.ToInt32(NodeInnerText("sectionFieldSec"));

            }
            catch
            {
                return -1;
            }
        }

        public Time BuildTime()
        {
            return new Time(NodeInnerText("sectionFieldTime"));
        }

        public float BuildUnits()
        {
            try
            {
                return float.Parse(NodeInnerText("sectionFieldUnits"));
            }
            catch
            {
                return -1f;
            }
        }

        public Meeting GetResult()
        {
            return new Meeting(
                period,
                BuildCourse(),
                BuildSection(),
                BuildSchedule(),
                BuildUnits(),
                BuildFormat(),
                BuildTime(),
                BuildDay(),
                BuildLocation(),
                BuildInstructor()
            );
        }

        private string NodeInnerText(string classAttribute)
         {
            HtmlNode n = Parser.FindChildByClass(node, classAttribute);
            return n.InnerText.Trim();
        }

      
    }
}
