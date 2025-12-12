using System.Diagnostics;

namespace Day11cs;

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
        Dictionary<string, List<string>> edges = new();
        foreach (string col in inputCol)
        {
            ParseLine(col, edges);
        }

        RunRecursive("you", "out", edges, new HashSet<string>(), ref sum);
        return sum.ToString();
    }

    private static void RunRecursive(string currNode, string finalNode, Dictionary<string, List<string>> edges, HashSet<string> badOnes, ref long sum)
    {
        if (currNode == finalNode)
        {
            sum++;
            return;
        }
        if(badOnes.Contains(currNode))
        {
            return;
        }
        if (!edges.ContainsKey(currNode))
        {
            return;
        }
        foreach (string neighbor in edges[currNode])
        {
            RunRecursive(neighbor, finalNode, edges, badOnes, ref sum);
        }
    }

    private static void ParseLine(string col, Dictionary<string, List<string>> edges)
    {
        string[] parts = col.Split(' ');
        string key = parts[0].Substring(0, parts[0].Length - 1);
        List<string> values = parts.Skip(1).ToList();
        edges[key] = values;
    }
    private static void ParseLineReverse(string col, Dictionary<string, List<string>> dict)
    {
        string[] parts = col.Split(' ');
        string key = parts[0].Substring(0, parts[0].Length - 1);
        List<string> values = parts.Skip(1).ToList();
        foreach(string val in values)
        {
            if (!dict.ContainsKey(val))
                dict[val] = new List<string>();
            dict[val].Add(key);
        }
    }

    private static string GetResult2(List<string> inputCol)
    {
        long sum = 0;
        long sum2 = ReverseMethod(inputCol);

        return sum2.ToString();
        Dictionary<string, List<string>> edges = new();
        foreach (string col in inputCol)
        {
            ParseLine(col, edges);
        }

        HashSet<string> rechableFromFft = new HashSet<string>();
        FindAllRechable("fft", edges, rechableFromFft);

        HashSet<string> rechableFromDac = new HashSet<string>();
        FindAllRechable("dac", edges, rechableFromFft);


        HashSet<string> rechableFromFftNotByDac = new HashSet<string>() { "dac"};
        FindAllRechable("fft", edges, rechableFromFft);

        HashSet<string> rechableFromDacNotByFft = new HashSet<string>() { "fft"};
        FindAllRechable("dac", edges, rechableFromFft);

        HashSet<string> union = new HashSet<string>(rechableFromFft);
        union.UnionWith(rechableFromDac);


        Console.WriteLine("svr to fft");
        long svrToFft = 0;
        RunRecursive("svr", "fft", edges, union, ref svrToFft);

        Console.WriteLine("svr to dac");
        long svrToDac = 0;
        RunRecursive("svr", "dac", edges, union, ref svrToDac);

        Console.WriteLine("fft to dac");
        long fftToDac = 0;
        RunRecursive("fft", "dac", edges, rechableFromDac, ref fftToDac);

        Console.WriteLine("dac to fft");
        long dacToFft = 0;
        RunRecursive("dac", "fft", edges, rechableFromFft, ref dacToFft);

        Console.WriteLine("fft to out");
        long fftToOut = 0;
        RunRecursive("fft", "out", edges, rechableFromDacNotByFft, ref fftToOut);

        Console.WriteLine("dac to out");
        long dacToOut = 0;
        RunRecursive("dac", "out", edges, rechableFromFftNotByDac, ref dacToOut);

        sum += svrToFft * fftToDac * dacToOut;
        sum += svrToDac * dacToFft * fftToOut;
        return sum.ToString();
    }

    private static long ReverseMethod(List<string> inputCol)
    {
        Dictionary<string, List<string>> edges = new();
        foreach (string col in inputCol)
        {
            ParseLineReverse(col, edges);
        }
        Console.WriteLine("svr to fft");
        
        long svrToFft = RunRecursiveReverse("svr", "fft", edges, new Dictionary<string, long>());

        Console.WriteLine("svr to dac");
        long svrToDac = RunRecursiveReverse("svr", "dac", edges, new Dictionary<string, long>());

        Console.WriteLine("fft to dac");
        long fftToDac = RunRecursiveReverse("fft", "dac", edges, new Dictionary<string, long>());

        Console.WriteLine("dac to fft");
        long dacToFft = RunRecursiveReverse("dac", "fft", edges, new Dictionary<string, long>());

        Console.WriteLine("fft to out");
        long fftToOut = RunRecursiveReverse("fft", "out", edges, new Dictionary<string, long>());

        Console.WriteLine("dac to out");
        long dacToOut = RunRecursiveReverse("dac", "out",edges,new Dictionary<string, long>());

        long sum = 0;
        sum += svrToFft * fftToDac * dacToOut;
        sum += svrToDac * dacToFft * fftToOut;
       
        return sum;
    }
    private static long RunRecursiveReverse(string finalNode, string currNode, Dictionary<string, List<string>> edges,Dictionary<string,long>MEMO)
    {
        if(MEMO.ContainsKey(currNode))
            return MEMO[currNode];
        if (currNode == finalNode)
        {
            return 1;
        }

        if (!edges.ContainsKey(currNode))
            return 0;
        long sum = 0;
        foreach (string neighbor in edges[currNode])
        {
            sum +=RunRecursiveReverse(finalNode, neighbor, edges,MEMO);
        }
        MEMO[currNode] = sum;
        return sum;
    }

    private static void FindAllRechable(string currNode, Dictionary<string, List<string>> edges, HashSet<string> rechableFrom)
    {
        if (rechableFrom.Contains(currNode))
        {
            return;
        }
        rechableFrom.Add(currNode);
        if (!edges.ContainsKey(currNode))
        {
            return;
        }
        foreach (string neighbor in edges[currNode])
        {
            FindAllRechable(neighbor, edges, rechableFrom);
        }
    }
}


