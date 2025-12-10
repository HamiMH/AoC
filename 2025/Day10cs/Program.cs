using LpSolveDotNet;
using System.Diagnostics;



namespace Day10cs;

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
        foreach (string line in inputCol)
        {
            sum += ResolveLine(line);
        }
        return sum.ToString();
    }

    private static long ResolveLine(string line)
    {
        string[] parts = line.Split(' ');
        ulong finalVal = ExtractFinalVal(parts[0]);

        List<ulong> buttons = parts.Skip(1).Take(parts.Length - 2).Select(s => ExtractButtonVal(s)).ToList();

        HashSet<ulong> allAttended = new() { 0 };
        HashSet<ulong> oldAttended = new() { 0 };

        int iter = 1;
        while (true)
        {
            HashSet<ulong> newAttended = new();

            foreach (ulong lon in oldAttended)
            {
                foreach (ulong butt in buttons)
                {
                    ulong newVal = lon ^ butt;
                    if (newVal == finalVal) return iter;
                    if (allAttended.Contains(newVal)) continue;
                    newAttended.Add(newVal);
                    allAttended.Add(newVal);
                }
            }
            oldAttended = newAttended;
            iter++;
        }
        return 0;
    }

    private static ulong ExtractButtonVal(string s)
    {
        string importPart = s.Substring(1, s.Length - 2);
        IEnumerable<int> nums = importPart.Split(',').Select(s => int.Parse(s));
        ulong val = 0;
        foreach (int num in nums)
        {
            val |= 1Lu << num;
        }
        return val;
    }

    private static ulong ExtractFinalVal(string v)
    {
        ulong val = 0;
        foreach (char c in v.Reverse())
        {
            if (c == '.')
            {
                val <<= 1;
            }
            else if (c == '#')
            {
                val <<= 1;
                val |= 1;
            }
        }
        return val;
    }

    private static string GetResult2(List<string> inputCol)
    {
        long sum = 0;
        foreach (string line in inputCol)
        {
            sum += ResolveLine2(line);
        }
        return sum.ToString();
    }

    private static long ResolveLine2(string line)
    {
        Console.WriteLine(line);
        string[] parts = line.Split(' ');
        List<int> finalArr = ExtractArray(parts.Last());

        List<List<int>> buttons = parts.Skip(1).Take(parts.Length - 2).Select(s => ExtractArray2(s, finalArr.Count)).ToList();
        //buttons.Sort((x1, x2) => x2.Sum() - x1.Sum());

        int sol = LpSolution(buttons, finalArr);
        return sol;

        //int reductionSize = 4;
        //while (true)
        //{
        //    List<int> finalArrC = new List<int>(finalArr);
        //    int reductionCount = DoReduction(finalArrC, buttons, reductionSize);
        //    MEMO.Clear();
        //    int val = Recursion(finalArrC, buttons);
        //    if(val>int.MaxValue/10)
        //    {
        //        reductionSize += 2;
        //        continue;
        //    }
        //    return (long)reductionCount + val;
        //}
    }

    private static int LpSolution(List<List<int>> buttons, List<int> finalArr)
    {
       
        LpSolve lp = LpSolve.make_lp(0, buttons.Count);
        double[] c = new double[buttons.Count+1];
        for (int i = 1; i < c.Length; i++) c[i] = 1;
        lp.set_obj_fn(c);
        lp.set_minim();

        for (int i = 0; i < finalArr.Count; i++)
        {
            double[] row = new double[buttons.Count+1];
            for(int j=0;j<buttons.Count;j++)
            {
                row[j+1] = buttons[j][i];
            }
            lp.add_constraint(row, lpsolve_constr_types.EQ, finalArr[i]);
            lp.set_int(i + 1, true);

        }
        //bool wr=lp.write_lp("debug_model.lp");
        var solved=lp.solve().ToString();
        if (solved != "OPTIMAL")
            throw new Exception();

        double[] vars = new double[buttons.Count];
        lp.get_variables(vars);
        

        lp.delete_lp();
        return (int)Math.Round( vars.Sum());
    }

    private static int DoReduction(List<int> finalArr, List<List<int>> buttons, int dec)
    {
        int sum = 0;
        foreach (List<int> butt in buttons)
        {
            int min = int.MaxValue;

            for (int i = 0; i < butt.Count; i++)
            {
                if (butt[i] == 1)
                    min = Math.Min(min, finalArr[i]);
            }
            if (min < dec)
                continue;
            min -= dec;

            for (int i = 0; i < butt.Count; i++)
            {
                finalArr[i] -= min;
            }
            sum += min;
        }
        return sum;
    }

    private static List<int> ExtractArray2(string s, int count)
    {
        List<int> tmp = ExtractArray(s);
        int[] list = new int[count];
        foreach (int tm in tmp)
        {
            list[tm] = 1;
        }
        return list.ToList();
    }

    private static Dictionary<int, int> MEMO = new();
    private static int Recursion(List<int> currentArr, List<List<int>> buttons)
    {
        int hc = GetHashCode(currentArr);
        if (MEMO.ContainsKey(hc))
            return MEMO[hc];

        int min = int.MaxValue / 5;
        foreach (List<int> butt in buttons)
        {
            (int retVal, List<int> newArr) = DiffArr(currentArr, butt);
            if (retVal == 0) return 1;
            if (retVal == 2) continue;
            min = Math.Min(min, 1 + Recursion(newArr, buttons));
        }
        MEMO[hc] = min;
        return min;
    }

    public static int GetHashCode(List<int> obj)
    {
        if (obj is null) return 0;
        var hc = new HashCode();
        // combine elements into hash (order-sensitive)
        foreach (var v in obj) hc.Add(v);
        return hc.ToHashCode();
    }
    private static (int, List<int>) DiffArr(List<int> baseArr, List<int> subArr)
    {
        int retval = 0;
        List<int> arr = new();
        for (int i = 0; i < baseArr.Count; i++)
        {
            arr.Add(baseArr[i] - subArr[i]);
            if ((baseArr[i] > subArr[i]) && retval == 0)
                retval = 1;
            if (baseArr[i] < subArr[i])
            {
                retval = 2;
            }

        }
        return (retval, arr);
    }



    private static List<int> ExtractArray(string s)
    {
        string importPart = s.Substring(1, s.Length - 2);
        return importPart.Split(',').Select(s => int.Parse(s)).ToList();
    }
}


