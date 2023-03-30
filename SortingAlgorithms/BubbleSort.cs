namespace SortingAlgorithms
{
    public class BubbleSort<T> : SortingBase<T> where T : IComparable
    {
        public override T[] Sort(T[] array)
        {
            var len = array.Length;
            for (var i = 1; i < len; i++)
            {
                for (var j = 0; j < len - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }

            return array;
        }
    }
}