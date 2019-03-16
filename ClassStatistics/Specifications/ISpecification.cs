namespace Filtering
{
    public interface ISpecification<T>
    {
        bool Satisfied(T item);
    }
}
