using System.Diagnostics;

namespace Day09cs;

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
        List<(long, long)> corners = new List<(long, long)>();
        foreach (string col in inputCol)
        {
            string[] parts = col.Split(',');
            long x = long.Parse(parts[0]);
            long y = long.Parse(parts[1]);
            corners.Add((x, y));
        }

        long sum = 0;
        for (int i = 0; i < corners.Count; i++)
        {
            (long x0, long y0) = corners[i];
            for (int j = i + 1; j < corners.Count; j++)
            {
                (long x1, long y1) = corners[j];
                sum = Math.Max(sum, (Math.Abs(x0 - x1) + 1) * (Math.Abs(y0 - y1) + 1));
            }
        }
        return sum.ToString();
    }
    private static string GetResult2(List<string> inputCol)
    {
        List<(long, long)> corners = new List<(long, long)>();
        foreach (string col in inputCol)
        {
            string[] parts = col.Split(',');
            long x = long.Parse(parts[0]);
            long y = long.Parse(parts[1]);
            corners.Add((x, y));
        }

        Polynom pol = new Polynom(corners);
        List<Line> lines = new();
        for (int i = 0; i < corners.Count; i++)
        {
            (long x0, long y0) = corners[i];
            (long x1, long y1) = corners[(i + 1) % corners.Count];
            lines.Add(new Line(x0, y0, x1, y1));
        }

        long sum = 0;
        for (int i = 0; i < corners.Count; i++)
        {
            (long x0, long y0) = corners[i];
            for (int j = i + 1; j < corners.Count; j++)
            {
                (long x1, long y1) = corners[j];
                bool isOk = TestSq(corners[i], corners[j], pol);
                if (!isOk)
                    continue;
                long size = (Math.Abs(x0 - x1) + 1) * (Math.Abs(y0 - y1) + 1);
                sum = Math.Max(sum, size);
            }
        }
        return sum.ToString();
    }

    private static bool TestSq((long, long) value1, (long, long) value2, Polynom pol)
    {
        (long x0, long y0) = value1;
        (long x1, long y1) = value2;

        Line lineA = new Line(x0, y0, x0, y1);
        Line lineB = new Line(x0, y1, x1, y1);
        Line lineC = new Line(x1, y1, x1, y0);
        Line lineD = new Line(x1, y0, x0, y0);

        foreach (var line in new Line[] { lineA, lineB, lineC, lineD })
        {
            if (pol.IntersectWithAny(line))
                return false;
        }
        foreach ((long x, long y) in new (long, long)[] { (x0, y0), (x0, y1), (x1, y1), (x1, y0) })
        {
            if (pol.PointIsOnLine((x, y)))
                continue;
            if (!pol.PointIsInside((x, y)))
                return false;
        }
        return true;
    }
}


