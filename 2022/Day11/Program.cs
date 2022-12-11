
using Day11;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text;
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
        //long result = GetResult1(inputCol);
        long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long GetResult1(List<string> inputCol)
    {
        StringBuilder stb= new StringBuilder();
        foreach(string str in inputCol)
        {
            if(str=="" || str==String.Empty)
            {
                Monkey.MonkeyList.Add(new Monkey(stb.ToString()));
                stb.Clear();
                continue;
            }
            stb.Append(str);
            stb.Append(";");
        }
        Monkey.MonkeyList.Add(new Monkey(stb.ToString()));
        stb.Clear();

        for (int i = 0; i < 20; i++)
            foreach (Monkey monkey in Monkey.MonkeyList)
                monkey.PlayWithItems();


        return Monkey.Biggest2();
    }


    private static long GetResult2(List<string> inputCol)
    {
        StringBuilder stb = new StringBuilder();
        foreach (string str in inputCol)
        {
            if (str == "" || str == String.Empty)
            {
                Monkey.MonkeyList.Add(new Monkey(stb.ToString()));
                stb.Clear();
                continue;
            }
            stb.Append(str);
            stb.Append(";");
        }
        Monkey.MonkeyList.Add(new Monkey(stb.ToString()));
        stb.Clear();

        for (int i = 0; i < 10000; i++)
            foreach (Monkey monkey in Monkey.MonkeyList)
                monkey.PlayWithItems();


        return Monkey.Biggest2();
    }


}
