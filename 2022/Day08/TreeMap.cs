internal class TreeMap
{

    private int[,] _trees;
    private bool[,] _visible;
    private int nOfRows;
    private int nOfCols;

    public TreeMap(List<string> inputCol)
    {
        nOfRows = inputCol.Count;
        nOfCols = inputCol[0].Count();
        int i, j;
        _trees = new int[nOfRows, nOfCols];
        _visible = new bool[nOfRows, nOfCols];

        for (i = 0; i < nOfRows; i++)
            for (j = 0; j < nOfCols; j++)
            {
                _trees[i, j] = int.Parse(inputCol[i][j].ToString());
                _visible[i, j] = false;
            }
    }

    public long TreeVisibility()
    {
        int i, j;
        int maxHei;

        for (i = 0; i < nOfRows; i++)
        {
            maxHei = -1;
            for (j = 0; j < nOfCols; j++)
            {
                if (_trees[i, j] > maxHei)
                {
                    maxHei = _trees[i, j];
                    _visible[i, j] = true;
                }
            }
        }
        for (i = 0; i < nOfRows; i++)
        {
            maxHei = -1;
            for (j = nOfCols - 1; j >= 0; j--)
            {
                if (_trees[i, j] > maxHei)
                {
                    maxHei = _trees[i, j];
                    _visible[i, j] = true;
                }
            }
        }
        for (i = 0; i < nOfRows; i++)
        {
            maxHei = -1;
            for (j = 0; j < nOfCols; j++)
            {
                if (_trees[j, i] > maxHei)
                {
                    maxHei = _trees[j, i];
                    _visible[j, i] = true;
                }
            }
        }
        for (i = 0; i < nOfRows; i++)
        {
            maxHei = -1;
            for (j = nOfCols - 1; j >= 0; j--)
            {
                if (_trees[j, i] > maxHei)
                {
                    maxHei = _trees[j, i];
                    _visible[j, i] = true;
                }
            }
        }


        long nOfVisible = 0;
        for (i = 0; i < nOfRows; i++)
            for (j = 0; j < nOfCols; j++)
                if (_visible[i, j])
                    nOfVisible++;

        return nOfVisible;
    }

    public long GetMaxViewScore()
    {
        int i, j;

        long score;
        long maxScore = 0;
        for (i = 1; i < nOfRows-1; i++)
            for (j = 1; j < nOfCols-1; j++)
            {
                score = ViewOfXY(i, j);
                if (score > maxScore)
                    maxScore = score;
            }
        return maxScore;


    }

    private long ViewOfXY(int x, int y)
    {
        long viewScore = 1;
        int i, j;

        for (i = 1; x+i < nOfRows - 1; i++)
        {
            if (_trees[x+i, y] >= _trees[x, y])
                break;
        }
        viewScore *= i;

        for (i = -1; x + i >= 1; i--)
        {
            if (_trees[x+i, y] >= _trees[x, y])
                break;
        }
        viewScore *= i;


        for (i = 1; y + i < nOfCols - 1; i++)
        {
            if (_trees[x , y + i] >= _trees[x, y])
                break;
        }
        viewScore *= i;

        for (i = -1; y + i >= 1; i--)
        {
            if (_trees[x , y + i] >= _trees[x, y])
                break;
        }
        viewScore *= i;


        return viewScore;
    }
}