using Day19;
using System;
using System.Collections.Generic;
using System.Linq;

internal class BluePrint
{


    private const int ORE = 0;
    private const int CLAY = 1;
    private const int OBSI = 2;
    private const int GEOD = 3;
    public static readonly int NOfGeodes = 4;

    //private int[] Robots = new int[NOfGeodes];
    //private int[] Material = new int[NOfGeodes];
    public static int[,] Cost = new int[NOfGeodes, NOfGeodes];
    public static int[] CostOreHiearch = new int[NOfGeodes];
    private int[] MaxUsefulRobots = new int[NOfGeodes];

    //List<SortedSet<StateOfSim>> sortedSets = new List<SortedSet<StateOfSim>>();
    Dictionary<string, int> MEMO = new Dictionary<string, int>();
    Dictionary<int, int>[,,,,] MEMO2;

    public BluePrint()
    {
    }


    private void Clear()
    {
        int i, j;
        for (i = 0; i < NOfGeodes; i++)
            for (j = 0; j < NOfGeodes; j++)
                Cost[i, j] = 0;

    }

    internal int GetBestOresDFS(int nOfSteps)
    {
        MEMO.Clear();
        State state = new State(new int[] { 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0 });

        int result = RunDFS(nOfSteps, state);
        return result;

    }

    private int RunDFS(int nOfSteps, State state)
    {
        if (nOfSteps < 0)
            return -10000;
        if (nOfSteps == 0)
            return state.Material[3];
        NeededMaterial(nOfSteps, state);
        //if (MEMO.ContainsKey("T: " + nOfSteps + ", " + state.ToString()))
        //    return MEMO["T: " + nOfSteps + ", " + state.ToString()];

        if (MEMO2[nOfSteps, state.Robots[0], state.Robots[1], state.Robots[2], state.Robots[3]].ContainsKey(state.MaterialHash()))
            return MEMO2[nOfSteps, state.Robots[0], state.Robots[1], state.Robots[2], state.Robots[3]][state.MaterialHash()];

        int nOfMoves;
        int maxValue = state.Material[3] + nOfSteps * state.Robots[3];
        for (int type = 0; type < 4; type++)
        {
            if (state.Robots[type] >= MaxUsefulRobots[type])
                continue;
            if (CanBuild(type, state.Robots) == false)
                continue;
            nOfMoves = MovesToCanBuild(type, state.Robots, state.Material);

            maxValue = Math.Max(maxValue, RunDFS(nOfSteps - (nOfMoves + 1), state.Build(type, nOfMoves + 1)));
        }

        //MEMO.Add("T: " + nOfSteps + ", " + state.ToString(), maxValue);
        MEMO2[nOfSteps, state.Robots[0], state.Robots[1], state.Robots[2], state.Robots[3]].Add(state.MaterialHash(), maxValue);
        return maxValue;
    }


    internal void SetBP(string inputStr, int time)
    {
        Clear();
        string[] parts = inputStr.Split(new char[] { ' ' });
        Cost[0, 0] = int.Parse(parts[6]);
        Cost[1, 0] = int.Parse(parts[12]);
        Cost[2, 0] = int.Parse(parts[18]);
        Cost[2, 1] = int.Parse(parts[21]);
        Cost[3, 0] = int.Parse(parts[27]);
        Cost[3, 2] = int.Parse(parts[30]);
        MaxUsefulRobots[0] = Math.Max(Math.Max(Math.Max(Cost[0, 0] - 1, Cost[1, 0]), Cost[2, 0]), Cost[3, 0]);
        MaxUsefulRobots[1] = Cost[2, 1];
        MaxUsefulRobots[2] = Cost[3, 2];
        MaxUsefulRobots[3] = time;

        MEMO2 = new Dictionary<int, int>[time + 1, MaxUsefulRobots[0] + 1, MaxUsefulRobots[1] + 1, MaxUsefulRobots[2] + 1, MaxUsefulRobots[3] + 1];
        int i, j, k, l, m;
        for (i = 0; i < time + 1; i++)
            for (j = 0; j < MaxUsefulRobots[0] + 1; j++)
                for (k = 0; k < MaxUsefulRobots[1] + 1; k++)
                    for (l = 0; l < MaxUsefulRobots[2] + 1; l++)
                        for (m = 0; m < MaxUsefulRobots[3] + 1; m++)
                            MEMO2[i, j, k, l, m] = new Dictionary<int, int>();

        CostOreHiearch = new int[] { -1, -1, -1, -1 };

        int maxIndex;
        int max;
        for (i = 0; i < 4; i++)
        {
            maxIndex = 0;
            max = 0;
            for (j = 0; j < 4; j++)
            {
                if (CostOreHiearch.Contains(j))
                    continue;
                if (Cost[j, 0] >= max)
                {
                    max = Cost[j, 0];
                    maxIndex = j;
                }
            }
            CostOreHiearch[i] = maxIndex;
        }
    }
    internal bool CanBuild(int type, int[] robots)
    {
        switch (type)
        {
            case 0:
                return true;
            case 1:
                return true;
            case 2:
                if (robots[1] > 0)
                    return true;
                else
                    return false;
            case 3:
                if (robots[2] > 0)
                    return true;
                else
                    return false;
            default:
                throw new NotImplementedException();

        }
    }
    internal int MovesToCanBuild(int type, int[] robots, int[] material)
    {
        switch (type)
        {
            case 0:
                if (Cost[0, 0] - material[0] <= 0)
                    return 0;
                return IntDiv((Cost[0, 0] - material[0]), robots[0]);
            case 1:
                if (Cost[1, 0] - material[0] <= 0)
                    return 0;
                return IntDiv((Cost[1, 0] - material[0]), robots[0]);
            case 2:
                int val0 = Math.Max(0, Cost[2, 0] - material[0]);
                int val1 = Math.Max(0, Cost[2, 1] - material[1]);
                return Math.Max(IntDiv(val0, robots[0]), IntDiv(val1, robots[1]));
            case 3:
                int val00 = Math.Max(0, Cost[3, 0] - material[0]);
                int val2 = Math.Max(0, Cost[3, 2] - material[2]);
                return Math.Max(IntDiv(val00, robots[0]), IntDiv(val2, robots[2]));
            default:
                throw new NotImplementedException();
        }
    }

    private int IntDiv(int a, int b)
    {
        int res = a / b;
        if (a % b > 0)
            res++;
        return res;
    }

    internal void NeededMaterial(int timeToEnd, State state)
    {
        int timeToEndTmp = timeToEnd;
        int mater0 = 0;
        int timeToRem;
        for (int i = 0; i < 4; i++)
        {
            timeToRem = Math.Min(MaxUsefulRobots[CostOreHiearch[i]] - state.Robots[CostOreHiearch[i]], timeToEndTmp);
            mater0 += timeToRem * Cost[CostOreHiearch[i], 0];
            timeToEndTmp -= timeToRem;
        }
        state.Material[0] = Math.Min(state.Material[0], mater0);
        //state.Material[0] = Math.Min(state.Material[0],
        //    Math.Min(MaxUsefulRobots[0] - state.Robots[0], timeToEnd) * Cost[0, 0] +
        //    Math.Min(MaxUsefulRobots[1] - state.Robots[1], timeToEnd) * Cost[1, 0] +
        //    Math.Min(MaxUsefulRobots[2] - state.Robots[2], timeToEnd) * Cost[2, 0] +
        //    Math.Min(MaxUsefulRobots[3] - state.Robots[3], timeToEnd) * Cost[3, 0]);

        state.Material[1] = Math.Min(state.Material[1],
            Math.Min(MaxUsefulRobots[2] - state.Robots[2], timeToEnd) * Cost[2, 1]);

        state.Material[2] = Math.Min(state.Material[2],
            Math.Min(MaxUsefulRobots[3] - state.Robots[3], timeToEnd) * Cost[3, 2]);


    }
}