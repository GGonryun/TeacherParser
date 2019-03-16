using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filtering.Specifications
{
    public class CourseSubjectSpecification : ISpecification<Meeting>
    {
        private readonly string _courseSubject;
        public CourseSubjectSpecification(string courseSubject)
        {
            _courseSubject = courseSubject;
        }

        public bool Satisfied(Meeting item)
        {
            return item.Course.Subject == _courseSubject;
        }
    }
}
