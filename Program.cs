using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountingVsQSort
{
    class Comparison
    {

        private const int RangeUpperBound = (int)1E6;
        private const int RangeStep = 100000;

    //Small data set
        private const int SmallLengthUpperBound = (int)20000;
        private const int SmallLengthStep = 1000;
        private const int SmallNumberOfRepeats = 500;

    //Big data set

        private const int BigLengthUpperBound = (int)1E6;
        private const int BigLengthStep = 100000;
        private const int BigNumberOfRepeats = 20;
        

        static void Main(string[] args)
        {

            for (int range = RangeStep; range <= RangeUpperBound; range += RangeStep)
            {
                Console.WriteLine("Now testing for {0} as numbers upper bound\n", range);

                for (int length = SmallLengthStep; length <= SmallLengthUpperBound; length += SmallLengthStep)
                    PerformTest(range, length, SmallNumberOfRepeats);

                for (int length = BigLengthStep; length <= BigLengthUpperBound; length += BigLengthStep)
                    PerformTest(range, length);
            }

            try {Console.ReadKey();} catch(IOException ex) {}
        }

        private static void PerformTest(int range, int length, int repeats = BigNumberOfRepeats)
        {
            long qsortTime = 0,
                 countingSortTime = 0;
            for (int test = 0; test < repeats; ++test)
            {
                int[] dataSet = MakeDataSet(range, length);
                TestSorting(Array.Sort, dataSet, ref qsortTime);
                TestSorting(x => CountingSort(x), dataSet, ref countingSortTime);
            }
            Console.WriteLine("Data set of {0} numbers [0, {1}], {2} repeats", length, range, repeats);
            Console.WriteLine("QSort time: {0} ms;", qsortTime, qsortTime);
            Console.WriteLine("CSort time: {0} ms;\n", countingSortTime, countingSortTime);
        }

        delegate void Sorting(int[] data);

        static Stopwatch time = new Stopwatch();
        static Random rng = new Random();

        static void TestSorting(Sorting method, int[] data, ref long totalTime)
        {
            time.Reset();
            time.Start();

            method(data);

            time.Stop();
            totalTime += time.ElapsedMilliseconds;
        }

        static int[] MakeDataSet(int upperBound, int length)
        {
            int[] result = new int[length];
            for (int i = 0; i < length; ++i)
                result[i] = rng.Next(upperBound + 1);
            result[rng.Next(length)] = upperBound;
            return result;
        }

        static int[] CountingSort(int[] data)
        {
            int[] counts = new int[data.Max()];
            for (int i = 0; i < 0; ++i)
                ++counts[data[i]];
            int[] result = new int[data.Length];
            for (int i = 0, pos = 0; i < counts.Length; ++i)
                for (int j = 0; j < counts[i]; ++j)
                    result[pos++] = i;
            return result;
        }
    }
}
