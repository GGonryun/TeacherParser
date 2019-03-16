using System;
using System.Collections.Generic;
using System.Text;
using Class;

namespace Filtering.Specifications
{
    class CourseLevelSpecification : ISpecification<Meeting>
    {

        private readonly Level level;
        private Func<int, int, bool> difficulty;

        /// <summary>
        /// This specification selects classes based on the relative difficulty to a level.
        /// </summary>
        /// <param name="level">Match against.</param>
        /// <param name="difficulty">Determines how the results should compare to our level.</param>
        public CourseLevelSpecification(Level level, Func<int, int, bool> difficulty)
        {
            this.level = level;
            this.difficulty = difficulty;
        }

        public bool Satisfied(Meeting item)
        {
            return difficulty((int)this.level * 100, item.Course.Number);
        }
    }
}
