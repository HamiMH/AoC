using System;

namespace Day04
{
        
    class Program
        {

        static int[][,] memo = new int[100][,];
        static int[,,] pocZmen = new int[100, 5, 2];
        static int[] numbToCall;
        static int[] completed = new int[100];
        static int numbOfMatrix = -1;
        static void StepOne()
            {
                
            int sizeOfInput = 12;
            string line;
            string[] lineArr;
            // int[][,] memo = new int[200][,];
            //int[,,] pocZmen = new int[200,5,2];
            int len = 0;
            int i;
            int j;
            int x = 0;                
            int y = 0;
            line = Console.ReadLine();
            lineArr = line.Split(",");
            numbToCall = new int[lineArr.Length];
            for(i=0;i< lineArr.Length; i++)
            {                    
                int.TryParse(lineArr[i].Trim(), out numbToCall[i]);
            }
            Console.ReadLine();
            i=5;
            while ((line = Console.ReadLine()) != null)                
            {                   
                if (line == "end")                        
                    break;
                if (line.Length < 10)
                {
                    continue;
                }
                if (i == 5)
                {
                    numbOfMatrix++;
                    memo[numbOfMatrix] = new int[5,5];
                    i = 0;                    
                }
                lineArr = line.Split(" ");
                int jj = 0;
                for (j = 0; j < 5; )
                {
                    if (lineArr[jj].Trim()=="")
                    {
                        jj++;
                        continue;
                    }
                    int.TryParse(lineArr[jj].Trim(), out memo[numbOfMatrix][i, j]);
                    j++; jj++;
                }
                    
                i++;
            }
            int outNum=gnenerujOutput();

            for (int matrixIndex = 0; matrixIndex <= numbOfMatrix; matrixIndex++)
            {
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        Console.Write(memo[matrixIndex][i, j] + " ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("");
            }

                        Console.WriteLine("outNum = {0} ", outNum);
            }

        private static int vypoctiMatici(int indexToCallMax, int matrixIndex)
        {
            int sumOut = 0;
            for (int indexToCall = 0; indexToCall <= indexToCallMax; indexToCall++)
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                    {
                        if (memo[matrixIndex][i, j] == numbToCall[indexToCall])
                        {
                            memo[matrixIndex][i, j] = 0;
                        }
                    }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    sumOut += memo[matrixIndex][i, j];
            return sumOut* numbToCall[indexToCallMax];
        }

        private static int gnenerujOutput()
        {
            for (int indexToCall = 0; indexToCall <= numbToCall.Length; indexToCall++)
                for (int matrixIndex = 0; matrixIndex <= numbOfMatrix; matrixIndex++)
                    for (int i = 0; i < 5; i++)
                        for (int j = 0; j < 5; j++)
                        {
                            if (memo[matrixIndex][i, j] == numbToCall[indexToCall])
                            {
                                pocZmen[matrixIndex, i, 0]++;
                                pocZmen[matrixIndex, j, 1]++;
                            }
                            if (pocZmen[matrixIndex, i, 0] == 5 || pocZmen[matrixIndex, j, 1] == 5)
                            {
                                completed[matrixIndex] = 1;
                                int vseCompleted = 1;
                                for (int k = 0; k<= numbOfMatrix; k++)
                                {
                                    if (completed[k] == 0)
                                    {
                                        vseCompleted = 0;
                                        break;
                                    }
                                    

                                }
                                if (vseCompleted == 1)
                                    return vypoctiMatici(indexToCall, matrixIndex);
                            }
                            
                        }

            return 0;
        }

        static void Main(string[] args)
            {
                StepOne();
            }
        }

        
    }
