
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

class Program
{
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
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //int result = GetResult1(inputCol);
        int result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static int GetResult1(List<string> inputCol)
    {
        return GetResult(inputCol, new Rps1());
        
    }

    private static int GetResult2(List<string> inputCol)
    {
        return GetResult(inputCol, new Rps2());
    }

    private static int GetResult(List<string> inputCol, RpsBase rps)
    {
        int result = 0;
        foreach (string str in inputCol)
            result += rps.GetPoints(str);
        return result;
    }



}
