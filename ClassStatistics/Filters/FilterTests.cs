using System;
using System.Collections.Generic;
using System.Text;
using Class;
using Utility;
using NUnit.Framework;
using System.Linq;

namespace Filtering
{
    class FilterTests
    {
        List<Meeting> meetings;
        [SetUp]
        public void SetUp()
        {
            meetings = new List<Meeting>()
            {
                new Meeting(
                    new Course("CS",101),
                    1,
                    30853,
                    "COMPUTATIONAL THINKING",
                    3.0f,
                    Format.Lecture,
                    new Time("0930-1045"),
                    new Day("TTH"),
                    new Location(24, 6, "GMCS",325),
                    "L. Beck"
                ),
                new Meeting(
                    new Course("CS",107),
                    1,
                    20896,
                    "INTRO COMPUTER PROGRAM",
                    3.0f,
                    Format.Lecture,
                    new Time("1400-1515"),
                    new Day("MW"),
                    new Location(-12, 80, "GMCS",214),
                    "S. Lindeneau"
                ),
                new Meeting(
                    new Course("CS",107),
                    2,
                    20897,
                    "INTRO COMPUTER PROGRAM",
                    3.0f,
                    Format.Lecture,
                    new Time("1230-1345"),
                    new Day("TTH"),
                    new Location(80, 4, "GMCS",325),
                    "L. Riggins"
                )
            };
        }


        [Test]
        public void SingleFilter()
        {
            var filter = new MeetingFilter();
            IEnumerable<Meeting> teachers = filter.Filter(meetings, new InstructorNameSpecification("L. Beck"));
            Assert.AreEqual(1, teachers.Count());
        }

    }
}
