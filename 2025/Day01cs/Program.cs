using System.Diagnostics;

namespace Day01cs;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> inputCol = new List<string>();
        string inp = Console.ReadLine();
        //while ((lineIn1 = Console.ReadLine()) != null)
        using (StreamReader file = new StreamReader("..\\..\\..\\" + inp + ".txt"))
        {
            string? ln;

            while ((ln = file.ReadLine()) != null)
            {
                inputCol.Add(ln);
            }
        }
        Stopwatch sw = new Stopwatch();
        sw.Start();
        string result1 = GetResult1(inputCol);
        sw.Stop();
        Console.WriteLine("Result 1:"); ;
        Console.WriteLine(result1);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");

        Console.WriteLine("");
        Console.WriteLine("");

        sw.Reset();
        sw.Start();
        string result2 = GetResult2(inputCol);
        sw.Stop();
        Console.WriteLine("Result 2:"); ;
        Console.WriteLine(result2);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static string GetResult1(List<string> inputCol)
    {
        int currVal = 50;
        long count = 0;
        foreach (string s in inputCol)
        {
            string beg = s.Substring(0, 1);
            string end = s.Substring(1);
            int turnVal = int.Parse(end) % 100;

            if (beg == "L")
            {
                currVal -= turnVal;
                if (currVal < 0) currVal += 100;

            }
            else if (beg == "R")
            {
                currVal += turnVal;
                currVal %= 100;
            }
            if (currVal == 0) count++;
        }

        return count.ToString();
    }
    private static string GetResult2(List<string> inputCol)
    {
        int currVal = 50;
        long count = 0;
        foreach (string s in inputCol)
        {
            int multip ;
            if(currVal ==0)
            {
                multip = 0;
            }
            else
            {
                multip = 1;
            }
            string beg = s.Substring(0, 1);
            string end = s.Substring(1);
            int wholeTurnVal = int.Parse(end);
            int turnVal = wholeTurnVal % 100;
            count+= wholeTurnVal / 100;

            if (beg == "L")
            {
                currVal -= turnVal;
                if (currVal < 0)
                {
                    currVal += 100;
                    count+=multip;
                }

            }
            else if (beg == "R")
            {
                currVal += turnVal;
                if (currVal > 100)
                {
                    currVal -= 100;
                    count += multip;
                }
                if (currVal == 100)
                {
                    currVal -= 100;
                }
            }
            if (currVal == 0)
                count += multip; 
        }

        return count.ToString();
    }
}


