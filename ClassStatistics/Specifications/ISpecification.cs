namespace Utility
{
    public interface ISpecification<T>
    {
        bool Satisfied(T item);
    }
}
