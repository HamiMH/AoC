﻿
using Day20;
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
        //long result = GetResult1(inputCol);
        long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        if (sw.ElapsedMilliseconds > 10000)
            Console.WriteLine("Time was: " + sw.ElapsedMilliseconds / 60000 + "m," + sw.ElapsedMilliseconds / 1000 + " s.");
        else
            Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
        Console.ReadLine();
    }

    private static long GetResult1(List<string> inputCol)
    {
        DouLL dll = new DouLL(inputCol,1);
        return dll.Simulate();
    }


    private static long GetResult2(List<string> inputCol)
    {
        long dk = 811589153;
        DouLL dll = new DouLL(inputCol,dk);
        return dll.Simulate(10);
    }


}
