using System.Diagnostics;

namespace Day08cs;

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
        List<Box> boxes = new List<Box>();
        int index= 0;
        foreach (string col in inputCol)
        {
            string[] parts = col.Split(',');
            Box box = new Box();
            box.Id = index;
            box.X = long.Parse(parts[0]);
            box.Y = long.Parse(parts[1]);
            box.Z = long.Parse(parts[2]);
            boxes.Add(box);
            index++;
        }

        List<(long,int,int)> dists = new List<(long, int, int)>();
        for (int i = 0 ; i < boxes.Count; i++)
        {
            Box boxA = boxes[i];
            for(int j = i + 1; j < boxes.Count; j++)
            {
                Box boxB = boxes[j];
                dists.Add((boxA.DistanceSqTo(boxB), boxA.Id, boxB.Id));
            }
        }
        dists.Sort();

        int numConnections = 0;
        foreach (var dist in dists.Take(1000))
        {
            //if(numConnections >= 10)
            //{
            //    break;
            //}
            Box boxA = boxes[dist.Item2];
            Box boxB = boxes[dist.Item3];

            if(!PathExist(boxA.Id,boxB.Id,boxes, new HashSet<int>()))
            {
                boxA.Neigh.Add(boxB.Id);
                boxB.Neigh.Add(boxA.Id);
                numConnections++;
            }
        }

        List<long> sizes= new List<long>();
        HashSet<int> visited = new HashSet<int>();
        for(int i=0; i< boxes.Count; i++)
        {
            if(visited.Contains(i))
            {
                continue;
            }
            long size = Search(boxes, i, visited);
            sizes.Add(size);
        }
        sizes.Sort();
        long sum = sizes.TakeLast(3).Aggregate(1L, (a, b) => a * b);

        return sum.ToString();
    }

    private static bool PathExist(int fromId, int finalId, List<Box> boxes, HashSet<int> visited)
    {
        if(fromId == finalId)
        {
            return true;
        }
        if(visited.Contains(fromId))
        {
            return false;
        }
        visited.Add(fromId);
        bool found= false;
        foreach (int boxId in boxes[fromId].Neigh)
        {
            found |= PathExist(boxId, finalId, boxes, visited);
        }
        return found;
    }

    private static long Search(List<Box> boxes, int i, HashSet<int> visited)
    {
        if(visited.Contains(i))
        {
            return 0;
        }
        visited.Add(i);


        long sum= 1;
        foreach (int neighId in boxes[i].Neigh)
        {
            sum+=Search(boxes, neighId, visited);
        }
        return sum;
    }

    private static string GetResult2(List<string> inputCol)
    {
        List<Box> boxes = new List<Box>();
        int index = 0;
        foreach (string col in inputCol)
        {
            string[] parts = col.Split(',');
            Box box = new Box();
            box.Id = index;
            box.X = long.Parse(parts[0]);
            box.Y = long.Parse(parts[1]);
            box.Z = long.Parse(parts[2]);
            boxes.Add(box);
            index++;
        }

        List<(long, int, int)> dists = new List<(long, int, int)>();
        for (int i = 0; i < boxes.Count; i++)
        {
            Box boxA = boxes[i];
            for (int j = i + 1; j < boxes.Count; j++)
            {
                Box boxB = boxes[j];
                dists.Add((boxA.DistanceSqTo(boxB), boxA.Id, boxB.Id));
            }
        }
        dists.Sort();

        int numConnections = 0;
        foreach (var dist in dists)
        {
            Box boxA = boxes[dist.Item2];
            Box boxB = boxes[dist.Item3];

            if (!PathExist(boxA.Id, boxB.Id, boxes, new HashSet<int>()))
            {
                boxA.Neigh.Add(boxB.Id);
                boxB.Neigh.Add(boxA.Id);
                numConnections++;
            }

            if(numConnections == inputCol.Count-1)
            {
                return (boxA.X*boxB.X).ToString();
            }
        }


        return 0.ToString();
    }
}


