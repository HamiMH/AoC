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
        return sum.ToString();
    }

    private static void LoadInput(List<string> inputCol, List<Shape> shapes, List<Space> spaces)
    {
        Shape currShape=null;
        foreach (string line in inputCol)
        {
            if (string.IsNullOrEmpty(line))
            {
                shapes.Add(currShape);
                continue;
            }
            if (line[1] == ':')
            {
                currShape = new Shape();
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


