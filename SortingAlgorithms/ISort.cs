namespace SortingAlgorithms
{
    public interface ISort<T> where T : IComparable
    {
        T[] Sort(T[] array);
    }
}