
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
        string a = Path.Combine("fsdfs", "sdf*");
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //int result = GetResult1(inputCol);
        int result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        if(sw.ElapsedMilliseconds>10000)
        Console.WriteLine("Time was: "+ sw.ElapsedMilliseconds/60000+"m," + sw.ElapsedMilliseconds/1000 + " s.");
        else
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static int GetResult1(List<string> inputCol)
    {
        CaveLayout cl = new CaveLayout(inputCol);
        FastLayout fl = new FastLayout(cl,30);

        long at = 0;
        return fl.Simulate(30);
    }


    private static int GetResult2(List<string> inputCol)
    {
        CaveLayout cl = new CaveLayout(inputCol);
        FastLayout fl = new FastLayout(cl,26);

        return fl.Simulate2(26);
    }


}
