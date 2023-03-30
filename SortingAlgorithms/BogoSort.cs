namespace SortingAlgorithms
{
    public class BogoSort<T>: SortingBase<T> where T : IComparable
    {
        private static readonly Random random = new();

        public override T[] Sort(T[] array)
        {
            while (!IsSorted(array))
            {
                array = RandomPermutation(array);
            }
            return array;
        }

        static T[] RandomPermutation(T[] a)
        {
            var n = a.Length;
            while (n > 1)
            {
                n--;
                var i = random.Next(n + 1);
                (a[n], a[i]) = (a[i], a[n]);
            }

            return a;
        }

        static bool IsSorted(T[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                if (a[i].CompareTo(a[i + 1]) > 0)
                    return false;
            }

            return true;
        }
    }
}