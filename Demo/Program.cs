using DataStructures;
using SearchAlgorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demo
{
    internal class Program
    {
        static void Main()
        {
            (string description, IDemo demo)[] demos = new[]
            {
                ("Sort algorithms demonstration", new SortDemo() as IDemo)
            };

            foreach(var (description, demo) in demos)
            {
                Console.WriteLine(description);
                demo.Show();
                Console.WriteLine('\n');
            }
        }
    }
}