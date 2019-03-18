using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filtering.Specifications
{
    class FormatTypeSpecification : ISpecification<Meeting>
    {
        private readonly Predicate<Format> _matchAgainstFormat;

        public FormatTypeSpecification(Predicate<Format> matchAgainstFormat)
        {
            _matchAgainstFormat = matchAgainstFormat;
        }

        public bool Satisfied(Meeting item)
        {
            return _matchAgainstFormat(item.Format);
        }
    }
}
