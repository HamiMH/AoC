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
        long result = NumberOfBags(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long NumberOfBags(List<string> inputCol)
    {
        BagGraph bagGraph = new BagGraph();
        foreach(string s in inputCol)
        {
            bagGraph.AddEdge(s);
        }
        //return bagGraph.GetSetOfTypeOfOuterBags("shinygoldbag").Count-1;
        return bagGraph.GetNumberOfOuterBags("shinygoldbag");

    }
}