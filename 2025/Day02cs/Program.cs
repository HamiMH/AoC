using System.Diagnostics;

namespace Day02cs;

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
        long sum = 0;
        string[] parts = inputCol[0].Split(',');
        foreach (string s in parts)
        {
            string si = s.Trim();
            string[] be = si.Split('-');
            long a = long.Parse(be[0]);
            long b = long.Parse(be[1]);
            for (long i = a; i <= b; i++)
            {
                string num = i.ToString();
                if (num.Length % 2 == 1)
                {
                    continue;
                }
                if (num.Substring(0, num.Length / 2) == num.Substring(num.Length / 2))
                {
                    sum += i;
                }
            }
        }

        return sum.ToString();
    }

    private static bool NumberIsValid(string num)
    {
        for (int j = 2; j <= num.Length; j++)
        {
            if (num.Length % j == 1)
            {
                continue;
            }
            int step = num.Length / j;

            bool allOk = true;
            for (int k = 0; k < num.Length - step; k += step)
            {
                if (num.Substring(k, step) != num.Substring(k + step, step))
                {
                    allOk = false;
                    break;
                }
            }
            if(allOk)
            {
                return true;
            }
        }
        return false;
    }
    private static string GetResult2(List<string> inputCol)
    {
        long sum = 0;
        string[] parts = inputCol[0].Split(',');
        foreach (string s in parts)
        {
            string si = s.Trim();
            string[] be = si.Split('-');
            long a = long.Parse(be[0]);
            long b = long.Parse(be[1]);
            for (long i = a; i <= b; i++)
            {
                string num = i.ToString();
                if(NumberIsValid(num))
                {
                    sum += i;
                }                
            }
        }

        return sum.ToString();
    }
}


