internal class MonkeyMap
{

    public byte[,] Map;
    public int nOfCol;
    public int nOfRow;
    public List<string> LineOfCommands;
    public int LineOfCommandsLen;
    public int[,] DirectMap = new int[4, 2] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
    public Dictionary<string, Tuple<int, int,int >> SpecCases = new Dictionary<string, Tuple<int, int,int>>();
    public MonkeyMap(List<string> inputCol)
    {
        inputCol[0] = inputCol[0].Substring(3);
        inputCol[0] = "   "+inputCol[0];
        LineOfCommands = new List<string>();
        CreateLineOfCommands(inputCol.Last());

        int i, j;
        nOfRow = 0;
        nOfCol = 0;
        foreach (string str in inputCol)
        {
            if (str == "" || str == null || str == string.Empty)
                break;
            nOfCol = Math.Max(nOfCol, str.Length);
            nOfRow++;
        }
        Map = new byte[nOfRow, nOfCol];
        //for(i=0; i<nOfRow;i++)
        //for(j=0; j<nOfRow;j++)
        //        Map[i,j] = -1;
        string str1;
        int str1Len;

        for (i = 0; i < nOfRow; i++)
        {
            str1 = inputCol[i];
            str1Len = str1.Length;
            for (j = 0; j < str1Len; j++)
            {
                switch (str1[j])
                {
                    case ' ':
                        Map[i, j] = 0;
                        break;
                    case '.':
                        Map[i, j] = 1;
                        break;
                    case '#':
                        Map[i, j] = 2;
                        break;
                }
            }
        }


        //for (i = 0; i < nOfRow; i++)
        //{
        //    Console.WriteLine("");
        //    for (j = 0; j < nOfCol; j++)
        //    {
        //        switch (Map[i, j])
        //        {
        //            case 0:
        //                Console.Write(" ");
        //                break;
        //            case 1:
        //                Console.Write(".");
        //                break;
        //            case 2:
        //                Console.Write("#");
        //                break;
        //        }
        //    }

        //}
        //Console.WriteLine("");




    }

    private void GetPosition(ref int x, ref int y, ref int dirSP)
    {
        string str = x + "," + y + "," + dirSP;
        if (SpecCases.ContainsKey(str))
        {
            Tuple<int, int,int> tup = SpecCases[str];
            x = tup.Item1;
            y = tup.Item2;
            dirSP = tup.Item3;
        }
    }

    private void GetPosition2(ref int x, ref int y, ref int dirSP)
    {
        switch (dirSP)
        {
            case 0:
                if (y < 50)
                {
                    if (x >= 150)
                    {
                        x = 99;
                        y = 149 - y;
                        dirSP = 2;
                    }
                }
                else if (y < 100)
                {
                    if (x >= 100)
                    {
                        x = y;
                        y = x;
                        dirSP = 3;
                    }
                }
                else if (y < 150)
                {
                    if (x >= 100)
                    {
                        x = 149;
                        y = 149 - y;
                        dirSP = 2;
                    }
                }
                else if (y < 50)
                {

                }
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }

        string str = x + "," + y + "," + dirSP;
        if (SpecCases.ContainsKey(str))
        {
            Tuple<int, int, int> tup = SpecCases[str];
            x = tup.Item1;
            y = tup.Item2;
            dirSP = tup.Item3;
        }
    }

    public void PreProcessingOfMapV01()
    {
        int i, j, minX, maxX, minY, maxY;
        for (i = 0; i < nOfRow; i++)
        {
            minX = 1000000;
            maxX = 0;
            for (j = 0; j < nOfCol; j++)
            {
                if (Map[i, j] == 1 || Map[i, j] == 2)
                {
                    minX = Math.Min(minX, j);
                    maxX = Math.Max(maxX, j);
                }
            }
            SpecCases.Add((maxX + 1) + "," + i + ",0", new Tuple<int, int, int>(minX, i,0));
            SpecCases.Add((minX - 1) + "," + i + ",2", new Tuple<int, int, int>(maxX, i,2));
        }

        for (j = 0; j < nOfCol; j++)
        {
            if (j == 46)
                minX = 0;
            minY = 1000000;
            maxY = 0;
            for (i = 0; i < nOfRow; i++)
            {
                if (Map[i, j] == 1 || Map[i, j] == 2)
                {
                    minY = Math.Min(minY, i);
                    maxY = Math.Max(maxY, i);
                }
            }
            SpecCases.Add(j + "," + (maxY + 1) + ",1", new Tuple<int, int, int>(j, minY,1));
            SpecCases.Add(j + "," + (minY - 1) + ",3", new Tuple<int, int, int>(j, maxY,3));
        }
    }

    private void CreateLineOfCommands(string line)
    {
        int val = -1;
        foreach (char c in line)
        {
            switch (c)
            {
                case 'L':
                    if (val != -1)
                        LineOfCommands.Add(val.ToString());
                    val = -1;
                    LineOfCommands.Add("L");
                    break;
                case 'R':
                    if (val != -1)
                        LineOfCommands.Add(val.ToString());
                    val = -1;
                    LineOfCommands.Add("R");
                    break;
                default:
                    if (val == -1)
                        val = 0;
                    val *= 10;
                    val += (int)(c - '0');
                    break;
            }
        }
        if (val != -1)
            LineOfCommands.Add(val.ToString());
        LineOfCommandsLen = LineOfCommands.Count;
    }

    internal long Simulate()
    {
        int startX = GetStartX();

        return RunSim(startX, 0, 0, 0);
    }

    private long RunSim(int startX, int startY, int direct, int step)
    {
        //Console.WriteLine(step + ": "+startX + ", " + startY + ", " + direct);
        if (step == LineOfCommandsLen)
            return (startY + 1) * 1000 + 4 * (startX + 1) + direct;
        int a = 0;
        if (startX == 48 && startY == 100)
            a = 1;
        string com = LineOfCommands[step];
        if (com == "R")
            return RunSim(startX, startY, (direct + 4 + 1) % 4, step + 1);
        else if (com == "L")
            return RunSim(startX, startY, (direct + 4 - 1) % 4, step + 1);
        else
        {
            int mapxy = MakeMove(ref startX, ref startY, ref direct, int.Parse(com));
            return RunSim(startX, startY, direct, step + 1);
        }

        throw new NotImplementedException();
    }

    private int MakeMove(ref int startX, ref int startY, ref int direct, int nOfMoves)
    {
        int difX = DirectMap[direct, 0];
        int difY = DirectMap[direct, 1];
        int x;
        int y;
        int dir;
        int mapxy = Map[startY, startX];
        for (int i = 0; i < nOfMoves; i++)
        {
            x = startX + difX;
            y = startY + difY;
            dir = direct;
            GetPosition(ref x, ref y, ref dir);
            if (Map[y, x] == 2)
            {
                break;
            }
            else
            {
                startX = x;
                startY = y;
                direct = dir;
            }
        }
        return mapxy;
    }


    private int GetStartX()
    {
        for (int j = 0; j < nOfCol; j++)
        {
            if (Map[0, j] == 1)
                return j;
        }
        throw new Exception("GetStartX()");
    }
}