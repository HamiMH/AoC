
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

class Program
{
    private static int numbOfMaxs = 3;
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
        long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long GetResult1(List<string> inputCol)
    {
        long tmp = 0;
        long max = 0;
        foreach(string str in inputCol)
        {
            if (str == "" || str == String.Empty)
            {
                SetMax(ref max, tmp);
                tmp = 0;
                continue;
            }
            tmp+=long.Parse(str);
        }
        SetMax(ref max, tmp);

        return max;
    }


    private static long GetResult2(List<string> inputCol)
    {
        long tmp = 0;
        long[] max = new long[numbOfMaxs];
        foreach (string str in inputCol)
        {
            if (str == "" || str == String.Empty)
            {
                SetMax(ref max, tmp);
                tmp = 0;
                continue;
            }
            tmp += long.Parse(str);
        }
        SetMax(ref max, tmp);

        return max.Sum();
    }

    private static void SetMax(ref long max, long tmp)
    {
        if(tmp>max)
            max = tmp;
    }

    private static void SetMax(ref long[] maxArr, long tmp)
    {
        long minVal = maxArr[0];
        int minIndex = 0;
        for(int i = 1;i< numbOfMaxs; i++)
        {
            if (maxArr[i] < minVal)
            {
                minVal = maxArr[i];
                minIndex = i;
            }
        }
        if (tmp > minVal)
            maxArr[minIndex] = tmp;
    }

}
