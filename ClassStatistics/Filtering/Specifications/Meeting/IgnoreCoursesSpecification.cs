﻿using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filtering.Specifications
{
    class IgnoreCoursesSpecification : ISpecification<Meeting>
    {
        private readonly List<Course> _ignoreCourses;

        public IgnoreCoursesSpecification(List<Course> ignoreCourses)
        {
            _ignoreCourses = ignoreCourses;
        }


        public bool Satisfied(Meeting item)
        {
            foreach(Course course in _ignoreCourses)
            {
                if(course.Code == item.Course.Code)
                {
                    return false;
                }
            }
            return true;
        }
    }
}