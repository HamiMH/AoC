
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
        long result = GetResult1(inputCol);
        //long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long GetResult1(List<string> inputCol)
    {
        Bag bag = new Bag();
        long result = 0;

        foreach (string col in inputCol)
        {
            bag.SetContent(col);
            result+=bag.GetPrio();
        }

        return result;
    }


    private static long GetResult2(List<string> inputCol)
    {

        Bag[] bags = { new Bag() ,new Bag(), new Bag() };
        long result = 0;
        int i = 0;
        foreach (string col in inputCol)
        {
            bags[i].SetContent(col);
            i++;
            if (i == 3)
            {
                result += Bag.GetBadgePrio(bags);
                i = 0;
            }
        }

        return result;
    }


}
