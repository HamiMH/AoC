using System;

namespace Day11
{
    class Program
    {
        static int [,]memo;
        static int numOfFlash;
        static int lenX = 10;
        static int lenY = 10;
        static void Main(string[] args)
        {
            int i;
            int j;
           
            int maxKrok = 100;
            numOfFlash = 0;
            memo = new int[lenY, lenX];
            char zav;
            char zavOut;
            string lineIn;
            int sum = 0;
            i = 0;
            int allFlashedAt = 0;
            Boolean notFlashe = true;
            while ((lineIn = Console.ReadLine()) != null)
            {
                if (lineIn == "")
                    break;

                for (j = 0; j < lenX; j++)
                    memo[i, j] = lineIn[j] - '0';
                i++;
            }

            //for (int krok = 0; krok <= 1000; krok++)
            
               
            //for(int krok = 0; allFlashedAt==0; krok++)
                for (int krok = 0; krok <= 1000; krok++)
                {
                    
                notFlashe = false;

                /*if (krok == 195)
                {
                    for (i = 0; i < lenY; i++)
                    {
                        for (j = 0; j < lenX; j++)
                        {
                            Console.Write("{0}", memo[i, j]);
                        }
                        Console.WriteLine("");
                    }
                Console.WriteLine("");
                }*/


                for (i = 0; i < lenY; i++) { 
                    for (j = 0; j < lenX; j++)
                    {
                        if (memo[i, j] > 0)
                            notFlashe = true;
                        memo[i, j]++;
                    }
                }

                if (allFlashedAt == 0 && notFlashe == false)
                    allFlashedAt = krok;
                zpracujFlashe();
                if(krok%1000==0)
                    Console.Write("{0}, ", krok);
            }

            Console.WriteLine("numOfFlash = {0}, allFlashedAt = {1}", numOfFlash, allFlashedAt);
        }

        private static void zpracujFlashe()
        {
            int i, j;
            Boolean bylFlash = true;
            while (bylFlash)
            {
                bylFlash = false;
                for (i = 0; i < lenY; i++)
                    for (j = 0; j < lenX; j++)
                    {
                        if (memo[i, j] >= 10)
                        {
                            bylFlash = true;
                            provedFlash(i, j);
                        }                            

                    }
            }
        }

        private static void provedFlash(int i, int j)
        {
            memo[i, j] = 0;
            numOfFlash++;
            for(int iterY=i-1; iterY <= i + 1; iterY++)
            {
                if (iterY < 0 || iterY >= lenY)
                    continue;
                for (int iterX = j - 1; iterX <= j + 1; iterX++)
                {
                    if (iterX < 0 || iterX >= lenY)
                        continue;
                    if (iterX == j && iterY == i)
                        continue;

                    if (memo[iterY, iterX] != 0)
                    {
                        memo[iterY, iterX]++;
                    }
                        if (memo[iterY,iterX] >= 10)
                    {
                        provedFlash(iterY, iterX);
                    }

                }
            }

        }
    }
}
