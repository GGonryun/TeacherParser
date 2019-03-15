namespace ClassStatistics
{
    public class Instructor
    {
        public string Name { get; private set; }
        public string SearchQuery { get; private set; }
        public Instructor(string name, string query)
        {
            this.Name = name;
            this.SearchQuery = query;
        }
    }
}