using System;
using Class;

namespace Filtering.Specifications
{
    class CourseLevelSpecification : ISpecification<Meeting>
    {

        private readonly Predicate<int> _difficulty;

        /// <summary>
        /// This specification selects classes based on the relative difficulty to a level.
        /// </summary>
        /// <param name="level">Match against.</param>
        /// <param name="difficulty">Determines how the results should compare to our level.</param>
        public CourseLevelSpecification(Predicate<int> compareToDifficulty)
        {
            _difficulty = compareToDifficulty;
        }

        public bool Satisfied(Meeting item)
        {
            return _difficulty(item.Course.Number);
        }
    }
}
