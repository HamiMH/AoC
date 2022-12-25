

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
        string result = GetResult1(inputCol);
        //long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        if (sw.ElapsedMilliseconds > 10000)
            Console.WriteLine("Time was: " + sw.ElapsedMilliseconds / 60000 + "m," + sw.ElapsedMilliseconds / 1000 + " s.");
        else
            Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
        Console.ReadLine();
    }

    private static string GetResult1(List<string> inputCol)
    {
        Numbers nu = new Numbers(inputCol);
        //mm.PreProcessingOfMapV01();
        return nu.Sum5();
        //return 0;
    }


    private static string GetResult2(List<string> inputCol)
    {
        Numbers nu = new Numbers(inputCol);
        //mm.PreProcessingOfMapV01();
        return nu.Sum5();


    }
}
