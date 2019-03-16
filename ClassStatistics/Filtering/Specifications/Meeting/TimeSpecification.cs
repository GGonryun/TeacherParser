using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filtering.Specifications
{
    class TimeSpecification : ISpecification<Meeting>
    {

        private readonly NodaTime.LocalTime _time;
        private readonly Func<NodaTime.LocalTime, NodaTime.LocalTime, bool> _comparator;
        private readonly bool _checkAgainstStartTime;
        public TimeSpecification(bool checkAgainstStartTime, NodaTime.LocalTime time, Func<NodaTime.LocalTime, NodaTime.LocalTime, bool> comparator)
        {
            _checkAgainstStartTime = checkAgainstStartTime;
            _time = time;
            _comparator = comparator;
        }
        public TimeSpecification(bool checkAgainstStartTime, Meeting meeting, Func<NodaTime.LocalTime, NodaTime.LocalTime, bool> comparator) : this(checkAgainstStartTime, meeting.Time.End, comparator)
        {
        }

        public bool Satisfied(Meeting item)
        {
            if(_checkAgainstStartTime)
            {
                return _comparator(item.Time.Start, _time);
            }
            return _comparator(item.Time.End, _time);
        }
    }
}
