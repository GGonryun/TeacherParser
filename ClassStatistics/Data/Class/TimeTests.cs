using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NodaTime;

namespace Class
{
    class TimeTests
    {
        [Test]
        public void CreateBasicTimeRange()
        {
            LocalTime start = new LocalTime(0, 59);
            LocalTime end = new LocalTime(13, 2);
            Time t = new Time(start, end);
            Assert.AreEqual(start.Hour, t.Start?.Hour);
            Assert.AreEqual(start.Minute, t.Start?.Minute);
            Assert.AreEqual(end.Hour, t.End?.Hour);
            Assert.AreEqual(end.Minute, t.End?.Minute);
        }
        [Test]

        public void CreateBlockTimeRange()
        {
            Time t = new Time("0930-1945");
            Assert.AreEqual(9, t.Start?.Hour);
            Assert.AreEqual(30, t.Start?.Minute);
            Assert.AreEqual(19, t.End?.Hour);
            Assert.AreEqual(45, t.End?.Minute);
        }

        [Test]
        public void ValidStringRepresentation()
        {
            string timeBlock = "1130-1550";
            Time t1 = new Time(timeBlock);
            LocalTime start = new LocalTime(0, 1);
            LocalTime end = new LocalTime(23, 59);
            Time t2 = new Time(start, end);

            Assert.AreEqual(timeBlock, t1.ToString());
            Assert.AreEqual("0001-2359", t2.ToString());
        }

        [Test]
        public void TimeContainsAnotherLocalTime()
        {
            string timeBlock = "1200-1500";
            Time t1 = new Time(timeBlock);

            Assert.IsTrue(t1.Contains(new LocalTime(12, 00)));
            Assert.IsTrue(t1.Contains(new LocalTime(15, 00)));

            Assert.IsFalse(t1.Contains(new LocalTime(11, 59)));
            Assert.IsFalse(t1.Contains(new LocalTime(15, 01)));
        }
        [Test]

        public void EmptyTimeCannotOverlap()
        {
            string et = "";
            string at = "1200-1500";
            Time t1 = new Time(et);
            Time t2 = new Time(at);
            Assert.IsFalse(t1.Contains(t2));
        }

        [Test] 
        public void TimeOverlapsAnotherTime()
        {
            string timeBlockA = "1200-1500";
            string timeBlockB = "1100-1300";
            string timeBlockC = "1400-1600";

            Time t1 = new Time(timeBlockA);
            Time t2 = new Time(timeBlockB);
            Time t3 = new Time(timeBlockC);

            Assert.IsTrue(t1.Contains(t2));
            Assert.IsTrue(t1.Contains(t3));
            Assert.IsFalse(t2.Contains(t3));
        }


    }
}
