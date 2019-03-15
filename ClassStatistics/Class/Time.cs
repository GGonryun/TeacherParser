using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class Time
    {
        public Day Day { get; private set; }
        public LocalTime Start { get; private set; }
        public LocalTime End { get; private set; }

    }
}
