using System;
using System.Diagnostics;

namespace Day05cs;

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
        int i = 0;

        List<(long,long)> ranges = new List<(long, long)>();
        List<long> queries = new List<long>();

        for (;i < inputCol.Count; i++)
        {
            string line = inputCol[i];
            if(line.Length == 0)
            {
                i++;
                break;
            }
            string[] parts = line.Split('-');
            ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
        }
        for (; i < inputCol.Count; i++)
        {
            queries.Add(long.Parse(inputCol[i]));
        }

        foreach (long query in queries)
        {
            foreach((long,long) range in ranges)
            {
                if(query >= range.Item1 && query <= range.Item2)
                {
                    sum++;
                    break;
                }
            }
        }

        return sum.ToString();
    }
    private static string GetResult2(List<string> inputCol)
    {
        int i = 0;

        List<(long, long)> ranges = new List<(long, long)>();

        for (; i < inputCol.Count; i++)
        {
            string line = inputCol[i];
            if (line.Length == 0)
            {
                i++;
                break;
            }
            string[] parts = line.Split('-');
            ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
        }
        ranges.Sort();


        long sum = 0;
        long start = 0;
        long end = -1;
        foreach ((long r1,long r2) in ranges)
        {
            if(r1>end)
            {
                sum+= end - start + 1;
                start = r1;
                end = r2;
            }
            else
            {
                end = Math.Max(end, r2);
            }
        }
        sum += end - start + 1;

        return sum.ToString();
    }
}


