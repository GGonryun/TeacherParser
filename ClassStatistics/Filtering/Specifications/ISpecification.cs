namespace Filtering.Specifications
{
    public interface ISpecification<T>
    {
        bool Satisfied(T item);
    }
}
