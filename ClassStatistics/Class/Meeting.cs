
using System;

namespace Class
{
    public class Meeting
    {
        public Period Period { get; private set; }
        public Course Course { get; private set; }
        public int Section { get; private set; }
        public int Schedule { get; private set; }
        public float Units { get; private set; }
        public Class.Format Format { get; private set; }
        public Class.Time Time { get; private set; }
        public Class.Day Day { get; private set; }
        public Class.Location Location { get; private set; }
        public string Instructor { get; private set; }

        public Meeting(Period period, Course course, int section, int schedule, float units, Format format, Time time, Day day, Location location, string instructor)
        {
            Period = period ?? throw new ArgumentNullException(nameof(period));
            Course = course ?? throw new ArgumentNullException(nameof(course));
            Section = section;
            Schedule = schedule;
            Units = units;
            Format = format;
            Time = time ?? throw new ArgumentNullException(nameof(time));
            Day = day ?? throw new ArgumentNullException(nameof(day));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Instructor = instructor ?? throw new ArgumentNullException(nameof(instructor));
        }

        public string Display(bool verbose)
        {
            if (verbose)
                return $"[{Period.Semester.ToString()} {Period.Year}] - {Course.Title} ({Course.Code}) at {Time.ToString()} on {Day.Code} in room {Location.Room.Code} with Professor {Instructor}. {Location.RemainingSeats} seats currently remain out of {Location.Capacity}.";
            else
                return $"[{Period.Semester.ToString()} {Period.Year}] - {Course.Code}: {Time.ToString()} {Day.Code} @ {Location.Room.Code} w/ {Instructor}. {100*Location.Ratio:F2}%";
        }
    }
}
