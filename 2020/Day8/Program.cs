using Day8;
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
        long result = ResultOfSim(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long ResultOfSim(List<string> inputCol)
    {
        ProgramRunner programRunner=new ProgramRunner();
        foreach (string s in inputCol)
            programRunner.AddComm(s);
        programRunner.InputOver();
        //return programRunner.ExecProgram();
        return programRunner.ReparingExec();
    }
}