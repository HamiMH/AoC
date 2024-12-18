﻿
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
        //long result = GetResult1(inputCol);
        long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long GetResult1(List<string> inputCol)
    {
        HeadTailSimulator headTailSimulator = new HeadTailSimulator();
        foreach(string str in inputCol)
            headTailSimulator.MakeMove(str);

        return headTailSimulator.NofAttended(); ;
    }


    private static long GetResult2(List<string> inputCol)
    {

        SnakeSimulator headTailSimulator = new SnakeSimulator();

        foreach (string str in inputCol)
            headTailSimulator.MakeMove(str);

        return headTailSimulator.NofAttended(); ;
    }


}
