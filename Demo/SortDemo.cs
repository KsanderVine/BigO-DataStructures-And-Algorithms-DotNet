using SortingAlgorithms;
using System.Diagnostics;

namespace Demo
{
    public class SortDemo : IDemo
    {
        private static readonly Random random = new();

        private readonly Stopwatch stopwatch = new();
        private readonly (string name, Type type)[] types = new (string name, Type type)[]
        {
            ("Shell sort", typeof(ShellSort<int>)),
            ("Bogo sort", typeof(BogoSort<int>)),
            ("Bubble sort", typeof(BubbleSort<int>)),
            ("Quick sort", typeof(QuickSort<int>)),
            ("Shaker sort", typeof(ShakerSort<int>)),
            ("Selection sort", typeof(SelectionSort<int>)),
            ("Insertion sort", typeof(InsertionSort<int>))
        };

        public void Show()
        {
            int[] ints;
            foreach (var sort in types)
            {
                Console.WriteLine(sort.name);

                ints = GetInts().ToArray();
                Console.WriteLine(string.Join(", ", ints));

                stopwatch.Restart();
                ints = ints.SortWith(sort.type);
                stopwatch.Stop();

                Console.WriteLine(string.Join(", ", ints));
                Console.WriteLine($"> Stopwatch: {stopwatch.ElapsedTicks} ticks | {stopwatch.ElapsedMilliseconds} ms.\n");
            }

            static IEnumerable<int> GetInts (int count = 10)
            {
                for(int i = 0; i < count; i++)
                {
                    yield return random.Next(100);
                }
            }
        }
    }
}