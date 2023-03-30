namespace SearchAlgorithms
{
    public static class SearchExtensions
    {
        public static int BinarySearch<T>(this T[] array, T searchedValue) where T : IComparable
        {
            return BinarySearch(array, searchedValue, 0, array.Length);
        }

        public static int BinarySearch<T>(this T[] array, T searchedValue, int start, int end) where T : IComparable
        {
            if (start > end)
            {
                return -1;
            }

            var middle = (start + end) / 2;
            var middleValue = array[middle];

            if (middleValue.Equals(searchedValue))
            {
                return middle;
            }
            else
            {
                if (middleValue.CompareTo(searchedValue) > 0)
                {
                    return BinarySearch(array, searchedValue, start, middle - 1);
                }
                else
                {
                    return BinarySearch(array, searchedValue, middle + 1, end);
                }
            }
        }
    }
}