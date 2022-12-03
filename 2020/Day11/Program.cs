using System;
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

    private static int nOfRows;
    private static int nOfCols;
    private static long CombinationsOfSteps(List<string> inputCol)
    {
        nOfRows = inputCol.Count;
        nOfCols = inputCol[0].Length;
        bool?[,,] table = new bool?[2, nOfRows, nOfCols];
        LoadInputToTable(inputCol, table);
        //return Simulate(table);
        return SimulateDist(table);
    }

    private static long Simulate(bool?[,,] table)
    {
        int i, j;
        int newInd = 1;
        bool? b;
        bool changed = true;
        while (changed)
        {
            changed = false;
            for (i = 0; i < nOfRows; i++)
                for (j = 0; j < nOfCols; j++)
                {
                    b = table[newInd ^ 1, i, j];
                    switch (b)
                    {
                        case true:
                            if (NumbOfNeig(table, newInd ^ 1, i, j) >= 4)
                            {
                                table[newInd, i, j] = false;
                                changed = true;
                            }
                            else
                            {
                                table[newInd, i, j] = true;
                            }
                            break;
                        case false:
                            if (NumbOfNeig(table, newInd ^ 1, i, j) == 0)
                            {
                                table[newInd, i, j] = true;
                                changed = true;
                            }
                            else
                            {
                                table[newInd, i, j] = false;
                            }
                            break;
                        case null:
                            break;
                        default:
                            throw new Exception("Case b");
                    }
                }
            newInd ^= 1;
        }
        long result = 0;
        for (i = 0; i < nOfRows; i++)
            for (j = 0; j < nOfCols; j++)
            {
                if (table[newInd, i, j] == true)
                    result++;
            }
        return result;
    }

    private static long SimulateDist(bool?[,,] table)
    {
        int i, j;
        int newInd = 1;
        bool? b;
        bool changed = true;
        while (changed)
        {
            changed = false;
            for (i = 0; i < nOfRows; i++)
                for (j = 0; j < nOfCols; j++)
                {
                    b = table[newInd ^ 1, i, j];
                    switch (b)
                    {
                        case true:
                            if (NumbOfDistantNeig(table, newInd ^ 1, i, j) >= 5)
                            {
                                table[newInd, i, j] = false;
                                changed = true;
                            }
                            else
                            {
                                table[newInd, i, j] = true;
                            }
                            break;
                        case false:
                            if (NumbOfDistantNeig(table, newInd ^ 1, i, j) == 0)
                            {
                                table[newInd, i, j] = true;
                                changed = true;
                            }
                            else
                            {
                                table[newInd, i, j] = false;
                            }
                            break;
                        case null:
                            break;
                        default:
                            throw new Exception("Case b");
                    }
                }
            //PrintTable(table, newInd);
            newInd ^= 1;
        }
        long result = 0;
        for (i = 0; i < nOfRows; i++)
            for (j = 0; j < nOfCols; j++)
            {
                if (table[newInd, i, j] == true)
                    result++;
            }
        return result;
    }

    private static int NumbOfNeig(bool?[,,] table, int v, int i, int j)
    {
        int ii, jj;
        int ret = 0;
        for (ii = i - 1; ii <= i + 1; ii++)
        {
            if (ii < 0 || ii >= nOfRows)
                continue;
            for (jj = j - 1; jj <= j + 1; jj++)
            {
                if (jj < 0 || jj >= nOfCols)
                    continue;
                if (ii == i && jj == j)
                    continue;
                if (table[v, ii, jj] == true)
                    ret++;
            }
        }
        return ret;
    }
    private static int NumbOfDistantNeig(bool?[,,] table, int v, int x, int y)
    {
        int i, j, xx, yy;
        int ret = 0;
        for (i = -1; i <= 1; i++)
            for (j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                xx = x + i;
                yy = y + j;
                while (xx >= 0 && xx < nOfRows && yy >= 0 && yy < nOfCols)
                {
                    if (table[v, xx, yy] == false)
                    {
                        break;
                    }
                    if (table[v, xx, yy] == true)
                    {
                        ret++;
                        break;
                    }
                    xx += i;
                    yy += j;
                }
            }
        return ret;
    }

    private static void LoadInputToTable(List<string> inputCol, bool?[,,] table)
    {
        int i, j;
        char c;
        for (i = 0; i < nOfRows; i++)
            for (j = 0; j < nOfCols; j++)
            {
                c = inputCol[i][j];
                switch (c)
                {
                    case 'L':
                        table[0, i, j] = false;
                        break;
                    case '#':
                        table[0, i, j] = true;
                        break;
                    case '.':
                        table[0, i, j] = null;
                        break;
                    default:
                        throw new Exception("Case c");
                }
            }
    }

    private static void PrintTable(bool?[,,] table, int index)
    {
        int i, j;
        bool? b;
        for (i = 0; i < nOfRows; i++)
        {
            for (j = 0; j < nOfCols; j++)
            {
                b = table[index, i, j];
                switch (b)
                {
                    case true:
                        Console.Write("#");
                        break;
                    case false:
                        Console.Write("L");
                        break;
                    case null:
                        Console.Write(".");
                        break;
                    default:
                        throw new Exception("Case c");
                }
            }
            Console.WriteLine();
        }
            Console.WriteLine();
            Console.WriteLine();
    }
}