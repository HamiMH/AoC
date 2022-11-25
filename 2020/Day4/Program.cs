
using Day4;
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
        long result = CountValidPassPorts(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: "+sw.ElapsedMilliseconds+" ms.");
    }

    private static long CountValidPassPorts(List<string> inputCol)
    {
        long result= 0;
        IValidator validator = new Validator2();
        List<PassPort> passPorts = new List<PassPort>();
        PassPort pp = new PassPort(validator);
        passPorts.Add(pp);
        foreach (string? str in inputCol)
        {
            if(str ==null || str == "")
            {
                pp=new PassPort(validator);
                passPorts.Add(pp);
                continue;
            }
            pp.AddData(str);
        }

        foreach(PassPort iter in passPorts)
        {
            if (iter.PassPortIsValid())
                result++;
                
        }
        return result;
    }
}