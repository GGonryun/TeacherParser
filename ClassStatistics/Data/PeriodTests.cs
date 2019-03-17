using System;
using System.Collections.Generic;
using System.Text;
using Class;
using NUnit.Framework;

namespace Data
{
    class PeriodTests
    {
        Period p1, p2, p3, p4, p5;
        [SetUp]
        public void SetUp()
        {
            p1 = new Period("20183");
            p2 = new Period("20182");
            p3 = new Period("20181");
            p4 = new Period("20194");
            p5 = new Period("20152");
        }

        [Test]
        public void PeriodCodeConvertsProperly()
        {
            Assert.AreEqual(2018, p1.Year);
            Assert.AreEqual((Semester)3, p1.Semester);
        }

        [Test]
        public void PeriodIsLessThan()
        {
            Assert.IsTrue(p2 < p1);
            Assert.IsTrue(p5 < p4);
            Assert.IsFalse(p2 < p5);
        }

        [Test]
        public void PeriodIsMoreThan()
        {
            Assert.IsTrue(p1 > p3);
            Assert.IsTrue(p4 > p5);
            Assert.IsFalse(p5 > p2);
        }
    }
}
