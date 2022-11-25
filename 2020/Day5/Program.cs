
using System.Diagnostics;

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
        long result = HighestSeat(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long HighestSeat(List<string> inputCol)
    {
        long result = 0;

        string rowStr;
        string colStr;

        int rowInt;
        int colInt;
        int rsltTemp;

        List<int> list = new List<int>();
        foreach (string inp in inputCol)
        {
            rowStr = inp.Substring(0,7);
            colStr = inp.Substring(7,3);
            rowStr = rowStr.Replace("F", "0");
            rowStr = rowStr.Replace("B", "1");
            colStr = colStr.Replace("R", "1");
            colStr = colStr.Replace("L", "0");

            rowInt = Convert.ToInt32(rowStr, 2);
            colInt = Convert.ToInt32(colStr, 2);
            rsltTemp = 8 * rowInt + colInt;

            list.Add(rsltTemp);
        }
        list.Sort();
        int len=list.Count;
        for(int i=1; i < len ; i++)
        {
            if (list[i - 1] != list[i] - 1)
                return list[i - 1] + 1;
        }


        return result;
    }
}