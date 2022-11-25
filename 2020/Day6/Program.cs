
using System.Diagnostics;
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
        long result = NumberOfGroups(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long NumberOfGroups(List<string> inputCol)
    {
        long result = 0;
        ICounter counter = new Counter2();
        List<Grup> grups = new List<Grup>();
        Grup gr = new Grup(counter);
        grups.Add(gr);
        foreach (string? str in inputCol)
        {
            if (str == null || str == "")
            {
                counter = new Counter2();
                gr = new Grup(counter);
                grups.Add(gr);
                continue;
            }
            gr.AddData(str);
        }

        foreach (Grup iter in grups)
        {
           
                result += iter.GrCount();

        }
        return result;
    }
}
