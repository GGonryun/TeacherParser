using System;
using System.Collections.Generic;
using System.Text;
using Class;
using NUnit.Framework;

namespace Data
{
    class PeriodTests
    {
        [Test]
        public void PeriodCodeConvertsProperly()
        {
            Period p = new Period("20193");
            Assert.AreEqual(2019, p.Year);
            Assert.AreEqual((Semester)3, p.Semester);
        }
    }
}
