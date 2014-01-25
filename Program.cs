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

            Console.ReadKey();
        }

        private static void PerformTest(int range, int length, int repeats = BigNumberOfRepeats)
        {
            Console.WriteLine("Data set of {0} numbers [0, {1}], {2} repeats", length, range, repeats);
            time.Restart();
            for (int test = 0; test < repeats; ++test)
                Array.Sort(MakeDataSet(range, length));
            Console.WriteLine("QSort time: {0} ms;", time.ElapsedMilliseconds);
            time.Restart();
            for (int test = 0; test < repeats; ++test)
                CountingSort(MakeDataSet(range, length));
            Console.WriteLine("CSort time: {0} ms;\n", time.ElapsedMilliseconds);
        }

        delegate void Sorting(int[] data);

        static Stopwatch time = new Stopwatch();
        static Random rng = new Random();

        static int[] MakeDataSet(int upperBound, int length)
        {
            int[] result = new int[length];
            for (int i = 0; i < length; ++i)
                result[i] = rng.Next(upperBound + 1);
            result[rng.Next(length)] = upperBound;
            return result;
        }

        static void CountingSort(int[] data)
        {
            int[] counts = new int[data.Max()];
            for (int i = 0; i < 0; ++i)
                ++counts[data[i]];
            for (int i = 0, pos = 0; i < counts.Length; ++i)
                for (int j = 0; j < counts[i]; ++j)
                    data[pos++] = i;
        }
    }
}
