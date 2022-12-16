
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
        //string result = GetResult1(inputCol);
        string result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static string GetResult1(List<string> inputCol)
    {
        int i = 0;        
        CraneField df = new CraneField(LoadData(ref i, inputCol));
        for(; i < inputCol.Count; i++)
        {
            df.MakeMove(inputCol[i]);
        }
        return df.GetTops();
    }

    private static List<char[]> LoadData(ref int i,List<string> inputCol)
    {
        int j = 0;
        string inpLine;
        List<char[]> list = new List<char[]>();
        for (i = 0; i < inputCol.Count; i++)
        {
            inpLine = inputCol[i];
            if (inpLine == null || inpLine == "")
            {
                i++;
                break;
            }
            if (inpLine.Contains('1'))
                continue;

            char[] cLine = new char[9];
            list.Add(cLine);

            for (j = 1; j < inpLine.Length; j += 4)
            {
                char c = inpLine[j];
                if (c >= 'A' && c <= 'Z')
                    cLine[(j - 1) / 4] = c;
                else
                    cLine[(j - 1) / 4] = '.';
            }
        }
        return list;
    }

    private static string GetResult2(List<string> inputCol)
    {

        int i = 0;
        CraneField df = new CraneField(LoadData(ref i, inputCol));
        for (; i < inputCol.Count; i++)
        {
            df.MakeMoveFast(inputCol[i]);
        }
        return df.GetTops();
    }


}
