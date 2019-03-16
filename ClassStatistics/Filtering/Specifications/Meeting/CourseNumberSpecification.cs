using Class;
using System;
using System.Collections.Generic;
using System.Text;
namespace Filtering.Specifications
{
    public class CourseNumberSpecification : ISpecification<Meeting>
    {
        private readonly int _number;

        public CourseNumberSpecification(int number)
        {
            _number = number;
        }

        public bool Satisfied(Meeting item)
        {
            return _number == item.Course.Number;
        }
    }
}
