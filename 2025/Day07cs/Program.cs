using System.Diagnostics;

namespace Day07cs;

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
        int colLen = inputCol.First().Length;
        HashSet<int> set = new();
        for (int i = 0; i < colLen; i++)
        {
            if (inputCol.First()[i] == 'S')
                set.Add(i);
        }

        foreach (string line in inputCol)
        {
            HashSet<int> setToAdd = new();

            foreach (int ind in set)
            {
                if (line[ind] == '^')
                {
                    sum++;
                    set.Remove(ind);
                    setToAdd.Add(ind-1);
                    setToAdd.Add(ind+1);
                }
            }
            set.UnionWith(setToAdd);
        }

        return sum.ToString();
    }
    private static string GetResult2(List<string> inputCol)
    {
        long sum = 0;
        int colLen = inputCol.First().Length;
        Dictionary<int,long> dict = new();

        for (int i = 0; i < colLen; i++)
        {
            if (inputCol.First()[i] == 'S')
                dict[i] = 1;
        }

        foreach (string line in inputCol)
        {
            Dictionary<int,long> tmpDict= new();

            foreach (var pair in dict)
            {
                if (line[pair.Key] == '^')
                {
                    if (!tmpDict.ContainsKey(pair.Key - 1))
                    {
                        tmpDict[pair.Key - 1] = 0;
                    }
                    tmpDict[pair.Key - 1] += pair.Value;

                    if (!tmpDict.ContainsKey(pair.Key + 1))
                    {
                        tmpDict[pair.Key + 1] = 0;
                    }
                    tmpDict[pair.Key + 1] += pair.Value;
                }
                else
                {
                    if (!tmpDict.ContainsKey(pair.Key))
                    {
                        tmpDict[pair.Key] = 0;
                    }
                    tmpDict[pair.Key] += pair.Value;
                }
            }
            dict = tmpDict;
        }

        return dict.Values.Sum().ToString();
    }
}


