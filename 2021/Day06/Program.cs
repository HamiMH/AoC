using System;
namespace Day06
{
   
    class Program
    {

        public static long[,] MultiplyMatrix(long[,] A, long[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);
            long temp = 0;
            long[,] kHasil = new long[rA, cB];
            if (cA != rB)
            {
                Console.WriteLine("matrik can't be multiplied !!");
                return kHasil;
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }
                return kHasil;
            }
        }
        static long[] memo;
        static long[,] jednMatice={
            {1, 0, 1, 0, 0, 0, 0, 0, 0}, {0, 1, 0, 1, 0, 0, 0, 0, 0}, 
            {0, 0, 1, 0, 1, 0, 0, 0, 0}, {0, 0, 0, 1, 0, 1, 0, 0, 0}, 
            {0, 0, 0, 0, 1, 0, 1, 0, 0}, {1, 0, 0, 0, 0, 1, 0, 1, 0}, 
            {0, 1, 0, 0, 0, 0, 1, 0, 1}, {1, 0, 0, 0, 0, 0, 0, 1, 0}, 
            {0, 1, 0, 0, 0, 0, 0, 0, 1}};
        static void Main(string[] args)
        {
            int memLen = 9;
            int pocKroku = 256;
            memo = new long[memLen];
            long sum = 0;
            string line = Console.ReadLine();
            string []lineArr = line.Split(",");
            int len=lineArr.Length;
            int i;
            int input;
            for (i = 0; i < len; i++)
            {
                int.TryParse(lineArr[i], out input);
                memo[input]++;
            }

            pocKroku = zpracujRychle(pocKroku);

            for (i = 0; i < pocKroku; i++)
            {
                memo[(i + 7) % memLen] += memo[i % memLen];
            }

            for (i = 0; i < memLen; i++)
            {
                sum +=memo[i];
            }
            Console.WriteLine("");
            Console.WriteLine("PocKroku: {0}, Suma: {1}", pocKroku, sum);
        }

        private static int zpracujRychle(int pocKroku)
        {
            int pocRychlKroku = pocKroku / 9;

            long[,] multipMat = new long[9, 9];

            multipMat= vratVyslednouMatici(pocRychlKroku);
            long[] newMemo = new long[9];

            for(int i = 0; i<9; i++)
                for (int j = 0; j < 9; j++)
                {
                    newMemo[i] += multipMat[i, j]*memo[j];
                }

            memo = newMemo;                    
            return pocKroku % 9;
        }

        private static long[,] vratVyslednouMatici(int pocRychlKroku)
        {
            if (pocRychlKroku == 1)
            {
                return jednMatice;
            }
            if ((pocRychlKroku & 1) == 1)
            {
                return MultiplyMatrix(jednMatice, vratVyslednouMatici(pocRychlKroku - 1));
            }
            else
            {
                return MultiplyMatrix(vratVyslednouMatici(pocRychlKroku / 2), vratVyslednouMatici(pocRychlKroku /2));
            }
        }
    }
}
