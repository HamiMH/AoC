internal class BlizzValley
{
    public int LCM { get; }
    public int SizeX;
    public int SizeY;
    public byte[,] State;

    public int StartX;
    public int StartY;
    public int FinalX;
    public int FinalY;

    public List<HashSet<Tuple<int, int>>> AttendedStates = new List<HashSet<Tuple<int, int>>>();

    public BlizzValley(List<string> inputCol)
    {
        int i, j;
        int inputRows = inputCol.Count;
        int inputCols = inputCol[0].Length;
        if (inputRows == 37)
            LCM = 700;
        else
            LCM = 12;

        SizeY = inputRows - 2;
        SizeX = inputCols - 2;
        State = new byte[SizeY, SizeX];

        for (i = 1; i < inputRows - 1; i++)
            for (j = 1; j < inputCols - 1; j++)
                switch (inputCol[i][j])
                {
                    case '>':
                        State[i - 1, j - 1] = (1 << 0);
                        break;
                    case 'v':
                        State[i - 1, j - 1] = (1 << 1);
                        break;
                    case '<':
                        State[i - 1, j - 1] = (1 << 2);
                        break;
                    case '^':
                        State[i - 1, j - 1] = (1 << 3);
                        break;
                }
       StartX = 0;
        StartY = -1;
        FinalX = SizeX - 1;
        FinalY = SizeY;
    }

    internal long TimeToTravel()
    {
        int iter;

        HashSet<Tuple<int, int>> attendedStatePrev;
        HashSet<Tuple<int, int>> attendedStateNew;
        AttendedStates.Add(new HashSet<Tuple<int, int>>() { new Tuple<int, int>(0, -1) });

        Print();
        for (iter = 1; ; iter++)
        {
            attendedStatePrev = AttendedStates[iter - 1];
            attendedStateNew = new HashSet<Tuple<int, int>>();
            AttendedStates.Add(attendedStateNew);
            MoveBlizzard();
            //Print();
            foreach (Tuple<int, int> state in attendedStatePrev)
            {
                if (MakeMoves(state, attendedStateNew))
                    return iter-1;
            }
        }


        return 0;
    }


    internal long TimeToTravelBackAndFor()
    {
        int iter;

        HashSet<Tuple<int, int>> attendedStatePrev;
        HashSet<Tuple<int, int>> attendedStateNew;
        AttendedStates.Add(new HashSet<Tuple<int, int>>() { new Tuple<int, int>(StartX, StartY) });

        //Print();
        for (iter = 1; ; iter++)
        {
            attendedStatePrev = AttendedStates.Last();
            attendedStateNew = new HashSet<Tuple<int, int>>();
            AttendedStates.Add(attendedStateNew);
            MoveBlizzard();
            foreach (Tuple<int, int> state in attendedStatePrev)
            {
                MakeMoves(state, attendedStateNew);
            }
            if (attendedStateNew.Contains(new Tuple<int, int>(FinalX, FinalY)))
                break;
        }
        iter++;
        AttendedStates.Add( new HashSet<Tuple<int, int>>() { new Tuple<int, int>(FinalX, FinalY) });
        //Console.WriteLine(iter);
        for (; ; iter++)
        {
            attendedStatePrev = AttendedStates.Last();
            attendedStateNew = new HashSet<Tuple<int, int>>();
            AttendedStates.Add(attendedStateNew);
            MoveBlizzard();
            foreach (Tuple<int, int> state in attendedStatePrev)
            {
                MakeMovesBack(state, attendedStateNew);
            }
            if (attendedStateNew.Contains(new Tuple<int, int>(StartX, StartY)))
                break;
        }
        iter++;
        AttendedStates.Add(new HashSet<Tuple<int, int>>() { new Tuple<int, int>(StartX, StartY) });
        //Console.WriteLine(iter);

        for (; ; iter++)
        {
            attendedStatePrev = AttendedStates.Last();
            attendedStateNew = new HashSet<Tuple<int, int>>();
            AttendedStates.Add(attendedStateNew);
            MoveBlizzard();
            foreach (Tuple<int, int> state in attendedStatePrev)
            {
                MakeMoves(state, attendedStateNew);
            }
            if (attendedStateNew.Contains(new Tuple<int, int>(FinalX, FinalY)))
                break;
        }
        //Console.WriteLine(iter);



        return iter;
    }
    private bool MakeMoves(Tuple<int, int> state, HashSet<Tuple<int, int>> attendedStateNew)
    {
        //if (attendedStateNew.Contains(state))
        //    return false;
        int stateX = state.Item1;
        int stateY = state.Item2;
        if (stateX == FinalX && stateY == FinalY)
            return true;

        if (stateX == 0 && stateY == -1)
        {

            if ((stateX == FinalX && stateY + 1 == FinalY) || ((stateY + 1 < SizeY) && State[stateY + 1, stateX] == 0))
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY + 1));
            attendedStateNew.Add(new Tuple<int, int>(stateX, stateY));
        }
        else
        {
            if ((stateX + 1 < SizeX) && State[stateY, stateX + 1] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX + 1, stateY));

            if ((stateX == FinalX && stateY + 1 == FinalY) || ((stateY + 1 < SizeY) && State[stateY + 1, stateX] == 0))
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY + 1));

            if ((stateX - 1 >= 0) && State[stateY, stateX - 1] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX - 1, stateY));

            if ((stateY - 1 >= 0) && State[stateY - 1, stateX] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY - 1));

            if (State[stateY, stateX] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY));
        }




        return false;

    }
    private bool MakeMovesBack(Tuple<int, int> state, HashSet<Tuple<int, int>> attendedStateNew)
    {
        //if (attendedStateNew.Contains(state))
        //    return false;
        int stateX = state.Item1;
        int stateY = state.Item2;
        if (stateX == StartX && stateY == StartY)
            return true;

        if (stateX == FinalX && stateY == FinalY)
        {


            if ((stateX == StartX && stateY - 1 == StartY) || (stateY - 1 >= 0) && State[stateY - 1, stateX] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY - 1));
            attendedStateNew.Add(new Tuple<int, int>(stateX, stateY));
        }
        else
        {
            if ((stateX + 1 < SizeX) && State[stateY, stateX + 1] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX + 1, stateY));

            if ( (stateY + 1 < SizeY) && State[stateY + 1, stateX] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY + 1));

            if ((stateX - 1 >= 0) && State[stateY, stateX - 1] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX - 1, stateY));

            if ((stateX == StartX && stateY - 1 ==StartY) || (stateY - 1 >= 0) && State[stateY - 1, stateX] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY - 1));

            if (State[stateY, stateX] == 0)
                attendedStateNew.Add(new Tuple<int, int>(stateX, stateY));
        }




        return false;

    }
    private void MoveBlizzard()
    {
        byte[,] oldState = State;
        byte[,] newState = new byte[SizeY, SizeX];
        byte stateXY;
        int i, j;
        for (i = 0; i < SizeY; i++)
            for (j = 0; j < SizeX; j++)
            {
                stateXY = State[i, j];

                if ((stateXY & (1 << 0)) > 0)
                    newState[Y(i), X(j + 1)] |= (1 << 0);

                if ((stateXY & (1 << 1)) > 0)
                    newState[Y(i + 1), X(j)] |= (1 << 1);

                if ((stateXY & (1 << 2)) > 0)
                    newState[Y(i), X(j - 1)] |= (1 << 2);

                if ((stateXY & (1 << 3)) > 0)
                    newState[Y(i - 1), X(j)] |= (1 << 3);
            }
        State = newState;
    }

    private void Print()
    {
        byte stateXY;
        int i, j;
        for (i = 0; i < SizeY; i++)
        {
            Console.WriteLine();
            for (j = 0; j < SizeX; j++)
            {
                stateXY = State[i, j];
                if (stateXY < 10)
                {
                    Console.Write("0" + stateXY + " ");
                }
                else
                    Console.Write("" + stateXY + " ");
            }
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    public int X(int x) { return (x + SizeX) % SizeX; }
    public int Y(int y) { return (y + SizeY) % SizeY; }
}