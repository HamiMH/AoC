
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
        //int result = GetResult1(inputCol);
        int result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static int GetResult1(List<string> inputCol)
    {
        SandFallSim sfs = new SandFallSim(inputCol);
        return sfs.Simulate();
    }


    private static int GetResult2(List<string> inputCol)
    {
        Stopwatch sw = new Stopwatch();
        SandFallSim sfs = new SandFallSim(inputCol,true);
        int result = sfs.Simulate()+1;
        return result;
    }


}
