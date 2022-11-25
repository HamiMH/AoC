using System;
using System.Collections;

namespace Day01
{
    class Program
    {
        static void StepOne()
        {
            int oldVal = int.MaxValue;
            int numberOfInc = 0;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int nowVal))
                {
                    break;
                }
                else
                {
                    if (nowVal > oldVal)
                    {
                        numberOfInc++;
                    }
                    oldVal = nowVal;
                }
            }
            Console.WriteLine("Number of increses: {0}", numberOfInc);
        }

        static void StepTwo()
        {
            int oldVal;
            int newVal;
            int iter = 0;
            //ArrayList arList = new ArrayList();
            int[] arList = new int[10000];
            int numberOfInc = 0;
            int arListLen = 0;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int val))
                {
                    break;
                }
                else
                {
                    arList[iter] = val;
                    arListLen++;
                    iter++;
                }
            }
            oldVal = arList[0] + arList[1] + arList[2];
            newVal = arList[1] + arList[2] + arList[3];
            if (newVal > oldVal)
                numberOfInc++;

            for (iter = 4; iter < arListLen; iter++)
            {
                oldVal = oldVal + arList[iter - 1] - +arList[iter - 4];
                newVal = newVal + arList[iter] - +arList[iter - 3];
                if (newVal > oldVal)
                    numberOfInc++;

            }


            Console.WriteLine("Number of increses: {0}", numberOfInc);
        }

        static void StepTwov2()
        {
            int iter = 0;
            int[] arList = new int[10000];
            int numberOfInc = 0;
            int arListLen = 0;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int val))
                {
                    break;
                }
                else
                {
                    arList[iter] = val;
                    iter++;
                }
            }

            arListLen = iter;

            for (iter = 3; iter < arListLen; iter++)
            {
                if (arList[iter] > arList[iter - 3])
                    numberOfInc++;

            }


            Console.WriteLine("Number of increses: {0}", numberOfInc);
        }

        static void StepTwov3()
        {
            int iter = 0;
            int[] arList = { -1,-1,-1};
            int numberOfInc = 0;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int val))
                {
                    break;
                }
                else
                {
                    if (arList[iter] != -1 && val> arList[iter])
                    {
                        numberOfInc++;
                    }
                    arList[iter] = val;
                    iter++;
                    iter=iter%3;
                }
            }
            Console.WriteLine("Number of increses: {0}", numberOfInc);
        }
        static void Main(string[] args)
        {
            StepOne();
            //StepTwov3();
        }
    }
}
