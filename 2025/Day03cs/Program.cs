using System.Diagnostics;

namespace Day03cs;

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

    private static long ResolveLine(string line, int finalLen)
    {
        long[] arr = new long[finalLen];
        for (int i = 0; i < line.Length; i++)
        {
            int startIndex = int.Max(finalLen - (line.Length - i), 0);
            long tmpVal = line[i] - '0';
            bool lookForPlace = true;
            for(int j=startIndex;j<finalLen;j++)
            {
                if (lookForPlace)
                {
                    if(tmpVal > arr[j])
                    {
                        arr[j] = tmpVal;
                        lookForPlace = false;
                    }
                }
                else
                {
                    arr[j] = 0;
                }
            }
        }

        long result = 0;
        foreach(long l in arr)
        {
            result = result * 10 + l;
        }
        return result;
    }

    private static string GetResult1(List<string> inputCol)
    {
        long sum = 0;
        foreach (string s in inputCol)
        {
            sum += ResolveLine(s, 2);
        }
        return sum.ToString();
    }
    private static string GetResult2(List<string> inputCol)
    {
        long sum = 0;
        foreach (string s in inputCol)
        {
            sum += ResolveLine(s, 12);
        }
        return sum.ToString();
    }
}


