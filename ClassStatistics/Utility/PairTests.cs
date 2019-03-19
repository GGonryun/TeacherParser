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

        public void AddableIntPair()
        {
            int f1 = 1, f2 = 2, s1 = 25, s2 = 10;
            Pair<int> p1 = new Pair<int>(f1, s1);
            Pair<int> p2 = new Pair<int>(f2, s2);
            Pair<int> p3 = p1 + p2;
            Assert.AreEqual(f1 + s1, p3.First);
            Assert.AreEqual(f2 + s2, p3.Second);
        }
    }
}
