using Class;
using System;

namespace Filtering.Specifications
{
    public class InstructorNameSpecification : ISpecification<Meeting>
    {

        private readonly Predicate<string> _matchAgainst;

        public InstructorNameSpecification(Predicate<string> matchAgainst)
        {
            _matchAgainst = matchAgainst;
        }

        public bool Satisfied(Meeting item)
        {
            return _matchAgainst(item.Instructor);
        }
    }
}
