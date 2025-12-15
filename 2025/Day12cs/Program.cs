using System.Diagnostics;

namespace Day12cs;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> inputCol = new List<string>();
        string inp = Console.ReadLine();
        //while ((lineIn1 = Console.ReadLine()) != null)
        using (StreamReader file = new StreamReader("..\\..\\..\\" + inp + ".txt"))
        {
            string? ln;

            while ((ln = file.ReadLine()) != null)
            {
                inputCol.Add(ln);
            }
        }
        Stopwatch sw = new Stopwatch();
        sw.Start();
        string result1 = GetResult1(inputCol);
        sw.Stop();
        Console.WriteLine("Result 1:"); ;
        Console.WriteLine(result1);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");

        Console.WriteLine("");
        Console.WriteLine("");

        sw.Reset();
        sw.Start();
        string result2 = GetResult2(inputCol);
        sw.Stop();
        Console.WriteLine("Result 2:"); ;
        Console.WriteLine(result2);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static string GetResult1(List<string> inputCol)
    {
        long sum = 0;
        List<Shape> shapes = new List<Shape>();
        List<Space> spaces = new List<Space>();
        LoadInput(inputCol,shapes,spaces);
        foreach(Shape shape in shapes) shape.Init();
        
        ResolveHeuristics(shapes, spaces);
        
        foreach(Space sp in spaces)
        {
            Console.WriteLine(sp.Line);
            long res= sp.ResolveSpace(shapes);
            Console.WriteLine(res);
            sum += res;
        }

        return sum.ToString();
    }

    private static void ResolveHeuristics(List<Shape> shapes, List<Space> spaces)
    {
        long[]arr= new long[10];
        foreach (Space sp in spaces)
        {
            Console.WriteLine(sp.Line);
            long type = sp.ResolveHeuristicSpace(shapes);
            arr[type]++;
        }
        foreach (long v in arr)
        {
            Console.Write(v+", ");
        }
        Console.WriteLine();
    }

    private static void LoadInput(List<string> inputCol, List<Shape> shapes, List<Space> spaces)
    {
        Shape currShape=null;
        int index = 0;
        foreach (string line in inputCol)
        {
            if (string.IsNullOrEmpty(line))
            {
                shapes.Add(currShape);
                continue;
            }
            if (line[1] == ':')
            {
                currShape = new Shape(index);
                index++;
                continue;
            }
            if (line.Contains('.') || line.Contains('#'))
            {
                currShape.AddLine(line);
                continue;
            }
            if (line.Contains('x'))
                {
                Space sp = new Space(line);
                spaces.Add(sp);
                continue;
            }
        }
    }

    private static string GetResult2(List<string> inputCol)
    {
        long sum = 0;
        return sum.ToString();
    }
}


