using Class;
using System;

namespace Filtering.Specifications
{
    class ClassRatioSpecification : ISpecification<Meeting>
    {
        private readonly Predicate<float> _comparator;

        public ClassRatioSpecification(Predicate<float> compareToRatio)
        {
            _comparator = compareToRatio;
        }

public bool Satisfied(Meeting item)
        {
            return _comparator(item.Location.Ratio);
        }
    }
}
