using Class;
using NodaTime;
using System;

namespace Filtering.Specifications
{
    class TimeRangeSpecification : ISpecification<Meeting>
    {
        private readonly LocalTime _range;
        private readonly Time _time;

        public TimeRangeSpecification(string range, Time time)
        {
            int hour = Convert.ToInt32(range.Substring(0, 2));
            int minute = Convert.ToInt32(range.Substring(2, 2));
            _range = new LocalTime(hour, minute);
            _time = time;
        }

        public TimeRangeSpecification(LocalTime range, Time time)
        {
            _range = range;
            _time = time;
        }

        public TimeRangeSpecification(LocalTime range, Meeting meeting) : this(range, meeting.Time)
        {
        }
        
        public bool Satisfied(Meeting item)
        {
            if(item.Time.Start == null || item.Time.End == null)
            {
                return false;
            }

            NodaTime.LocalTime startTime = item.Time.Start ?? default(NodaTime.LocalTime);
            NodaTime.LocalTime endTime = item.Time.End ?? default(NodaTime.LocalTime);
            NodaTime.LocalTime _startTime = _time.Start ?? default(NodaTime.LocalTime);
            NodaTime.LocalTime _endTime = _time.End ?? default(NodaTime.LocalTime);

            NodaTime.Period start = NodaTime.Period.Between(startTime, _endTime);
            NodaTime.Period end = NodaTime.Period.Between(_startTime, endTime);

            if(Math.Abs(start.Hours) <= _range.Hour)
            {
                if (Math.Abs(start.Minutes) <= _range.Minute)
                {
                    return true;
                }
            }
            else if (Math.Abs(end.Hours) <= _range.Hour)
            {
                if (Math.Abs(end.Minutes) <= _range.Minute)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
