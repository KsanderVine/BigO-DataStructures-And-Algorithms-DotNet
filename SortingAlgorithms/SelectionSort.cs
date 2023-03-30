namespace SortingAlgorithms
{
    public class SelectionSort<T> : SortingBase<T> where T : IComparable
    {
        static int IndexOfMin(T[] array, int n)
        {
            int result = n;
            for (var i = n; i < array.Length; ++i)
            {
                if (array[i].CompareTo(array[result]) < 0)
                {
                    result = i;
                }
            }

            return result;
        }

        T[] Sort(T[] array, int currentIndex)
        {
            if (currentIndex == array.Length)
                return array;

            var index = IndexOfMin(array, currentIndex);
            if (index != currentIndex)
            {
                Swap(ref array[index], ref array[currentIndex]);
            }

            return Sort(array, currentIndex + 1);
        }

        public override T[] Sort(T[] array)
        {
            return Sort(array, 0);
        }
    }
}