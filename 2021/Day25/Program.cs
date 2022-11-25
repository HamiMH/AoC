using System;
using System.Collections.Generic;

namespace Day25
{
    class Program
    {
        static int lenX = 0;
        static int lenY = 0;
        static char[,,] memo;
        static int nowIndex = 0;
        static int nextIndex = 1;
        static void Main(string[] args)
        {
            List<string> inputCol = new List<string>();
            string lineIn1;
            while ((lineIn1 = Console.ReadLine()) != null)
            {
                if (lineIn1 == "")
                    break;
                inputCol.Add(lineIn1);
            }

            int i, j;
            lenY = inputCol.Count;
            lenX = inputCol[0].Length;
            memo = new char[2, lenY, lenX];

            for (i = 0; i < lenY; i++)
                for (j = 0; j < lenX; j++)
                    memo[nowIndex, i, j] = inputCol[i][j];

            int changes = 1;
            int stepCount = 0;
            while (changes > 0)
            {
                changes = 0;

                changes+=stepX();
                nowIndex ^= 1;
                nextIndex ^= 1;
                changes +=stepY();
                nowIndex ^= 1;
                nextIndex ^= 1;
            
                stepCount++;

                /*for (i = 0; i < lenY; i++)
                {
                    for (j = 0; j < lenX; j++)
                        Console.Write("{0}", memo[0,i,j]);
                }
                Console.WriteLine("");*/
            }
                    
            Console.WriteLine("stepCount = {0}", stepCount);
        }

        private static int stepX()
        {
            int numOfChanges = 0;
            int i, j;
            for (i = 0; i < lenY; i++)
                for (j = 0; j < lenX; j++)
                    memo[nextIndex, i, j] = '.';

            for (i = 0; i < lenY; i++)
                for (j = 0; j < lenX; j++)
                {
                    if (memo[nowIndex, i, j] == '>')
                    {
                        if (memo[nowIndex, i, nextX(j)] == '.')
                        {
                            memo[nextIndex, i, nextX(j)] = '>';
                            numOfChanges++;
                        }
                        else
                        {
                            memo[nextIndex, i, j] = '>';
                        }
                    }else if (memo[nowIndex, i, j] == 'v')
                        memo[nextIndex, i, j] = 'v';

                }

            return numOfChanges;
        }

        private static int stepY()
        {
            int numOfChanges = 0;
            int i, j;
            for (i = 0; i < lenY; i++)
                for (j = 0; j < lenX; j++)
                    memo[nextIndex, i, j] = '.';

            for (i = 0; i < lenY; i++)
                for (j = 0; j < lenX; j++)
                {
                    if (memo[nowIndex, i, j] == 'v')
                    {
                        if (memo[nowIndex, nextY(i), j] == '.')
                        {
                            memo[nextIndex, nextY(i), j] = 'v';
                            numOfChanges++;
                        }
                        else
                        {
                            memo[nextIndex, i, j] = 'v';
                        }
                    }
                    else if (memo[nowIndex, i, j] == '>')
                        memo[nextIndex, i, j] = '>';

                }

            return numOfChanges;
        }

        private static int nextX(int nowX)
        {
            int nextX = nowX + 1;
            nextX %= lenX;
            return nextX;
        }
        private static int nextY(int nowY)
        {
            int nextY = nowY + 1;
            nextY %= lenY;
            return nextY;
        }
    }
}
