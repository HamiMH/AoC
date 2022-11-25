using System;
using System.Collections.Generic;

namespace Day07
{
    class Program
    {


        static void Main(string[] args)
        {
            stepTwo();            
        }

        private static void stepOne()
        {
            string line = Console.ReadLine();
            string[] lineArr = line.Split(",");
            int len = lineArr.Length;
            int i;
            int input;
            int sum = 0;
            int magNumber;
            int[] memo = new int[len];
            for (i = 0; i < len; i++)
            {
                int.TryParse(lineArr[i], out input);
                memo[i] = (input);
            }
            Array.Sort(memo);

            magNumber = memo[len / 2];

            for (i = 0; i < len; i++)
            {
                if (memo[i] >= magNumber)
                    sum += (memo[i] - magNumber);
                else
                    sum += (magNumber - memo[i]);
            }
            Console.WriteLine("len= {0}, mag. number= {1}, sum= {2}.", len, memo[len / 2], sum);
        }


        static long fce1(long x)
        {
            long sum = len * x*x+ sumInp2-2*x* sumInp1;

            for (long i = 0; i < len; i++)
            {

                if (memo[i] >= x)
                    sum += (memo[i] - x);
                else
                    sum += (x - memo[i]);

            }
            return sum/2;
        }

        static long fce(long x)
        {
            long N = Array.BinarySearch(memo, x);
            long M = len;
            if (N < 0)
            {
                N = -N;
            }
            N--;
            long sum = M * x * x + sumInp2 - 2 * x * sumInp1 + (2*N-M)*x + +sumInp1 - 2 * memoSum[N-1];
            return sum / 2;
        }

        static long minim(long a, long b, long c, long d)
        {
            if (d-a<10)
            {
                long minSum = long.MaxValue;
                long tempSum = 0;
                for (long i = a; i <= d; i++)
                {
                    tempSum = fce(i);
                    //Console.Write("{0} ", tempSum);
                    if (tempSum < minSum)
                    {
                        minSum = tempSum;
                    }
                }

                return minSum;
            }
            else
            {
                if (fce(b) <= fce(c))
                {                    
                    return minim(a,a*2/3+c/3,a/3+c*2/3, c);
                }
                else
                {
                    return minim(b, (b * 2 + d) / 3, (b  + d * 2) / 3, d);
                }
            }

        }

        static long sumInp1;
        static long sumInp2;
        static long[] memo;
        static long[] memoSum;
        static long len;
        private static void stepTwo()
        {
            string line = Console.ReadLine();
            string[] lineArr = line.Split(",");
            len = lineArr.Length;
            long i;
            long input;
            long magNumber;
            memo = new long[len];
            memoSum = new long[len];
            sumInp1 = 0;
            sumInp2 = 0;
            for (i = 0; i < len; i++)
            {
                long.TryParse(lineArr[i], out input);
                memo[i] = (input);
                sumInp1 += input;
                sumInp2 += input * input;
            }
            Array.Sort(memo);
            memoSum[0] = memo[0];
            for (i = 1; i < len; i++)
            {
                memoSum[i] = memo[i] + memoSum[i - 1];
            }
            magNumber = minim(memo[0], memo[len /3], memo[len *2/3], memo[len - 1]);

            Console.WriteLine("len= {0}, mag.number= {1}.", len, magNumber);

        }
    }
}
