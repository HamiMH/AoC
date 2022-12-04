
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
        //while ((lineIn1 = Console.ReadLine()) != "eof")
        {
            if (lineIn1 == "")
                break;

            inputCol.Add(lineIn1);
        }
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //long result = GetResult1(inputCol);
        long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long GetResult1(List<string> inputCol)
    {
        Assignment ass = new Assignment();

        long result = 0;    
        foreach(string str in inputCol)
        {
            if(ass.IsOverlapping(str))
                result++;

        }

        return result;
    }


    private static long GetResult2(List<string> inputCol)
    {

        Assignment ass = new Assignment();

        long result = 0;
        foreach (string str in inputCol)
        {
            if (ass.IsPartlyOverlapping(str))
                result++;

        }

        return result;
    }


}
