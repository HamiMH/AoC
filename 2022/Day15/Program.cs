
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
        int result = GetResult1(inputCol);
        //long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static int GetResult1(List<string> inputCol)
    {
        BeaconMap bm = new BeaconMap(inputCol);

        return bm.GetEmpty(2000000);
    }


    private static long GetResult2(List<string> inputCol)
    {
        BeaconMap bm = new BeaconMap(inputCol);

        //return bm.GetValOfFree(20);
        return bm.GetValOfFree(4000000);
    }


}
