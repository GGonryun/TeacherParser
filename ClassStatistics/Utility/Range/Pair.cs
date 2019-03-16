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
     
    }
}
