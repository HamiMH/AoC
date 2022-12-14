using System.Collections.Generic;
using System.Drawing;

internal class SandFallSim
{

    List<List<Tuple<int, int>>> Rocks = new List<List<Tuple<int, int>>>();

    private int _xMin = 500;
    private int _xMax = 500;
    private int _xSize;
    private int _ySize;
    private int _yMin = 0;
    private int _yMax = 0;
    private bool _part2;
    private bool[,] Occupied;


    public SandFallSim(List<string> inputCol, bool part2=false)
    {
        _part2 = part2;

        string[] strLineArr;
        string[] strRockArr;
        List<Tuple<int, int>> rockPart;
        Tuple<int, int> rockCoord;
        foreach (string strLine in inputCol)
        {
            strLineArr = strLine.Split(" -> ");
            rockPart = new List<Tuple<int, int>>();
            foreach (string strRock in strLineArr)
            {
                strRockArr = strRock.Split(",");
                rockCoord = new Tuple<int, int>(int.Parse(strRockArr[0].Trim()), int.Parse(strRockArr[01].Trim()));
                SetBoundary(rockCoord);
                rockPart.Add(rockCoord);
            }
            Rocks.Add(rockPart);
        }
        if (_part2)
        {
            _xMin -= _yMax;
            _xMax += _yMax;
        }
        _xSize = X(_xMax) + 1;
        _ySize = _yMax + 1;
        if (_part2)
        {

            _ySize += 2;
        }
        Occupied = new bool[_ySize, _xSize];
        SetOccupied();
    }


    private void SetOccupied()
    {
        int i, j;

        for (i = 0; i < _ySize; i++)
            for (j = 0; j < _xSize; j++)
                Occupied[i, j] = false;

        int rockPartLen;
        foreach (List<Tuple<int, int>> rockPart in Rocks)
        {
            rockPartLen = rockPart.Count;
            Tuple<int, int> rockBeg = rockPart[0];
            Tuple<int, int> rockEnd;
            for (i = 1; i < rockPartLen; i++)
            {
                rockEnd = rockPart[i];
                Connect(rockBeg, rockEnd);
                rockBeg = rockEnd;
            }
        }
        //for (i = 0; i < _ySize; i++)
        //{
        //    Console.WriteLine();
        //    for (j = 0; j < _xSize; j++)
        //        if (Occupied[i, j])
        //            Console.Write("#");
        //        else
        //            Console.Write(".");
        //}
    }

    private void Connect(Tuple<int, int> rockBeg, Tuple<int, int> rockEnd)
    {
        Tuple<int, int> rockDirect = new Tuple<int, int>(
            Sign(rockEnd.Item1 - rockBeg.Item1)
            ,
           Sign(rockEnd.Item2 - rockBeg.Item2)
            );

        Occupied[rockBeg.Item2,X( rockBeg.Item1)] = true;
        while (!(rockBeg.Item1 == rockEnd.Item1 && rockBeg.Item2 == rockEnd.Item2))
        {
            rockBeg = new Tuple<int, int>(rockBeg.Item1 + rockDirect.Item1, rockBeg.Item2 + rockDirect.Item2);
            Occupied[rockBeg.Item2, X(rockBeg.Item1)] = true;
        }
    }

    private int X(int x)
    {
        return x - _xMin;
    }

    private void SetBoundary(Tuple<int, int> tup)
    {
        _xMin = Min(_xMin, tup.Item1);
        _xMax = Max(_xMax, tup.Item1);
        _yMin = Min(_yMin, tup.Item2);
        _yMax = Max(_yMax, tup.Item2);
    }
    private int Min(int a, int b) { if (a > b) return b; else return a; }
    private int Max(int a, int b) { if (a < b) return b; else return a; }
    private int Sign(int a) { if (a == 0) return a; else if (a > 0) return 1; else return -1; }


    public int Simulate()
    {
        int i;
        int xFallCoord = X(500);
        for ( i=0; ; i++)
        {
            if (Fall(xFallCoord, 0) &&_part2==false)
                break;
            if (Occupied[0,xFallCoord])
                break;
        }
        PrintSpace();
        
        return i;
    }

    private void PrintSpace()
    {
        int i,j;
        for ( i = 0; i < _ySize; i++)
        {
            Console.WriteLine();
            for (j = 0; j < _xSize; j++)
                if (Occupied[i, j])
                    Console.Write("#");
                else
                    Console.Write(".");
            Console.WriteLine();
        }
    }

    private bool Fall(int x, int y)
    {
        while (true)
        {
            if (_part2 == false)
            {
                if (y >= _ySize)
                    return true;
            }
            else
            {
                if (y == _ySize - 2)
                {
                    Occupied[y, x] = true;
                    return false;
                }
            }
            if (Occupied[y + 1, x] == false)
            {
                y = y + 1;
                continue;
            }
            if (x == 0)
                return true;
            if (Occupied[y + 1, x - 1] == false)
            {
                y = y + 1;
                x = x - 1;
                continue;
            }
            if (x >= _xSize-1)
                return true;
            if (Occupied[y + 1, x + 1] == false)
            {
                y = y + 1;
                x = x + 1;
                continue;
            }
            Occupied[y, x] = true;
            return false;
        }
    }
}