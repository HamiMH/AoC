using System.Diagnostics;

namespace Day04cs;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> inputCol = new List<string>();
        string inp = Console.ReadLine();
        //while ((lineIn1 = Console.ReadLine()) != null)
        using (StreamReader file = new StreamReader("..\\..\\..\\" + inp + ".txt"))
        {
            string? ln;

            while ((ln = file.ReadLine()) != null)
            {
                inputCol.Add(ln);
            }
        }
        Stopwatch sw = new Stopwatch();
        sw.Start();
        string result1 = GetResult1(inputCol);
        sw.Stop();
        Console.WriteLine("Result 1:"); ;
        Console.WriteLine(result1);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");

        Console.WriteLine("");
        Console.WriteLine("");

        sw.Reset();
        sw.Start();
        string result2 = GetResult2(inputCol);
        sw.Stop();
        Console.WriteLine("Result 2:"); ;
        Console.WriteLine(result2);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static string GetResult1(List<string> inputCol)
    {
        long sum = 0;
        bool[,] map = new bool[inputCol.Count, inputCol.First().Length];
        int i = 0;
        int j = 0;

        foreach (string line in inputCol)
        {
            j = 0;
            foreach (char c in line)
            {
                if (c == '@')
                {
                    map[i, j] = true;
                }
                j++;
            }
            i++;
        }

        for (i = 0; i < inputCol.Count; i++)
        {
            for (j = 0; j < inputCol.First().Length; j++)
            {
                if (map[i, j])
                {
                    int neighbors = GetNumOfNeigbors(i, j, map);
                    if (neighbors < 4)
                        sum++;
                }
            }
        }

        return sum.ToString();
    }

    private static int GetNumOfNeigbors(int row, int col, bool[,] map)
    {
        int nOfNeighbors = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int newRow = row + i;
                int newCol = col + j;
                if (newRow >= 0 && newRow < map.GetLength(0) && newCol >= 0 && newCol < map.GetLength(1) && map[newRow, newCol])
                {
                    nOfNeighbors++;
                }
            }
        }
        return nOfNeighbors;
    }

    private static string GetResult2(List<string> inputCol)
    {

        long sum = 0;
        bool wasChanged = true;


        bool[,] map = new bool[inputCol.Count, inputCol.First().Length];
        int i = 0;
        int j = 0;

        foreach (string line in inputCol)
        {
            j = 0;
            foreach (char c in line)
            {
                if (c == '@')
                {
                    map[i, j] = true;
                }
                j++;
            }
            i++;
        }

        while (wasChanged)
        {
            wasChanged = false;


            for (i = 0; i < inputCol.Count; i++)
            {
                for (j = 0; j < inputCol.First().Length; j++)
                {
                    if (map[i, j])
                    {
                        int neighbors = GetNumOfNeigbors(i, j, map);
                        if (neighbors < 4)
                        {
                            map[i, j] = false;
                            wasChanged = true;
                            sum++;
                        }
                    }
                }
            }
        }

        return sum.ToString();
    }
}


