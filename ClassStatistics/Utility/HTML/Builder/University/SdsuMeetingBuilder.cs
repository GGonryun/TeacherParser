using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Class;
using HtmlAgilityPack;
using Utility;
namespace Utility.HTML
{
    public class SdsuMeetingBuilder : Builder
    {
        HtmlNode node;
        Period period;
        
        public SdsuMeetingBuilder(Semester semester, int year, HtmlNode node)
        {
            this.node = node;
            period = new Period(semester, year);
        }

        public override Period BuildPeriod()
        {
            return period;
        }

        public override Course BuildCourse()
        {
            string title = NodeInnerText("sectionFieldTitle");
            string code = NodeInnerText("sectionFieldCourse");
            return new Course(title, code);
        }

        public override Day BuildDay()
        {
            string code = NodeInnerText("sectionFieldDay");
            return new Day(code);
        }

        public override Format BuildFormat()
        {
            string format = NodeInnerText("sectionFieldType");
            return format.ParseEnum<Format>();
        }

        public override string BuildInstructor()
        {
            string instructor = NodeInnerText("sectionFieldInstructor");
            return instructor;
        }

        public override Location BuildLocation()
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

        public override int BuildSchedule()
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

        public override int BuildSection()
        {
            return Convert.ToInt32(NodeInnerText("sectionFieldSec"));
        }

        public override Time BuildTime()
        {
            return new Time(NodeInnerText("sectionFieldTime"));
        }

        public override float BuildUnits()
        {
            return float.Parse(NodeInnerText("sectionFieldUnits"));
        }

        public override Meeting GetResult()
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
