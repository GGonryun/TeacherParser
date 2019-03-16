using System;
using System.Collections.Generic;
using System.Text;
using Utility.HTML;
using NUnit.Framework;
using Utility;
using Class;

namespace Utility.HTML
{
    class SdsuMeetingBuilderTests
    {
        Fetcher fetcher;
        Parser parser;
        SdsuMeetingBuilder builder;

        [SetUp]
        public void SetUp()
        {
            fetcher = new Fetcher(Source.Local, CONSTANTS.SdsuHTMLPath, "");

            string pattern = "//div[contains(concat(' ', @class, ' '), ' sectionMeeting ')]";

            parser = new Parser(fetcher.Document, pattern);

            builder = new SdsuMeetingBuilder(parser.Nodes);
        }

        [Test]
        public void IsNotEmpty()
        {
            Assert.IsNotNull(parser.Nodes);
            Assert.IsTrue(parser.Nodes.Count > 0);
        }

        [Test]
        public void BuildCourseSuccessfully()
        {
            Course course = builder.BuildCourse();
            Assert.AreEqual("CS-320", course.Code);
        }
        [Test]
        public void BuildDaySuccessfully()
        {
            Day day = builder.BuildDay();
            Assert.AreEqual(2, day.Days.Count);
        }

        [Test]
        public void BuildFormatSuccessfully()
        {
            Format format = builder.BuildFormat();
            Assert.AreEqual(Format.Lecture, format);
        }
    }

}
