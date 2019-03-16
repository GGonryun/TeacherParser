using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NodaTime;
namespace Class
{
    [TestFixture]
    public class DayTests
    {
        Day thursdayDay;
        Day mwDays;
        Day tthDays;
        Day mwfDays;


        [SetUp] protected void SetUp()
        {
            thursdayDay = new Day("TH");
            mwDays = new Day("MW");
            tthDays = new Day("TTH");
            mwfDays = new Day("MWF");
        }

        [Test] public void ThursdayConverts()
        {
            Assert.AreEqual("TH", thursdayDay.Code);
            Assert.IsTrue(thursdayDay.Days.Contains(IsoDayOfWeek.Thursday));
            Assert.AreEqual(thursdayDay.Days.Count, 1);
        }

        [Test] public void MondayWednesdayCombo()
        {
            Assert.AreEqual("MW", mwDays.Code);
            Assert.IsTrue(mwDays.Days.Contains(IsoDayOfWeek.Wednesday));
            Assert.IsTrue(mwDays.Days.Contains(IsoDayOfWeek.Monday));
            Assert.AreEqual(2, mwDays.Days.Count);
        }

        [Test] public void TuesdayThursdayCombo()
        {
            Assert.AreEqual("TTH", tthDays.Code);
            Assert.IsTrue(tthDays.Days.Contains(IsoDayOfWeek.Tuesday));
            Assert.IsTrue(tthDays.Days.Contains(IsoDayOfWeek.Thursday));
            Assert.AreEqual(2, tthDays.Days.Count);
        }

        [Test] public void MondayWednesdayFridayCombo()
        {
            Assert.AreEqual("MWF", mwfDays.Code);
            mwfDays.Days.Remove(IsoDayOfWeek.Friday);
            Assert.IsTrue(mwDays.Days.Overlaps(mwfDays.Days));
        }
    }
}
