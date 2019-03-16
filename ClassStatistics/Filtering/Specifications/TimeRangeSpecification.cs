using System;
using System.Collections.Generic;
using System.Text;
using Class;
using NodaTime;

namespace Filtering.Specifications
{
    class TimeRangeSpecification : ISpecification<Meeting>
    {
        private readonly LocalTime _range;
        private readonly Time _time;

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
            NodaTime.Period start = NodaTime.Period.Between(item.Time.Start, _time.End);
            NodaTime.Period end = NodaTime.Period.Between(_time.Start, item.Time.End);

            if(Math.Abs(start.Hours) <= _range.Hour)
            {
                if (MathF.Abs(start.Minutes) <= _range.Minute)
                {
                    return true;
                }
            }
            else if (Math.Abs(end.Hours) <= _range.Hour)
            {
                if (MathF.Abs(end.Minutes) <= _range.Minute)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
