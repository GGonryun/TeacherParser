using Class;
using HtmlAgilityPack;
using System.Collections.Generic;
using Utility.HTML;

namespace Builder
{
    public class SdsuMeetingsBuilder : IBuilder<IEnumerable<Meeting>>
    {
        private List<Meeting> _meetings;
        private readonly List<Period> _periods;
        private readonly List<string> _majors;

        public SdsuMeetingsBuilder(List<string> majors, List<Period> periods)
        {
            _periods = periods;
            _majors = majors;
        }

        public IEnumerable<Meeting> GetResult()
        {
            if (_meetings == null)
            {
                _meetings = new List<Meeting>();

                foreach (string major in _majors)
                {
                    foreach (Period period in _periods)
                    {
                        if (period.Semester == Semester.Winter)
                        {
                            continue;
                        }

                        Fetcher f = new Fetcher(Source.Web, $@"https://sunspot.sdsu.edu/schedule/search?mode=search&period={period}&abbrev={major}");
                        Parser p = new Parser(f.Document, Parser.SelectByClassPattern("sectionMeeting"));

                        Course c_tmp = null;
                        int sec_tmp = -1;
                        int sch_tmp = -1;
                        float u_tmp = 0f;
                        foreach (HtmlNode node in p.Nodes)
                        {
                            SdsuMeetingBuilder b = new SdsuMeetingBuilder(period, node);

                            Period per = b.BuildPeriod();
                            Course course = b.BuildCourse() ?? c_tmp;
                            int section = b.BuildSection();
                            int schedule = b.BuildSchedule();
                            float units = b.BuildUnits();
                            Format format = b.BuildFormat();
                            Time time = b.BuildTime();
                            Day days = b.BuildDay();
                            Location location = b.BuildLocation();
                            string instructor = b.BuildInstructor();

                            Meeting m = new Meeting(
                                period,
                                course,
                                section == -1 ? sec_tmp : section,
                                schedule == -1 ? sch_tmp : schedule,
                                units < -1 ? u_tmp : units,
                                format,
                                time,
                                days,
                                location,
                                instructor
                                );
                            _meetings.Add(m);

                            c_tmp = course;
                            sec_tmp = section;
                            sch_tmp = schedule;
                            u_tmp = units;
                        }
                    }
                }
            }
            return _meetings;
        }
    }
}
