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
    class MeetingSpecificationTests
    {
        List<Meeting> meetings;
        IFilter<Meeting> filter;

        [SetUp]
        public void SetUp()
        {
            filter = new MatchAllFilter<Meeting>();
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
                    new Location(24, 13, 0, new Room("GMCS", "325")),
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
                    new Location(80, -12, 0, new Room("GMCS", "214")),
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
                    new Location(80, 4, 0, new Room("GMCS", "325")),
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
                    new Location(110, 12, 0, new Room("COM", "207")),
                    "L. Riggins"
                ),
                new Meeting(
                    new Period(Semester.Spring, 2014),
                    new Course("SPECIAL STUDY", "CS", 299),
                    1,
                    0,
                    1.0f,
                    Format.Discussion,
                    new Time(""),
                    new Day("Arranged"),
                    new Location(5, 0, 0, new Room("")),
                    "L. Beck"
                )
            };

        }


        [Test]
        public void InstructorNameSpecification()
        {
            IEnumerable<Meeting> teachers = filter.Filter(meetings, new InstructorNameSpecification("L. Beck"));
            Assert.AreEqual(2, teachers.Count());
        }

        [Test]
        public void CourseSubjectSpecification()
        {
            IEnumerable<Meeting> subject = filter.Filter(meetings, new CourseSubjectSpecification("CS"));
            Assert.AreEqual(5, subject.Count());
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
            Assert.AreEqual(2, course.Count());
        }

        [Test]
        public void IgnoreCourseSpecification()
        {
            ISpecification<Meeting> specs = new IgnoreCoursesSpecification(new List<Course>() { new Course("INTRO COMPUTER PROGRAM", "CS", 107) });
            IEnumerable<Meeting> courses = filter.Filter(meetings, specs);
            Assert.AreEqual(3, courses.Count());
        }

        [Test]
        public void TimeRangeSpecification()
        {
            ISpecification<Meeting> specs = new TimeRangeSpecification(new NodaTime.LocalTime(0, 30), new Time("1130-1200"));
            IEnumerable<Meeting> courses = filter.Filter(meetings, specs);
            Assert.AreEqual(2, courses.Count());
        }


        [Test]
        public void TimeGreaterThanSpecification()
        {
            Func<NodaTime.LocalTime?, NodaTime.LocalTime, bool> comparator = (x, y) => x > y;

            //Returns all classes that end after 12:45pm.
            ISpecification<Meeting> specs = new TimeSpecification(false, new NodaTime.LocalTime(12, 45), comparator);
            IEnumerable<Meeting> courses = filter.Filter(meetings, specs);
            Assert.AreEqual(3, courses.Count());

            //Returns all classes that start after 12:45pm.
            ISpecification<Meeting> specs2 = new TimeSpecification(true, new NodaTime.LocalTime(12, 45), comparator);
            IEnumerable<Meeting> courses2 = filter.Filter(meetings, specs2);
            Assert.AreEqual(1, courses2.Count());
        }

        [Test]
        public void TimeLessThanSpecification()
        {
            Func<NodaTime.LocalTime?, NodaTime.LocalTime, bool> comparator = (x, y) => x < y;

            //Returns all classes that end before 12:45pm.
            ISpecification<Meeting> specs = new TimeSpecification(false, new NodaTime.LocalTime(12, 45), comparator);
            IEnumerable<Meeting> courses = filter.Filter(meetings, specs);
            Assert.AreEqual(1, courses.Count());

            //Returns all classes that start before 12:45pm.
            ISpecification<Meeting> specs2 = new TimeSpecification(true, new NodaTime.LocalTime(12, 45), comparator);
            IEnumerable<Meeting> courses2 = filter.Filter(meetings, specs2);
            Assert.AreEqual(3, courses2.Count());
        }

        [Test]
        public void TimeNonOverlapping()
        {
            ISpecification<Meeting> specs = new TimeNonOverlappingSpecification(new Time("1330-1430"));
            IEnumerable<Meeting> courses = filter.Filter(meetings, specs);
            Assert.AreEqual(2, courses.Count());
        }

        [Test]
        public void ClassRatioNotFull()
        {
            ISpecification<Meeting> specs = new ClassRatioSpecification(1.0f, (x, y) => x < y);
            IEnumerable<Meeting> courses = filter.Filter(meetings, specs);
            Assert.AreEqual(3, courses.Count());
        }
        public void ClassRatioMoreThan50Percent()
        {
            ISpecification<Meeting> specs = new ClassRatioSpecification(.5f, (x, y) => x > y);
            IEnumerable<Meeting> courses = filter.Filter(meetings, specs);
            Assert.AreEqual(3, courses.Count());
        }
    }
}
