internal class CubeSim
{
    private const int SIZE = 30;

    private bool?[,,] MEMO = new bool?[SIZE, SIZE, SIZE];

    public CubeSim(List<string> inputCol)
    {
        int i, j, k;
        for (i = 0; i < SIZE; i++)
            for (j = 0; j < SIZE; j++)
                for (k = 0; k < SIZE; k++)
                    MEMO[i, j, k] = null;

        string[] strArr;
        foreach (string str in inputCol)
        {
            strArr = str.Split(',');
            MEMO[int.Parse(strArr[0].Trim()) + 1, int.Parse(strArr[1].Trim()) + 1, int.Parse(strArr[2].Trim()) + 1] = true;
        }
    }

    public void FloorFill(int x, int y, int z, int depth)
    {
        if (x < 0 || x >= SIZE || y < 0 || y >= SIZE || z < 0 || z >= SIZE)
            return;
        if (MEMO[x, y, z] != null)
            return;

        MEMO[x, y, z] = false;

        FloorFill(x - 1, y, z, depth + 1);
        FloorFill(x, y - 1, z, depth + 1);
        FloorFill(x, y, z - 1, depth + 1);
        FloorFill(x + 1, y, z, depth + 1);
        FloorFill(x, y + 1, z, depth + 1);
        FloorFill(x, y, z + 1, depth + 1);
    }


    public void FloorFillNR()
    {
        Queue<Tuple<int,int,int>> que=new Queue<Tuple<int, int, int>>();

        que.Enqueue(new Tuple<int, int, int>(0, 0, 0));

        Tuple<int, int, int> tmp;

        int x, y, z;
        while (que.Any())
        {
            tmp= que.Dequeue();
            x= tmp.Item1;
            y= tmp.Item2;
            z= tmp.Item3;

            if (x < 0 || x >= SIZE || y < 0 || y >= SIZE || z < 0 || z >= SIZE)
                continue;
            if (MEMO[x, y, z] != null)
                continue;

            MEMO[x, y, z] = false;

            que.Enqueue(new Tuple<int, int, int>(x - 1, y, z));
            que.Enqueue(new Tuple<int, int, int>(x, y - 1, z));
            que.Enqueue(new Tuple<int, int, int>(x, y, z - 1));
            que.Enqueue(new Tuple<int, int, int>(x + 1, y, z));
            que.Enqueue(new Tuple<int, int, int>(x, y + 1, z));
            que.Enqueue(new Tuple<int, int, int>(x, y, z + 1));
        }

        
    }

    internal long GetSurface()
    {
        long res = 0;

        int i, j, k;
        for (i = 0; i < SIZE; i++)
            for (j = 0; j < SIZE; j++)
                for (k = 0; k < SIZE; k++)
                    if (MEMO[i, j, k] == true)
                        res += (NofAdjCUbes(i, j, k));

        return res;
    }

    private long NofAdjCUbes(int x, int y, int z)
    {
        long res = 0;
        res += IsFree(x - 1, y, z);
        res += IsFree(x, y - 1, z);
        res += IsFree(x, y, z - 1);
        res += IsFree(x + 1, y, z);
        res += IsFree(x, y + 1, z);
        res += IsFree(x, y, z + 1);
        return res;
    }

    private long IsFree(int x, int y, int z)
    {
        if (x < 0 || x >= SIZE || y < 0 || y >= SIZE || z < 0 || z >= SIZE)
            return 1;

        if (MEMO[x, y, z] == false)
            return 1;
        else
            return 0;
    }
}