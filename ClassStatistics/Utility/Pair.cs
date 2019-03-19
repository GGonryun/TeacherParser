using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class Pair<T> 
    {
        public T First { get; protected set; }
        public T Second { get; protected set; }

        public Pair(T first, T second)
        {
            First = first;
            Second = second;
        }
     
        public static Pair<T> operator +(Pair<T> p1, Pair<T> p2)
        {
            dynamic p1f = p1.First, p2f = p2.First, p1s = p1.Second, p2s = p2.Second;
            return new Pair<T>(p1f + p2f, p1s + p2s);
        }

    }
}
