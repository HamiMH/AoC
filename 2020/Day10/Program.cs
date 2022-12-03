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
        //long result = CalculationOfSteps(inputCol);
        long result = CombinationsOfSteps(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long CombinationsOfSteps(List<string> inputCol)
    {
        inputCol.Add("0");
        int[] valArr = inputCol.Select(x => int.Parse(x)).ToArray();
        Array.Sort(valArr);
        int len = valArr.Length;

        long[] combArr=new long[len];
        combArr[0] = 1;

        int i, j, iVal;
        for (i = 1; i < len; i++)
        {
            iVal = valArr[i];
            for(j=i;j>=0; j--)
            {
                if(valArr[j] < iVal-3)
                    break;
                combArr[i] += combArr[j];
            }
        }
        return combArr[len-1];
    }

    private static long CalculationOfSteps(List<string> inputCol)
    {
        inputCol.Add("0");
        List<int> valList = inputCol.Select(x => int.Parse(x)).ToList();
        valList.Sort();
        int skip1 = 0;
        int skip3 = 0;
        int diff;
        for(int i = 1; i < valList.Count; i++)
        {
            diff = valList[i] - valList[i - 1];
            if (diff==1)
                skip1++;
            else if(diff==3)
                skip3++;
        }
        skip3++;
        return skip1* skip3;
    }
}