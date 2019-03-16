
using System;

namespace Class
{
    public class Meeting
    {


        public Course Course { get; private set; }
        public int Section { get; private set; }
        public int Schedule { get; private set; }
        public string CourseTitle { get; private set; }
        public float Units { get; private set; }
        public Class.Format Format { get; private set; }
        public Class.Time Time { get; private set; }
        public Class.Day Day { get; private set; }
        public Class.Location Location { get; private set; }
        public string Instructor { get; private set; }

        public Meeting(Course course, int section, int schedule, string courseTitle, float units, Format format, Time time, Day day, Location location, string instructor)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            Section = section;
            Schedule = schedule;
            CourseTitle = courseTitle ?? throw new ArgumentNullException(nameof(courseTitle));
            Units = units;
            Format = format;
            Time = time ?? throw new ArgumentNullException(nameof(time));
            Day = day ?? throw new ArgumentNullException(nameof(day));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Instructor = instructor ?? throw new ArgumentNullException(nameof(instructor));
        }

    }
}
