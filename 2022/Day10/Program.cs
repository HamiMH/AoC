
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
        //int result = GetResult1(inputCol);
        string result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static int GetResult1(List<string> inputCol)
    {
        Simulator sim = new Simulator();
        foreach(string str in inputCol)
        {
            sim.ExecCommand(str);
        }
        return sim.GetTotalScore() ;
    }


    private static string GetResult2(List<string> inputCol)
    {

        Simulator sim = new Simulator();
        foreach (string str in inputCol)
        {
            sim.ExecCommand(str);
        }
        return sim.GetRastr();
    }


}
