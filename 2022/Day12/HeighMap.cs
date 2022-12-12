internal class HeighMap
{

    private int[,] _heights;
    private int[,] _lenghtOP;
    private int goalX;
    private int goalY;
    private int startX;
    private int startY;

    private int nOfRows;
    private int nOfCols;

    private Queue<Tuple<int, int, int, int>> tuples;

    public HeighMap(List<string> inputCol)
    {
        tuples = new Queue<Tuple<int, int, int, int>>();
        nOfRows = inputCol.Count;
        nOfCols = inputCol[0].Count();
        _heights = new int[nOfRows, nOfCols];
        _lenghtOP = new int[nOfRows, nOfCols];
        int i, j;
        string strTmp;
        char charTmp;

        for (i = 0; i < nOfRows; i++)
            for (j = 0; j < nOfCols; j++)
                _lenghtOP[i, j] = -1;

        for (i = 0; i < nOfRows; i++)
        {
            strTmp = inputCol[i];
            for (j = 0; j < nOfCols; j++)
            {
                charTmp = strTmp[j];
                if (charTmp >= 'a' && charTmp <= 'z')
                {
                    _heights[i, j] = charTmp - 'a';
                }
                else if (charTmp == 'S')
                {
                    _heights[i, j] = 'a' - 'a';
                    startX = i;
                    startY = j;
                }
                else
                {
                    _heights[i, j] = 'z' - 'a';
                    goalX = i;
                    goalY = j;
                }
            }
        }
    }

    public int RunBFS()
    {
        tuples.Enqueue(Tuple.Create(startX, startY, -1, 0));

        Tuple<int, int, int, int> tmpTuple;
        while (tuples.Any())
        {
            tmpTuple = tuples.Dequeue();
            //MoveOfBFS(startX, startY, -1, 0);
            if (MoveOfBFS(tmpTuple.Item1, tmpTuple.Item2, tmpTuple.Item3, tmpTuple.Item4))
                break;
        }
        return _lenghtOP[goalX, goalY];

    }

    private bool MoveOfBFS(int x, int y, int previousLen, int previousHei)
    {

        if (x < 0 || y < 0 || x >= nOfRows || y >= nOfCols)
            return false;
        if (_lenghtOP[x, y] != -1)
            return false;
        if (_heights[x, y] > previousHei + 1)
            return false;

        _lenghtOP[x, y] = previousLen + 1;

        if (x == goalX && y == goalY)
            return true;
        tuples.Enqueue(Tuple.Create(x + 1, y, previousLen + 1, _heights[x, y]));
        tuples.Enqueue(Tuple.Create(x - 1, y, previousLen + 1, _heights[x, y]));
        tuples.Enqueue(Tuple.Create(x, y + 1, previousLen + 1, _heights[x, y]));
        tuples.Enqueue(Tuple.Create(x, y - 1, previousLen + 1, _heights[x, y]));
        return false;
    }
}