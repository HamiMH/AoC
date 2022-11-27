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
        long result = FirstWrongNumber(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long FirstWrongNumber(List<string> inputCol)
    {
        //for(int i = 0; i <= 6; i++)
        //{

        //    Simulator sim = new Simulator(i, inputCol);
        //    sim.Simulate();
        //    Console.WriteLine(sim.ActiveCount());
        //}

        //Simulator simulator = new Simulator(6, inputCol);
        Xmas xmas = new Xmas(inputCol);
        return xmas.FindSequenceForWrongNumber();
    }
}