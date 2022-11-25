using Day17;
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
        long result = NumberOfActiveReactors(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long NumberOfActiveReactors(List<string> inputCol)
    {
        //for(int i = 0; i <= 6; i++)
        //{

        //    Simulator sim = new Simulator(i, inputCol);
        //    sim.Simulate();
        //    Console.WriteLine(sim.ActiveCount());
        //}

        //Simulator simulator = new Simulator(6, inputCol);
        Simulator4dHs simulator = new Simulator4dHs(6, inputCol);
        simulator.Simulate();
        return simulator.ActiveCount();
    }
}