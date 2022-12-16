using System.Xml.Linq;

internal class FastLayout
{


    //public List<string> StartPlaces = new List<string>();

    public Dictionary<string, FastNode> FastNodes = new Dictionary<string, FastNode>();


    public Dictionary<string, int> Memo = new Dictionary<string, int>();
    public long AllOpen = 0;

    public FastNode TestNode;

    public FastLayout(CaveLayout caveLayout)
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


    public int Simulate()
    {
        int maxValue = 0;
        long lo = 0;

        foreach (KeyValuePair<FastNode, int> fn1 in TestNode.Adjenced)
            foreach (KeyValuePair<FastNode, int> fn2 in TestNode.Adjenced)
            {
                maxValue = Math.Max(maxValue, GetMaxPre2(fn1.Key.Name, fn2.Key.Name, fn1.Value, fn2.Value, lo, lo));
            }

        return maxValue;
    }
    //public int SimulateOld()
    //{
    //    int maxValue = 0;

    //    int i, j;
    //    long lo = 0;
    //    for (i = 0; i < StartPlaces.Count(); i++)
    //        for (j = i; j < StartPlaces.Count; j++)
    //            maxValue = Math.Max(maxValue, GetMaxPre2(StartPlaces[i], StartPlaces[j], 1, 1, lo, lo));



    //    return maxValue;
    //}

    internal int GetMaxPre2(string name, string name2, int time1, int time2, long opened1, long opened2)
    {
        if (time1 >= 26 || time2 >= 26)
            return 0;

        if (time1 > time2)
            return GetMaxPre2(name2, name, time2, time1, opened2, opened1);

        if (opened1 == AllOpen && opened2 == AllOpen)
        {
            if (time1 < time2)
                return GetMaxPre2(name, name2, time1 + 1, time2, opened1, opened2) + GetGain(opened1);
            else if (time1 > time2)
                return GetMaxPre2(name, name2, time1, time2 + 1, opened1, opened2) + GetGain(opened1);
            else
                return GetMaxPre2(name, name2, time1 + 1, time2 + 1, opened1, opened2) + GetGain(opened1);
        }

        if (Memo.ContainsKey(name + " " + name2 + " " + time1 + " " + time2 + " " + opened1 + " " + opened2))
            return Memo[name + " " + name2 + " " + time1 + " " + time2 + " " + opened1 + " " + opened2];
        if (Memo.ContainsKey(name2 + " " + name + " " + time2 + " " + time1 + " " + opened2 + " " + opened1))
            return Memo[name2 + " " + name + " " + time2 + " " + time1 + " " + opened2 + " " + opened1];

        int maxGain = 0;
        int tmpGain;
        FastNode node1 = FastNodes[name];
        FastNode node2 = FastNodes[name2];

        if (time1 == time2)
        {
            opened1 |= opened2;
            opened2 = opened1;
            if (node1.IsOpened == false && node2.IsOpened == false && node1.Name != node2.Name)
            {
                node1.IsOpened = true;
                node2.IsOpened = true;
                tmpGain = GetMaxPre2(name, name2, time1 + 1, time2 + 1, opened1 | (1 << node1.Index) | (1 << node2.Index), opened2 | (1 << node1.Index) | (1 << node2.Index));
                maxGain = Math.Max(tmpGain, maxGain);
                node1.IsOpened = false;
                node2.IsOpened = false;
            }

            if (node1.IsOpened == false)
            {
                node1.IsOpened = true;

                foreach (KeyValuePair<FastNode, int> adj2 in node2.Adjenced)
                {
                    tmpGain = GetMaxPre2(name, adj2.Key.Name, time1 + 1, time2 + adj2.Value, opened1 | (1 << node1.Index), opened2 | (1 << node1.Index));
                    maxGain = Math.Max(tmpGain, maxGain);
                }
                node1.IsOpened = false;
            }

            if (node2.IsOpened == false && node2.Flow > 0)
            {
                node2.IsOpened = true;

                foreach (KeyValuePair<FastNode, int> adj1 in node1.Adjenced)
                {
                    tmpGain = GetMaxPre2(adj1.Key.Name, name2, time1 + adj1.Value, time2+1, opened1 | (1 << node2.Index), opened2 | (1 << node2.Index));
                    maxGain = Math.Max(tmpGain, maxGain);
                }
                node2.IsOpened = false;
            }

            foreach (KeyValuePair<FastNode, int> adj1 in node1.Adjenced)
            {
                foreach (KeyValuePair<FastNode, int> adj2 in node2.Adjenced)
                {
                    tmpGain = (Math.Min(adj1.Value, adj2.Value)-1) * GetGain(opened1)+ GetMaxPre2(adj1.Key.Name, adj2.Key.Name, time1 + adj1.Value, time2 + adj2.Value, opened1, opened2);
                    maxGain = Math.Max(tmpGain, maxGain);
                }
            }
        }
        else
        {
            if (node1.IsOpened == false)
            {
                node1.IsOpened = true;
                tmpGain = GetMaxPre2(name, name2, time1 + 1, time2, opened1 | (1 << node1.Index), opened2 | (1 << node1.Index));
                maxGain = Math.Max(tmpGain, maxGain);
                node1.IsOpened = false;
            }
            foreach (KeyValuePair<FastNode, int> adj1 in node1.Adjenced)
            {
                tmpGain = (Math.Min(time2 - time1, adj1.Value)-1) * GetGain(opened1)+ GetMaxPre2(adj1.Key.Name, name2, time1 + adj1.Value, time2, opened1 | (1 << node1.Index), opened2 | (1 << node1.Index));
                maxGain = Math.Max(tmpGain, maxGain);
            }
        }

        Memo.Add(name + " " + name2 + " " + time1 + " " + time2 + " " + opened1 + " " + opened2, maxGain + GetGain(opened1));
        return maxGain + GetGain(opened1);
    }

    private int GetGain(long opened)
    {
        long I = 1;
        int result = 0;
        foreach (FastNode fastNode in FastNodes.Values)
        {
            if ((opened & (I << fastNode.Index)) > 0)
                result += fastNode.Flow;
        }
        return result;
    }
}