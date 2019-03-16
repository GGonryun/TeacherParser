using Class;
using System;
using System.Collections.Generic;
using System.Text;

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

        }
    }
}
