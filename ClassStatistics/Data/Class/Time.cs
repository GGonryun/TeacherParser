using NodaTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Class
{
    public class Time
    {
        public LocalTime? Start { get; private set; }
        public LocalTime? End { get; private set; }

        public Time(LocalTime start, LocalTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public Time(string timeBlock)
        {
            if(timeBlock.Length < 1)
            {
                Start = null;
                End = null;
            }
            else
            {
                string[] times = timeBlock.Split('-');
                Start = ConvertToLocalTime(times[0]);
                End = ConvertToLocalTime(times[1]);
            }
            
        }

        private LocalTime? ConvertToLocalTime(string value)
        {
            if(value.Length != 4)
            {
                return null;
            }
            int hour = Convert.ToInt32(value.Substring(0, 2));
            int minutes = Convert.ToInt32(value.Substring(2, 2));

            if(hour > 23)
            {
                throw new ArgumentException($"Invalid hour value [{hour}]! The value must be within 0-23.");
            }
            if(minutes > 59)
            {
                throw new ArgumentException($"Invalid minutes value [{minutes}]! The value must be within 0-59");
            }

            return new LocalTime(hour, minutes);

        }

        public override string ToString()
        {
            if(Start == null || End == null)
            {
                return "";
            }
            return $"{Start?.Hour:D2}{Start?.Minute:D2}-{End?.Hour:D2}{End?.Minute:D2}";
        }

        public bool Contains(LocalTime a)
        {
            return a >= Start && a <= End;
        }
        public bool Contains(Time a)
        {
            return a.Start <= End && Start <= a.End;
        }
    }
}
