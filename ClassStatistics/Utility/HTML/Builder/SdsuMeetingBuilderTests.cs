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
        public void BuilderCreatesCourseProperly()
        {
            Course course = builder.BuildCourse();
            Assert.Equals("CS-320", course);
        }

    }

}
