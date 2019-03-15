using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class Time
    {
        public LocalTime Start { get; private set; }
        public LocalTime End { get; private set; }

        public Time(LocalTime start, LocalTime end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
