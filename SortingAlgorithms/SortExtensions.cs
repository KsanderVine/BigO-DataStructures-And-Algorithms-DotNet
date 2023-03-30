namespace SortingAlgorithms
{
    public static class SortExtensions
    {
        public static T[] SortWith<T>(this T[] array, Type sortType) where T : IComparable
        {
            if (Activator.CreateInstance(sortType) is ISort<T> sort)
            {
                return sort.Sort(array).ToArray();
            }
            else
            {
                return array;
            }
        }
    }
}