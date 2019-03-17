using Class;


namespace Filtering.Specifications
{
    public class InstructorNameSpecification : ISpecification<Meeting>
    {

        private readonly string _instructorName;

        public InstructorNameSpecification(string instructorName)
        {
            _instructorName = instructorName;
        }

        public bool Satisfied(Meeting item)
        {
            return item.Instructor == _instructorName;
        }
    }
}
