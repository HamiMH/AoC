using System;
using System.Collections.Generic;

internal class Xmas
{
    //private List<long> InputCol { get; set; }
    private long[] InputCol;



    public Xmas(List<string> inputCol)
    {
        InputCol = inputCol.Select(x => long.Parse(x)).ToArray();
    }


    internal long FindWrongNumber()
    {
        int len = InputCol.Count();
        int first = 0;
        int last = 24;

        for (int i = 25; i < len; i++)
        {
            if (CanBeMade(InputCol[i], first, last) == false)
                return InputCol[i];
            first++;
            last++;
        }
        throw new NotImplementedException();
    }

    internal long FindSequenceForWrongNumber()
    {
        int len = InputCol.Count();
        int first = 0;
        int last = 24;
        int i, j, index;
        for (index = 25; index < len; index++)
        {
            if (CanBeMade(InputCol[index], first, last) == false)
                break;
            first++;
            last++;
        }


        return FindSumFromSquence(InputCol[index], first, last);
        throw new NotImplementedException();
    }

    private long FindSumFromSquence(long intendSum, int first, int last)
    {
        long sum;
        int i, j;
        for (i = 0; i <= last; i++)
        {
            sum = InputCol[i];
            for (j = i + 1; j <= last; j++)
            {
                sum += InputCol[j];
                if (sum > intendSum)
                    break;
                if (sum == intendSum)
                    return GetSum(i, j);
            }
        }
        return 0;
    }

    private long GetSum(int minIndex, int maxIndex)
    {
        long minval = long.MaxValue;
        long maxval = 0;
        for (int i = minIndex; i <= maxIndex; i++)
        {
            if (InputCol[i] < minval)
                minval = InputCol[i];
            if (InputCol[i] > maxval)
                maxval = InputCol[i];
        }
        return minval + maxval;
    }

    private bool CanBeMade(long v, int first, int last)
    {

        for (int i = first; i <= last; i++)
            for (int j = i + 1; j <= last; j++)
                if (InputCol[i] + InputCol[j] == v)
                    return true;
        return false;

    }
}