namespace SortingAlgorithms
{
    public abstract class SortingBase<T> : ISort<T> where T : IComparable
    {
        public abstract T[] Sort(T[] array);

        protected void Swap(ref T a, ref T b) => (b, a) = (a, b);
    }
}