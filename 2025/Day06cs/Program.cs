using System.Diagnostics;

namespace Day06cs;

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
        List<string[]> splits = new List<string[]>();
        foreach (string line in inputCol)
        {
            string[] sp = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            splits.Add(sp);
        }
        long[] nums = new long[splits.First().Length];
        string[] opers = splits.Last();

        bool first = true;
        foreach (string[] line in splits.Take(splits.Count - 1))
        {
            for (int i = 0; i < opers.Length; i++)
            {
                if (first)
                {
                    nums[i] = long.Parse(line[i]);
                    continue;
                }
                if (opers[i] == "+")
                {
                    nums[i] += long.Parse(line[i]);
                }
                else if (opers[i] == "*")
                {
                    nums[i] *= long.Parse(line[i]);
                }
            }
            first = false;
        }


        return nums.Sum().ToString();
    }
    private static string GetResult2(List<string> inputCol)
    {
        long sum = 0;
        int nRows = inputCol.Count;
        int nCols = inputCol.First().Length;
        int i = 0;      

        bool firstRow = true;
        char oper = 'a';
        long tmpNum = 0;
        while (i<nCols)
        {
            if (firstRow)
            {
                oper = inputCol[nRows - 1][i];
                if (oper == '+')
                {
                    tmpNum = 0;
                }
                else
                {
                    tmpNum = 1;
                }
                firstRow = false;
            }
            (long num, bool wasEmpty) = GetNum(i, inputCol);
            if(wasEmpty)
            {
                sum += tmpNum;
                firstRow = true;
            }
            else
            {
                if(oper=='+')
                {
                    tmpNum += num;
                }
                else
                {
                    tmpNum *= num;
                }
            }
            i++;
        }
        sum += tmpNum;

        return sum.ToString();
    }

    private static (long, bool) GetNum(int row, List<string> inputCol)
    {
        long sum = 0;
        bool wasEmpty = true;
        for (int i = 0; i < inputCol.Count; i++)
        {
            if (row >= inputCol[i].Length)
                continue;
            char c = inputCol[i][row];
            if (char.IsDigit(c))
            {
                sum *= 10;
                sum += long.Parse(c.ToString());
                wasEmpty = false;
            }
        }
        return (sum, wasEmpty);
    }
}


