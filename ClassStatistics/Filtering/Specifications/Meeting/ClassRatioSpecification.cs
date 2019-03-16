using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filtering.Specifications
{
    class ClassRatioSpecification : ISpecification<Meeting>
    {
        private readonly Func<float, float, bool> _comparator;
        private readonly float _ratio;

        public ClassRatioSpecification(float ratio, Func<float, float, bool> comparator)
        {
            _ratio = ratio;
            _comparator = comparator;
        }

public bool Satisfied(Meeting item)
        {
            return _comparator(item.Location.Ratio, _ratio);
        }
    }
}
