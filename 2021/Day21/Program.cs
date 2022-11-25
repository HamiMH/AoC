using System;
using System.Numerics;

namespace Day20
{
    class Program
    {
        static void v01(string line1, string line2)
        {
            int[] plerIndex = new int[2];
            int[] plerScore = { 0, 0 };
            plerIndex[0] = Int32.Parse(line1.Split(" ")[4]);
            plerIndex[1] = Int32.Parse(line2.Split(" ")[4]);
            int i, index, moveVal;
            index = 0;
            for (i = 1; ; i += 3)
            {
                plerIndex[index] += (3 * i) + 3;
                plerIndex[index] = (plerIndex[index] - 1) % 10 + 1;
                plerScore[index] += plerIndex[index];

                if (plerScore[index] >= 1000)
                    break;
                index ^= 1;
            }
            i += 2;
            index ^= 1;
            Console.WriteLine("index = {0}, playScore = {1}, output = {2}", i, plerScore[index], plerScore[index] * i);
        }

        static long[] addVector(long[] mat1, long[] mat2)
        {
            long[] matNew = { 0, 0 };

            matNew[0] = mat1[0] + mat2[0];
            matNew[1] = mat1[1] + mat2[1];
            return matNew;
        }

        static long count0 = 0;
        static long count1 = 0;
        static long scoreToStop = 21;
        static long[,,,,,] memo = new long[2, 11, 11, 22, 22,2];
        static long[] recursion(long[] positArr, long[] scoreArr, int index, int depth, string str)
        {

            if (depth == 0)
                if (memo[index, positArr[0], positArr[1], scoreArr[0], scoreArr[1], 0] != -1)
                {
                    long[] ret = { memo[index, positArr[0], positArr[1], scoreArr[0], scoreArr[1], 0], memo[index, positArr[0], positArr[1], scoreArr[0], scoreArr[1], 1] };
                    return ret;
                }

          
            if (depth == 3)
            {
                long[] scoreArrPrac = new long[2];
                Array.Copy(scoreArr, scoreArrPrac, 2);

                scoreArrPrac[index] = scoreArr[index] + positArr[index];

                if (scoreArrPrac[0] >= scoreToStop)
                {
                    count0++;
                    long[] vecRet = { 1, 0 };
                    return vecRet;
                }
                if (scoreArrPrac[1] >= scoreToStop)
                {
                    count1++;
                    long[] vecRet = { 0, 1 };
                    return vecRet;
                }

                return recursion(positArr, scoreArrPrac, index ^ 1, 0, str + "^");
            }

            long[] retArr = { 0, 0 };
            long[] prac;
            for (long i = 1; i <= 3; i++)
            {
                long[] tempPos = { 0, 0 };
                tempPos[index] = i;
                tempPos = addVector(tempPos, positArr);
                tempPos[index] = (tempPos[index] - 1) % 10 + 1;


                prac = recursion(tempPos, scoreArr, index, depth + 1, str + "" + i);
                retArr = addVector(retArr, prac);
            }

            if (depth == 0) { 
                memo[index, positArr[0], positArr[1], scoreArr[0], scoreArr[1], 0] = retArr[0];            
                memo[index, positArr[0], positArr[1], scoreArr[0], scoreArr[1], 1] = retArr[1];
            }
            return retArr;

        }

        static long v02(string line1, string line2)
        {
            long[] plerIndex = new long[2];
            long[] score = { 0, 0 };
            long[] output = new long[2];
            plerIndex[0] = Int64.Parse(line1.Split(" ")[4]);
            plerIndex[1] = Int64.Parse(line2.Split(" ")[4]);

            int i, j, k, l, m,n;
            for (i = 0; i < 2; i++)
                for (j = 0; j < 11; j++)
                    for (k = 0; k < 11; k++)
                        for (l = 0; l < 22; l++)
                            for (m = 0; m < 22; m++)
                                for (n = 0; n < 2; n++)
                                    memo[i, j, k, l, m,n] = -1;
                                
            output = recursion(plerIndex, score, 0, 0, "");

            //Console.WriteLine("output[0] = {0}, output[1] = {1}", output[0], output[1]);
            //Console.WriteLine("count0 = {0}, count1 = {1}", count0, count1);

            if(output[0] > output[1])
            {
                return output[0];
            }else
            {
                return output[1];
            }
        }
        static void Main(string[] args)
        {
            string line1 = Console.ReadLine();
            string line2 = Console.ReadLine();
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            //v01(line1, line2);
            long result =v02(line1, line2);

            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Vysledek = {0}", result);
        }
    }
}
