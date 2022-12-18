internal class FallingSim
{
    public char[] Directions;
    public int DirLen;

    public Dictionary<string, Tuple<long, long>> MEMO = new Dictionary<string, Tuple<long, long>>();

    public List<byte[,]> ListOfTypes = new List<byte[,]>();
    public int TypesLen;

    public byte[,] Layout;
    public int Height = 50000;
    public long HeighestPart = 0;
    public long BonusHei = 0;
    public FallingSim(List<string> inputCol)
    {
        string sss = inputCol[0].Substring(3);
        DirLen = sss.Count();
        Directions = new char[DirLen];
        int i;
        for (i = 0; i < DirLen; i++)
            Directions[i] = sss[i];


        ListOfTypes.Add(new byte[,] { { 2, 2, 2, 2 } });
        ListOfTypes.Add(new byte[,] { { 0, 2, 0 }, { 2, 2, 2 }, { 0, 2, 0 } });
        ListOfTypes.Add(new byte[,] { { 2, 2, 2 }, { 0, 0, 2 }, { 0, 0, 2 } });
        ListOfTypes.Add(new byte[,] { { 2 }, { 2 }, { 2 }, { 2 } });
        ListOfTypes.Add(new byte[,] { { 2, 2 }, { 2, 2 } });
        TypesLen = ListOfTypes.Count;

        Layout = new byte[Height, 9];
        for (i = 0; i < Height; i++)
        {
            Layout[i, 0] = 1;
            Layout[i, 8] = 1;
        }
        for (i = 0; i < 9; i++)
        {
            Layout[0, i] = 1;
        }
    }

    public long SimulateFalling(long nOfRocks)
    {
        char direc;
        byte[,] type;
        int typeWei;
        int typeHei;
        int indexX;
        int indexY;

        int Flow;

        bool canMove;

        int y, x;
        int stepOfSim = -1;
        int typeOfRock;
        int stepOfStone;
        for (long rock = 0; rock < nOfRocks; rock++)
        {
            //PrintTower(100);
            typeOfRock = (int)(rock % (long)TypesLen);
            type = ListOfTypes[typeOfRock];
            typeHei = type.GetLength(0);
            typeWei = type.GetLength(1);
            indexX = 3;
            indexY = (int)HeighestPart + 4;
            stepOfStone = 0;
            for (; ; )
            {
                stepOfSim++;
                direc = Directions[stepOfSim % DirLen];
                if (stepOfSim % DirLen == 0 )
                {
                    if (MEMO.ContainsKey("(" + typeOfRock + "," + stepOfStone + ")," + GetUppers()))
                    {
                        Tuple<long, long> tup = MEMO["(" + typeOfRock+","+stepOfStone + ")," + GetUppers()];
                        long previousRock = tup.Item2;
                        long rockDif=rock - previousRock;
                        long toEnd=nOfRocks-rock;
                        long iterRemain = toEnd / rockDif;
                        BonusHei += iterRemain * (HeighestPart- tup.Item1);
                        rock += iterRemain * rockDif;
                    }
                    else
                    {
                        AddToMemo(typeOfRock, rock, stepOfStone);
                    }
                }

                if (direc == '<')
                    Flow = -1;
                else
                    Flow = 1;

                //Move(type, typeHei, typeWei, Flow, 0);
                canMove = true;
                for (y = 0; y < typeHei; y++)
                    for (x = 0; x < typeWei; x++)
                    {
                        if (type[y, x] == 0)
                            continue;
                        if (Layout[indexY + y, indexX + x + Flow] == 1)
                            canMove = false;
                    }
                if (canMove)
                    indexX += Flow;

                canMove = true;
                for (y = 0; y < typeHei; y++)
                    for (x = 0; x < typeWei; x++)
                    {
                        if (type[y, x] == 0)
                            continue;
                        if (Layout[indexY + y - 1, indexX + x] == 1)
                            canMove = false;
                    }
                if (canMove)
                    indexY -= 1;
                else
                {
                    for (y = 0; y < typeHei; y++)
                        for (x = 0; x < typeWei; x++)
                        {
                            if (type[y, x] == 0)
                                continue;
                            Layout[indexY + y, indexX + x] = 1;
                            HeighestPart = Math.Max(HeighestPart, indexY + y);
                        }
                    break;
                }
                stepOfStone++;

            }

        }
        return HeighestPart+BonusHei;
    }

    private void AddToMemo(int typeOfRock, long rock, int stepOfStone)
    {
        //int typeOfRock = (int)(rock % (long)TypesLen);
        string key = "(" + typeOfRock + "," + stepOfStone + ")," + GetUppers();
        MEMO.Add(key, new Tuple<long,long>(HeighestPart, rock));
    }

    internal void PrintTower(int maxHei = 5000)
    {
        int i, j;
        for (i = maxHei - 1; i >= 0; i--)
        {
            Console.WriteLine();
            for (j = 0; j < 9; j++)
                if (Layout[i, j] == 0)
                    Console.Write(".");
                else
                    Console.Write("#");

        }
        Console.WriteLine();

    }

    public string GetUppers()
    {
        string toReturn = "";

        int i, j;
        for (i = 1; i < 8; i++)
        {
            for (j = (int)HeighestPart; j >= 0; j--)
            {
                if (Layout[j, i] == 1)
                {
                    toReturn = toReturn + (HeighestPart - j) + ",";
                    break;
                }
            }
        }
        return toReturn;
    }
}