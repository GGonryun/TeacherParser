using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
namespace Class
{
    public class LocationTests
    {
        [Test]
        public void LocationArgumentsAreValid()
        {
            Location l = new Location(100, 9, 0, new Room("GMCS", 101));
            Assert.AreEqual(100, l.Capacity);
            Assert.AreEqual("GMCS", l.Room.Building);
        }

        [Test]
        public void RatioIsValid()
        {
            Location l = new Location(100, -20, 0, "GMCS", 99);
            Assert.AreEqual(1.2f, l.Ratio, .005f);
        }
    }
}
