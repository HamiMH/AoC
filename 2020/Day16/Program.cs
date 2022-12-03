
using System.Diagnostics;
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
        long result = ErrorRate(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long ErrorRate(List<string> inputCol)
    {
        TicketValidator tv = new TicketValidator();

        int len = inputCol.Count;
        int i = 0;
        for (; i < len; i++)
        {
            if (inputCol[i] == "" || inputCol[i] == null)
                break;
            tv.AddRule(inputCol[i]);
        }
        i++;
        i++;
        tv.AddMyTicket(inputCol[i]);
        i++;
        i++;
        i++;
        for (; i < len; i++)
        {
            if (inputCol[i] == "" || inputCol[i] == null)
                break;
            tv.AddNearbyTicket(inputCol[i]);
        }

        return tv.GetDepartRate();
    }


}