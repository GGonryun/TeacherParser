
namespace Class
{
    class Meeting
    {
        public string Course { get; private set; }
        public int Section { get; private set; }
        public int Schedule { get; private set; }
        public string CourseTitle { get; private set; }
        public float Units { get; private set; }
        public Class.Format Format { get; private set; }
        public Class.Time Time { get; private set; }
        public Class.Day Day { get; private set; }
        public Class.Room Room { get; private set; }
        public Instructor Instructor { get; private set; }
    }
}
