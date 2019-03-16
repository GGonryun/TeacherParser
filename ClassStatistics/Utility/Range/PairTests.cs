using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Utility
{


    public class PairTests
    {
        [Test]
        public void IntPair()
        {
            int f = 1;
            int s = 9;
            Pair<int> ip = new Pair<int>(f, s);
            Assert.AreEqual(f, ip.First);
            Assert.AreEqual(s, ip.Second);
        }
    }
}
