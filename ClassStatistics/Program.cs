using System;
using System.Collections.Generic;
using Class;
using Filtering;
using Filtering.Specifications;
using Utility;

namespace ClassStatistics
{
    public enum School { SDSU }
    public class Program
    {
        static void Main(string[] args)
        {
            School school = School.SDSU;

            List<ISpecification<Meeting>> specs = new List<ISpecification<Meeting>>();
            List<string> subjects = new List<string>();
            List<Period> periods = new List<Period>();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    //=== SUBJECT ARGUMENTS ===//
                    case "--s":
                        i++;
                        subjects.Add(args[i]);
                        break;
                    case "--sm":
                        i++;
                        while (i < args.Length && (args[i])[0] != '-')
                        {
                            subjects.Add(args[i++]);
                        }
                        i--;
                        break;
                    //=== PERIOD ARGUMENTS ===//
                    case "--p":
                        i++;
                        periods.Add(new Period(args[i]));
                        break;
                    case "--pr":
                        i++;
                        string[] range = args[i].Split('-');
                        Period start = new Period(range[0]);
                        Period end = new Period(range[1]);
                        end++;
                        for (Period s = start; s < end; s++)
                        {
                            periods.Add(s);
                        }
                        break;
                    case "--pm":
                        i++;
                        while (i < args.Length && (args[i])[0] != '-')
                        {
                            periods.Add(new Period(args[i]));
                            i++;
                        }
                        i--;
                        break;
                    //=== FILL RATIO ARGUMENTS ===//
                    case "--maxRatio":
                        i++;
                        float maxR = float.Parse(args[i]);
                        specs.Add(new ClassRatioSpecification(ratio => ratio <= maxR));
                        break;
                    case "--minRatio":
                        i++;
                        float minR = float.Parse(args[i]);
                        specs.Add(new ClassRatioSpecification(ratio => ratio >= minR));
                        break;
                    //=== LEVEL ARGUMENTS ===//
                    case "--maxLevel":
                        i++;
                        int maxL = Convert.ToInt32(args[i]);
                        specs.Add(new CourseLevelSpecification(level => level <= maxL));
                        break;
                    case "--minLevel":
                        i++;
                        int minL = Convert.ToInt32(args[i]);
                        specs.Add(new CourseLevelSpecification(level => level >= minL));
                        break;
                    //=== COURSE ARGUMENTS ===//
                    case "--course-num":
                        int num = Convert.ToInt32(args[++i]);
                        specs.Add(new CourseNumberSpecification(num));
                        break;
                    case "--course-sub":
                        string subj = args[++i];
                        specs.Add(new CourseSubjectSpecification(subj));
                        break;
                    //=== IGNORE CLASS ARGUMENTS ===//
                    case "--ignore":
                        i++;
                        List<Course> coursesToIgnore = new List<Course>();
                        while (i < args.Length && (args[i])[0] != '-')
                        {
                            coursesToIgnore.Add(new Course("", args[i++]));
                        }
                        i--;
                        specs.Add(new IgnoreCoursesSpecification(coursesToIgnore));
                        break;
                    //=== INSTRUCTOR ARGUMENTS ===//
                    case "--prof":
                        string matchProf = args[++i];
                        specs.Add(new InstructorNameSpecification(instructor => instructor == matchProf));
                        break;
                    case "--notprof":
                        string notProf = args[++i];
                        specs.Add(new InstructorNameSpecification(instructor => instructor != notProf));
                        break;
                    case "--notprofs":
                        i++;
                        List<string> notProfs = new List<string>();
                        while (i < args.Length && (args[i])[0] != '-')
                        {
                            notProfs.Add(args[i++]);
                        }
                        i--;
                        foreach(string np in notProfs)
                        {
                            specs.Add(new InstructorNameSpecification(instructor => instructor != np));
                        }
                        break;
                    //=== TIME ARGUMENTS ===//
                    case "--outside":
                        string timeRangeCode = args[++i];
                        specs.Add(new TimeNonOverlappingSpecification(new Time(timeRangeCode)));
                        break;
                    case "--within":
                        string within = args[++i];
                        string timeRangeCode2 = args[++i];
                        specs.Add(new TimeRangeSpecification(within, new Time(timeRangeCode2)));
                        break;
                    case "--start-after":
                        string startAfter = args[++i];
                        int sa_hour = Convert.ToInt32(startAfter.Substring(0, 2));
                        int sa_minute = Convert.ToInt32(startAfter.Substring(2, 2));
                        NodaTime.LocalTime sa_time = new NodaTime.LocalTime(sa_hour, sa_minute);
                        specs.Add(new TimeSpecification(true, time => time >= sa_time));
                        break;
                    case "--end-after":
                        string endAfter = args[++i];
                        int ea_hour = Convert.ToInt32(endAfter.Substring(0, 2));
                        int ea_minute = Convert.ToInt32(endAfter.Substring(2, 2));
                        NodaTime.LocalTime ea_time = new NodaTime.LocalTime(ea_hour, ea_minute);
                        specs.Add(new TimeSpecification(true, time => time >= ea_time));
                        break;
                    case "--start-before":
                        string startBefore = args[++i];
                        int sb_hour = Convert.ToInt32(startBefore.Substring(0, 2));
                        int sb_minute = Convert.ToInt32(startBefore.Substring(2, 2));
                        NodaTime.LocalTime sb_time = new NodaTime.LocalTime(sb_hour, sb_minute);
                        specs.Add(new TimeSpecification(true, time => time >= sb_time));
                        break;
                    case "--end-before":
                        string endBefore = args[++i];
                        int eb_hour = Convert.ToInt32(endBefore.Substring(0, 2));
                        int eb_minute = Convert.ToInt32(endBefore.Substring(2, 2));
                        NodaTime.LocalTime eb_time = new NodaTime.LocalTime(eb_hour, eb_minute);
                        specs.Add(new TimeSpecification(true, time => time >= eb_time));
                        break;
                    //=== FORMAT ARGUMENTS ===//
                    case "--format":
                        string matchFormat = args[++i];
                        string matchFormatUppercased = char.ToUpper(matchFormat[0]) + matchFormat.Substring(1);
                        Format _matchFormat = matchFormatUppercased.ParseEnum<Format>();
                        specs.Add(new FormatTypeSpecification(format => format == _matchFormat));
                        break;
                    case "--notformat":
                        string dontMatchFormat = args[++i];
                        string dontMatchFormatUppercased = char.ToUpper(dontMatchFormat[0]) + dontMatchFormat.Substring(1);
                        Format _dontMatchFormat = dontMatchFormatUppercased.ParseEnum<Format>();
                        specs.Add(new FormatTypeSpecification(format => format == _dontMatchFormat));
                        break;
                }
            }

            IBuilder<Meetings> builder = GetBuilder(school, subjects, periods);
            Meetings meetings = builder.GetResult();

            IFilter<Meeting> filter = new MatchAllFilter<Meeting>();
            var filteredMeetings = filter.Filter(meetings, specs.ToArray());

            foreach (Meeting meeting in filteredMeetings)
            {
                Console.WriteLine(meeting.ToString());
            }
        }

        public static IBuilder<Meetings> GetBuilder(School school, List<string> subjects, List<Period> periods)
        {
            switch (school)
            {
                case School.SDSU:
                    return new SdsuMeetingsBuilder(subjects, periods);
            }
            return null;
        }
    }


   


}
