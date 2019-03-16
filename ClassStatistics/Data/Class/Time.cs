using NodaTime;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public Time(string timeBlock)
        {
            string[] times = timeBlock.Split('-');
            Start = new LocalTime(Convert.ToInt32(times[0].Substring(0, 2)), Convert.ToInt32(times[0].Substring(2, 2)));
            End = new LocalTime(Convert.ToInt32(times[1].Substring(0, 2)), Convert.ToInt32(times[1].Substring(2, 2)));
        }

        public override string ToString()
        {
            return $"{Start.Hour:D2}{Start.Minute:D2}-{End.Hour:D2}{End.Minute:D2}";
        }
    }
}
