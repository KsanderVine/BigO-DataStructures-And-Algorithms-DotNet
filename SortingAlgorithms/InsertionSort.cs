namespace SortingAlgorithms
{
    public class InsertionSort<T> : SortingBase<T> where T : IComparable
    {
        public override T[] Sort(T[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var key = array[i];
                var j = i;
                while ((j >= 1) && (array[j - 1].CompareTo(key) > 0))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }

                array[j] = key;
            }

            return array;
        }
    }
}