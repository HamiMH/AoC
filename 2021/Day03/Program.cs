using System;

namespace Day03
{
    class Program
    {

        static void StepOne()
        {
            int sizeOfInput = 12;
            string line;
            int[] memo = new int[sizeOfInput];
            int len = 0;
            int i;
            int x = 0;
            int y = 0;
            while ((line = Console.ReadLine()) != null)
            {
                if (line == "")
                    break;
                for (i = 0; i < sizeOfInput; i++)
                {
                    if (line[i] == '1')
                    {
                        memo[i]++;
                    }
                }

                len++;

            }
            for (i = 0; i < sizeOfInput; i++)
            {
                if (memo[sizeOfInput - 1 - i] > len / 2)
                {
                    x += (1 << i);
                }
                else
                {
                    y += (1 << i);
                }
            }

            Console.WriteLine("Multiply of x = {0} and y = {1}: {2}", x, y, x * y);
        }

        static void StepTwo()
        {
            int sizeOfInput = 12;
            string line;
            int intLine;
            int[] memo = new int[1<<(sizeOfInput+1)];
            int i;
            int x = 1;
            int y = 1;
            while ((line = Console.ReadLine()) != null)
            {
                if (line == "")
                    break;
                intLine= Convert.ToInt32("1"+line, 2);

                while (intLine > 0)
                {
                    memo[intLine]++;
                    intLine >>= 1;
                }
            }
            for (i = 0; i < sizeOfInput; i++)
            {
                x <<= 1;
                y <<= 1;
                if (memo[x | 1] >= memo[x])
                    x |= 1;

                if (memo[y | 1] < memo[y] && memo[y | 1] > 0)
                    y |= 1;
                if (memo[y] == 0)
                    y |= 1;
            }
            x &= (1 << sizeOfInput) - 1;
            y &= (1 << sizeOfInput) - 1;
            Console.WriteLine("Multiply of x = {0} and y = {1}: {2}", x, y, x * y);
        }
        static void Main(string[] args)
        {
            StepTwo();
        }
    }
}
