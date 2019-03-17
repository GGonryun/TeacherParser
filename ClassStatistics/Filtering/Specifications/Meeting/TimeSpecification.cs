using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filtering.Specifications
{
    class TimeSpecification : ISpecification<Meeting>
    {

        private readonly Predicate<NodaTime.LocalTime?> _comparator;
        private readonly bool _checkAgainstStartTime;

        public TimeSpecification(bool checkAgainstStartTime, Predicate<NodaTime.LocalTime?> comapreAgainstTime)
        {
            _checkAgainstStartTime = checkAgainstStartTime;
            _comparator = comapreAgainstTime;
        }

        public bool Satisfied(Meeting item)
        {
            if(_checkAgainstStartTime)
            {
                return _comparator(item.Time.Start);
            }
            return _comparator(item.Time.End);
        }
    }
}
