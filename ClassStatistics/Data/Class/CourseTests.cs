using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
namespace Class
{
    public class CourseTests
    {
        [Test] public void IsCourseCodeCorrect()
        {
            Course c = new Course("A CLASS", "MATH", 999);
            Assert.AreEqual("MATH-999", c.Code);
        }
    }
}
