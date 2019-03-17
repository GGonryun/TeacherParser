using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Class;
using HtmlAgilityPack;
using Utility.HTML;

namespace Utility
{
    public class SdsuMeetingBuilder : IBuilder<Meeting>
    {
        HtmlNode node;
        Period period;
        
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
            return new Course(title, code);
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
            return instructor;
        }

        public Location BuildLocation()
        {
            HtmlNode  n = Parser.FindChildByClass(node, "sectionFieldSeats");
            string ratio = n.SelectSingleNode("./text()[normalize-space()]").InnerText.ToString().Trim();
            int waitlist;
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
            try
            {
                string scheduleAsString = NodeInnerText("sectionFieldSched");
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
            return Convert.ToInt32(NodeInnerText("sectionFieldSec"));
        }

        public Time BuildTime()
        {
            return new Time(NodeInnerText("sectionFieldTime"));
        }

        public float BuildUnits()
        {
            return float.Parse(NodeInnerText("sectionFieldUnits"));
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
