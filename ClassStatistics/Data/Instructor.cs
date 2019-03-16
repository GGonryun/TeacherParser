using System;

namespace Class
{
    public class Instructor : IEquatable<Instructor>
    {
        public string Name { get; private set; }
        public string SearchQuery { get; private set; }
        public Instructor(string name, string query)
        {
            this.Name = name;
            this.SearchQuery = query;
        }

        public bool Equals(Instructor other)
        {
            return this.Name == other.Name && this.SearchQuery == other.SearchQuery;
        }
    }
}