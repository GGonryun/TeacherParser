using System;
using System.Collections.Generic;
using System.Text;
using Class;
using Utility;
using NUnit.Framework;
using System.Linq;
using Filtering.Specifications;

namespace Filtering
{
    class MeetingFilterTests
    {
        List<Meeting> meetings;
        IFilter<Meeting> filter;

        [SetUp]
        public void SetUp()
        {
            filter = new MeetingFilter();
            meetings = new List<Meeting>()
            {
                new Meeting(
                    new Period(Semester.Spring, 2014),
                    new Course("COMPUTATIONAL THINKING", "CS", 101),
                    1,
                    30853,
                    3.0f,
                    Format.Lecture,
                    new Time("0930-1045"),
                    new Day("TTH"),
                    new Location(24, 6, "GMCS", 325),
                    "L. Beck"
                ),
                new Meeting(
                    new Period(Semester.Spring, 2014),
                    new Course("INTRO COMPUTER PROGRAM", "CS", 107),
                    1,
                    20896,
                    3.0f,
                    Format.Lecture,
                    new Time("1400-1515"),
                    new Day("MW"),
                    new Location(-12, 80, "GMCS", 214),
                    "S. Lindeneau"
                ),
                new Meeting(
                    new Period(Semester.Spring, 2014),
                    new Course("INTRO COMPUTER PROGRAM", "CS", 107),
                    2,
                    20897,
                    3.0f,
                    Format.Lecture,
                    new Time("1230-1345"),
                    new Day("TTH"),
                    new Location(80, 4, "GMCS", 325),
                    "L. Riggins"
                ),
                new Meeting(
                    new Period(Semester.Spring, 2014),
                    new Course("MACHINE ORG&ASSEMBLY LANG", "CS", 237),
                    1,
                    20900,
                    3.0f,
                    Format.Lecture,
                    new Time("1230-1345"),
                    new Day("TTH"),
                    new Location(12, 110, "COM", 207),
                    "L. Riggins"
                )
            };

        }


        [Test]
        public void InstructorNameSpecification()
        {
            IEnumerable<Meeting> teachers = filter.Filter(meetings, new InstructorNameSpecification("L. Beck"));
            Assert.AreEqual(1, teachers.Count());
        }

        [Test]
        public void CourseSubjectSpecification()
        {
            IEnumerable<Meeting> subject = filter.Filter(meetings, new CourseSubjectSpecification("CS"));
            Assert.AreEqual(4, subject.Count());
        }

        [Test]
        public void CourseNumberSpecification()
        {
            IEnumerable<Meeting> number = filter.Filter(meetings, new CourseNumberSpecification(107));
            Assert.AreEqual(2, number.Count());
        }

        [Test]
        public void CourseSpecification()
        {
            ISpecification<Meeting>[] specs = new ISpecification<Meeting>[]
            {
                new CourseSubjectSpecification("CS"),
                new CourseNumberSpecification(107),
                new InstructorNameSpecification("L. Riggins")
            };

            IEnumerable<Meeting> course = filter.Filter(meetings, specs);
            Assert.AreEqual(1, course.Count());
        }

        [Test]
        public void CourseLevelSpecificationEasier()
        {
            ISpecification<Meeting> specs = new CourseLevelSpecification(Level.Sophomore, (x, y) => x < y);
            IEnumerable<Meeting> course = filter.Filter(meetings, specs);
            Assert.AreEqual(3, course.Count());
        }

        [Test] 
        public void CourseLevelSpecificationHarder()
        {
            ISpecification<Meeting> specs = new CourseLevelSpecification(Level.Sophomore, (x, y) => x >= y);
            IEnumerable<Meeting> course = filter.Filter(meetings, specs);
            Assert.AreEqual(1, course.Count());
        }

    }
}
