using System.Xml.Linq;

internal class FastLayout
{


    //public List<string> StartPlaces = new List<string>();
    public int TimeOfEnd = 30;
    public Dictionary<string, FastNode> FastNodes = new Dictionary<string, FastNode>();


    //public Dictionary<string, int> Memo = new Dictionary<string, int>();
    public int[,,] Memo ;

    public int AllOpen = 0;

    public FastNode TestNode;

    public FastLayout(CaveLayout caveLayout, int timeOfEnd)
    {

        int index = 0;
        foreach (Node node in caveLayout.Nodes.Values)
        {
            if (node.Flow == 0)
                continue;

            FastNode fastNode = new FastNode(node.Name, node.Flow, index);
            FastNodes.Add(fastNode.Name, fastNode);
            AllOpen |= (1 << index);
            index++;
        }

        foreach (Node node in caveLayout.Nodes.Values)
        {
            if (node.Flow == 0)
                continue;

            FastNode fastNode = FastNodes[node.Name];
            caveLayout.ResetNodes();
            RunBfs(fastNode, node);
        }

        caveLayout.ResetNodes();
        TestNode = new FastNode("AA", 0, -1);
        RunBfs(TestNode, caveLayout.Nodes["AA"]);
        Memo = new int[FastNodes.Count , timeOfEnd, AllOpen + 1];
        int i,j, k;
        for (i = 0; i < FastNodes.Count; i++)
            for (j = 0; j < timeOfEnd; j++)
                for (k = 0; k < AllOpen + 1; k++)
                    Memo[i, j, k] = -1;
    }

    private void RunBfs(FastNode fastNode, Node node)
    {
        Queue<Node> queOfNodes = new Queue<Node>();
        node.Distance = 0;
        node.Attended = true;
        queOfNodes.Enqueue(node);
        Node tmp;
        while (queOfNodes.Count() > 0)
        {
            tmp = queOfNodes.Dequeue();
            if (node != tmp)
            {
                if (tmp.Flow > 0)
                    fastNode.AddAdjenced(FastNodes[tmp.Name], tmp.Distance);
            }
            foreach (Node adj in tmp.Adjenced)
            {
                if (adj.Attended)
                    continue;
                adj.Distance = tmp.Distance + 1;
                adj.Attended = true;
                queOfNodes.Enqueue(adj);
            }
        }
    }


    public int Simulate(int timeOfEnd)
    {
        TimeOfEnd = timeOfEnd;
        int maxValue = 0;
        int lo = 0;

        foreach (KeyValuePair<FastNode, int> fn in TestNode.Adjenced)
        {
            lo = 0;
            if ((lo & (1 << fn.Key.Index)) == 0)
                maxValue = Math.Max(
                    maxValue
                    ,
                    GetMaxFlow(fn.Key.Name, fn.Value + 1, lo | (1 << fn.Key.Index)) + fn.Key.GetValueToEnd(fn.Value + 1, TimeOfEnd));
        }

        return maxValue;
    }
    public int Simulate2(int timeOfEnd)
    {
        TimeOfEnd = timeOfEnd;
        int maxValueOfFirst ;
        int maxValueOfSecond;
        int maxValue = 0;
        int lo = 0;

        int first;
        int second;
        int nOfOnes;
        for (first = 0; first <= AllOpen/2; first++)
        {
            nOfOnes = GetNofOnes(first);
            if (nOfOnes < 3 || nOfOnes > (FastNodes.Count - 3))
                continue;

            maxValueOfFirst = 0;
            maxValueOfSecond = 0;
            second = first ^ AllOpen;

            foreach (KeyValuePair<FastNode, int> fn in TestNode.Adjenced)
            {
                lo = first;
                if ((lo & (1 << fn.Key.Index)) == 0)
                    maxValueOfFirst = Math.Max(
                        maxValueOfFirst
                        ,
                        GetMaxFlow(fn.Key.Name, fn.Value + 1, lo | (1<< fn.Key.Index)) + fn.Key.GetValueToEnd(fn.Value + 1, TimeOfEnd));
            }
            foreach (KeyValuePair<FastNode, int> fn in TestNode.Adjenced)
            {
                lo = second;
                if ((lo & (1 << fn.Key.Index)) == 0)
                    maxValueOfSecond = Math.Max(
                        maxValueOfSecond
                        ,
                        GetMaxFlow(fn.Key.Name, fn.Value + 1, lo | (1 << fn.Key.Index)) + fn.Key.GetValueToEnd(fn.Value + 1, TimeOfEnd));
            }
            maxValue= Math.Max(
                        maxValue
                        ,
                        maxValueOfFirst+maxValueOfSecond
                        );
        }


        return maxValue;
    }

    private int GetNofOnes(int first)
    {

        int nofOnes = 0;
        for(int i=0;i<30;i++)
            if((first&(1<<i))>0)
                nofOnes++;
        return nofOnes;
    }

    internal int GetMaxFlow(string place, int time, int opened)
    {
        if (time > TimeOfEnd)
            return -100000;

        if (time == TimeOfEnd)
            return 0;


        if (opened == AllOpen)
        {
            return 0;
        }

        FastNode node = FastNodes[place];
        if (Memo[node.Index, time, opened] != -1)
            return Memo[node.Index, time, opened];
        //if (Memo.ContainsKey(place + " " + time + " " + opened))
        //    return Memo[place + " " + time + " " + opened];

        int maxValue = 0;

        foreach (KeyValuePair<FastNode, int> fn in node.Adjenced)
        {
            if ((opened & (1 << fn.Key.Index)) > 0)
                continue;
            maxValue = Math.Max(
                maxValue
                ,
                GetMaxFlow(fn.Key.Name, time + fn.Value + 1, opened | (1 << fn.Key.Index)) + fn.Key.GetValueToEnd(time + fn.Value + 1, TimeOfEnd) 
                );
        }

        //Memo.Add(place + " " + time + " " + opened, maxValue);
        Memo[node.Index, time, opened]= maxValue;
        return maxValue;
    }
}