﻿using Class;

namespace Filtering.Specifications
{
    class TimeNonOverlappingSpecification : ISpecification<Meeting>
    {
        private readonly Time _time;

        public TimeNonOverlappingSpecification(Time time)
        {
            _time = time;
        }

        public bool Satisfied(Meeting item)
        {
            return !(_time.Contains(item.Time));
        }
    }
}
