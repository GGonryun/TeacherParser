using System;
using System.Collections.Generic;
using System.Text;
using Utility.HTML;
using NUnit.Framework;
using Utility;
using Class;

namespace Utility
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
            parser = new Parser(fetcher.Document, Parser.SelectByClassPattern("sectionMeeting"));
            builder = new SdsuMeetingBuilder(Semester.Fall, 2014, parser.Nodes[0]);
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

        [Test]
        public void BuildInstructorSuccessfully()
        {
            string instructor = builder.BuildInstructor();
            Assert.AreEqual("S. HEALEY", instructor);
        }

        [Test]
        public void BuildLocationSuccessfully()
        {
            Location location = builder.BuildLocation();
            Assert.AreEqual("EBA-343", location.Room.Code);
            Assert.AreEqual(5, location.Waitlist);
            Assert.AreEqual(.98666f, location.Ratio, .0005f);
        }

        [Test] 
        public void BuildScheduleSuccessfully()
        {
            int schedule = builder.BuildSchedule();
            Assert.AreEqual(21028, schedule);
        }

        [Test]
        public void BuildSectionSuccessfully()
        {
            int section = builder.BuildSection();
            Assert.AreEqual(1, section);
        }

        [Test]
        public void BuildTimeSuccessfully()
        {
            Time time = builder.BuildTime();
            Assert.AreEqual("1400-1515", time.ToString());
        }

        [Test]
        public void BuildUnitsSuccessfully()
        {
            float unit = builder.BuildUnits();
            Assert.AreEqual(3.0f, unit, 0.00005f);
        }
    }
}
