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
        HtmlNodeCollection nodes;


        public SdsuMeetingBuilder(HtmlNodeCollection nodes)
        {
            this.nodes = nodes;
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
            throw new NotImplementedException();
        }

        public override Location BuildLocation()
        {
            throw new NotImplementedException();
        }

        public override Period BuildPeriod()
        {
            throw new NotImplementedException();
        }

        public override int BuildSchedule()
        {
            throw new NotImplementedException();
        }

        public override int BuildSection()
        {
            throw new NotImplementedException();
        }

        public override Time BuildTime()
        {
            throw new NotImplementedException();
        }

        public override float BuildUnits()
        {
            throw new NotImplementedException();
        }

        public override Meeting GetResult()
        {
            throw new NotImplementedException();
        }

        private string NodeInnerText(string classAttribute)
        {
            HtmlNode node = Parser.FindChildByClass(nodes, classAttribute);
            return node.InnerText.Trim();
        }

      
    }
}
