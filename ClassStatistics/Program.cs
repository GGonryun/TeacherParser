using System;
using System.Collections.Generic;
using Class;
using Filtering;
using Filtering.Specifications;
using Utility;
using Builder;
using System.Linq;

namespace ClassStatistics
{
    public enum School { SDSU }
    public class Program
    {
        class Arguments
        {
            //These values should not be changed after being initialized, we can modify their
            //contents but not change the list.
            public School School { get; private set; }
            public List<ISpecification<Meeting>> Specifications { get; private set; }
            public List<string> Subjects { get; private set; }
            public List<Period> Periods { get; private set; }
            public Dictionary<string, Pair<float>> PopularityChart { get; private set; }
            public List<string> Output { get; private set; }
            //These values can change at any time and should be set while parsing the
            //original args passed by the user.
            public bool DisplayPopularity { get; set; }
            public bool DisplayResults { get; set; }
            public bool DisplayVerbose { get; set; }

            public Arguments(School school = School.SDSU)
            {
                this.School = school;
                Specifications = new List<ISpecification<Meeting>>();
                Subjects = new List<string>();
                Periods = new List<Period>();
                PopularityChart = new Dictionary<string, Pair<float>>();
                Output = new List<string>();

                DisplayResults = true;
                DisplayVerbose = false;
                DisplayPopularity = false;

            }
        }

        static void Main(string[] args)
        {
            Arguments arguments = InitializeArguments(args);
            IEnumerable<Meeting> filteredMeetings = SelectMeetings(arguments);
            
            PopulateOutput(arguments, filteredMeetings);

            foreach(string line in arguments.Output)
            {
                Console.WriteLine(line);
            }
            

        }

        private static void PopulateOutput(Arguments arguments, IEnumerable<Meeting> filteredMeetings)
        {
            foreach (Meeting meeting in filteredMeetings)
            {
                PopulatePopularityDictionary(arguments.PopularityChart, meeting);

                if (arguments.DisplayResults)
                {
                    arguments.Output.Add(meeting.Display(arguments.DisplayVerbose));
                }

            }

            if (arguments.DisplayPopularity)
            {
                Func<KeyValuePair<string, Pair<float>>, float> calculateRatio = kvp => (kvp.Value.First - kvp.Value.Second) / kvp.Value.First;

                var popularity = arguments.PopularityChart.OrderByDescending(kvp => calculateRatio(kvp))
                                                          .Select(kvp => new
                                                          {
                                                              Instructor = kvp.Key,
                                                              Ratio = calculateRatio(kvp)
                                                          });

                arguments.Output.Add($"====== PROFESSOR POPULARITY ======");
                foreach (var pair in popularity)
                {
                    if(pair.Ratio == pair.Ratio)
                    {
                        arguments.Output.Add($"{pair.Instructor}'s popularity: {100 * pair.Ratio:F2}");
                    }
                }
            }
        }

        private static Arguments InitializeArguments(string[] args)
        {
            Arguments arguments = new Arguments();
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    //=== SUBJECT ARGUMENTS ===//
                    case "--s":
                        i++;
                        arguments.Subjects.Add(args[i]);
                        break;
                    case "--sm":
                        i++;
                        while (i < args.Length && (args[i])[0] != '-')
                        {
                            arguments.Subjects.Add(args[i++]);
                        }
                        i--;
                        break;
                    //=== PERIOD ARGUMENTS ===//
                    case "--p":
                        i++;
                        arguments.Periods.Add(new Period(args[i]));
                        break;
                    case "--pr":
                        i++;
                        string[] range = args[i].Split('-');
                        Period start = new Period(range[0]);
                        Period end = new Period(range[1]);
                        end++;
                        for (Period s = start; s < end; s++)
                        {
                            arguments.Periods.Add(s);
                        }
                        break;
                    case "--pm":
                        i++;
                        while (i < args.Length && (args[i])[0] != '-')
                        {
                            arguments.Periods.Add(new Period(args[i]));
                            i++;
                        }
                        i--;
                        break;
                    //=== FILL RATIO ARGUMENTS ===//
                    case "--maxRatio":
                        i++;
                        float maxR = float.Parse(args[i]);
                        arguments.Specifications.Add(new ClassRatioSpecification(ratio => ratio <= maxR));
                        break;
                    case "--minRatio":
                        i++;
                        float minR = float.Parse(args[i]);
                        arguments.Specifications.Add(new ClassRatioSpecification(ratio => ratio >= minR));
                        break;
                    //=== LEVEL ARGUMENTS ===//
                    case "--maxLevel":
                        i++;
                        int maxL = Convert.ToInt32(args[i]);
                        arguments.Specifications.Add(new CourseLevelSpecification(level => level <= maxL));
                        break;
                    case "--minLevel":
                        i++;
                        int minL = Convert.ToInt32(args[i]);
                        arguments.Specifications.Add(new CourseLevelSpecification(level => level >= minL));
                        break;
                    //=== COURSE ARGUMENTS ===//
                    case "--course-num":
                        int num = Convert.ToInt32(args[++i]);
                        arguments.Specifications.Add(new CourseNumberSpecification(num));
                        break;
                    case "--course-sub":
                        string subj = args[++i];
                        arguments.Specifications.Add(new CourseSubjectSpecification(subj));
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
                        arguments.Specifications.Add(new IgnoreCoursesSpecification(coursesToIgnore));
                        break;
                    //=== INSTRUCTOR ARGUMENTS ===//
                    //The Professor names are matched in uppercase only. Please
                    //ensure that the user argument is also uppercase when added.
                    case "--prof":
                        string matchProf = args[++i].ToUpper();
                        arguments.Specifications.Add(new InstructorNameSpecification(instructor => instructor == matchProf));
                        break;
                    case "--notprof":
                        string notProf = args[++i].ToUpper();
                        arguments.Specifications.Add(new InstructorNameSpecification(instructor => instructor != notProf));
                        break;
                    case "--notprofs":
                        i++;
                        List<string> notProfs = new List<string>();
                        while (i < args.Length && (args[i])[0] != '-')
                        {
                            notProfs.Add(args[i++].ToUpper());
                        }
                        i--;
                        foreach (string np in notProfs)
                        {
                            arguments.Specifications.Add(new InstructorNameSpecification(instructor => instructor != np));
                        }
                        break;
                    //=== TIME ARGUMENTS ===//
                    case "--outside":
                        string timeRangeCode = args[++i];
                        arguments.Specifications.Add(new TimeNonOverlappingSpecification(new Time(timeRangeCode)));
                        break;
                    case "--within":
                        string within = args[++i];
                        string timeRangeCode2 = args[++i];
                        arguments.Specifications.Add(new TimeRangeSpecification(within, new Time(timeRangeCode2)));
                        break;
                    case "--start-after":
                        string startAfter = args[++i];
                        int sa_hour = Convert.ToInt32(startAfter.Substring(0, 2));
                        int sa_minute = Convert.ToInt32(startAfter.Substring(2, 2));
                        NodaTime.LocalTime sa_time = new NodaTime.LocalTime(sa_hour, sa_minute);
                        arguments.Specifications.Add(new TimeSpecification(true, time => time >= sa_time));
                        break;
                    case "--end-after":
                        string endAfter = args[++i];
                        int ea_hour = Convert.ToInt32(endAfter.Substring(0, 2));
                        int ea_minute = Convert.ToInt32(endAfter.Substring(2, 2));
                        NodaTime.LocalTime ea_time = new NodaTime.LocalTime(ea_hour, ea_minute);
                        arguments.Specifications.Add(new TimeSpecification(false, time => time >= ea_time));
                        break;
                    case "--start-before":
                        string startBefore = args[++i];
                        int sb_hour = Convert.ToInt32(startBefore.Substring(0, 2));
                        int sb_minute = Convert.ToInt32(startBefore.Substring(2, 2));
                        NodaTime.LocalTime sb_time = new NodaTime.LocalTime(sb_hour, sb_minute);
                        arguments.Specifications.Add(new TimeSpecification(true, time => time <= sb_time));
                        break;
                    case "--end-before":
                        string endBefore = args[++i];
                        int eb_hour = Convert.ToInt32(endBefore.Substring(0, 2));
                        int eb_minute = Convert.ToInt32(endBefore.Substring(2, 2));
                        NodaTime.LocalTime eb_time = new NodaTime.LocalTime(eb_hour, eb_minute);
                        arguments.Specifications.Add(new TimeSpecification(false, time => time <= eb_time));
                        break;
                    //=== FORMAT ARGUMENTS ===//
                    case "--format":
                        string matchFormat = args[++i];
                        string matchFormatUppercased = char.ToUpper(matchFormat[0]) + matchFormat.Substring(1);
                        Format _matchFormat = matchFormatUppercased.ParseEnum<Format>();
                        arguments.Specifications.Add(new FormatTypeSpecification(format => format == _matchFormat));
                        break;
                    case "--notformat":
                        string dontMatchFormat = args[++i];
                        string dontMatchFormatUppercased = char.ToUpper(dontMatchFormat[0]) + dontMatchFormat.Substring(1);
                        Format _dontMatchFormat = dontMatchFormatUppercased.ParseEnum<Format>();
                        arguments.Specifications.Add(new FormatTypeSpecification(format => format == _dontMatchFormat));
                        break;
                    //=== DISPLAY ARGUMENTS ===//
                    case "--q":
                    case "--quiet":
                        arguments.DisplayResults = false;
                        break;
                    case "--v":
                    case "--verbose":
                        arguments.DisplayVerbose = true;
                        break;
                    case "--popularity":
                        arguments.DisplayPopularity = true;
                        break;
                }
            }
            return arguments;
        }

        private static IEnumerable<Meeting> SelectMeetings(Arguments arguments)
        {
            return FilterMeetings(arguments, CreateMeetings(arguments));
        }

        private static IEnumerable<Meeting> CreateMeetings(Arguments arguments)
        {
            IBuilder<IEnumerable<Meeting>> builder = GetBuilder(arguments.School, arguments.Subjects, arguments.Periods);
            IEnumerable<Meeting> meetings = builder.GetResult();
            return meetings;
        }

        private static IEnumerable<Meeting> FilterMeetings(Arguments arguments, IEnumerable<Meeting> meetings)
        {
            IFilter<Meeting> filter = new MatchAllFilter<Meeting>();
            IEnumerable<Meeting> filteredMeetings = filter.Filter(meetings, arguments.Specifications.ToArray());
            return filteredMeetings;
        }

        private static void PopulatePopularityDictionary(Dictionary<string, Pair<float>> PopularityChart, Meeting meeting)
        {
            string key = meeting.Instructor;
            Pair<float> value = new Pair<float>(meeting.Location.Capacity, meeting.Location.RemainingSeats);

            if (PopularityChart.ContainsKey(key))
            {
                value += new Pair<float>(PopularityChart[key].First, PopularityChart[key].Second);
            }
            PopularityChart[key] = value;
        }

        public static IBuilder<IEnumerable<Meeting>> GetBuilder(School school, List<string> subjects, List<Period> periods)
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
