
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        List<string> inputCol = new List<string>();
        string lineIn1;
        //while ((lineIn1 = Console.ReadLine()) != null)
        while ((lineIn1 = Console.ReadLine()) != "eof")
        {
            //if (lineIn1 == "")
            //    break;

            inputCol.Add(lineIn1);
        }
        Stopwatch sw = new Stopwatch();
        sw.Start();
        long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long GetResult1(List<string> inputCol)
    {
        List<long> longs = new List<long>();
        long tmp = 0;
        foreach(string str in inputCol)
        {
            if (str == "" || str == String.Empty)
            {
                longs.Add(tmp);
                tmp = 0;
                continue;
            }
            tmp+=long.Parse(str);
        }
        longs.Add(tmp);

        longs.Sort();

        return longs.Last();
    }
    private static long GetResult2(List<string> inputCol)
    {
        List<long> longs = new List<long>();
        long tmp = 0;
        foreach(string str in inputCol)
        {
            if (str == "" || str == String.Empty)
            {
                longs.Add(tmp);
                tmp = 0;
                continue;
            }
            tmp+=long.Parse(str);
        }
        longs.Add(tmp);

        longs.Sort();

        return longs[longs.Count - 3]+ longs[longs.Count - 2]+ longs[longs.Count - 1];
    }
}
