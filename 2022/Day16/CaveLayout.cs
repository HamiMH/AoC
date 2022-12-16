internal class CaveLayout
{

    public Dictionary<string, Node> Nodes = new Dictionary<string, Node>();

    public Dictionary<string, int> Memo = new Dictionary<string, int>();
    public long AllOpen = 0;

    public CaveLayout(List<string> inputCol)
    {
        List<Tuple<string, string>> listOfInputTup = new List<Tuple<string, string>>();

        string strTmp;
        string[] strTmpArr;
        string name;
        int flow;
        string nodes;
        int index = 0;
        foreach (string str in inputCol)
        {
            strTmp = str.Replace("Valve ", "").Trim();
            strTmpArr = strTmp.Split(" has flow rate=");
            name = strTmpArr[0];
            if (strTmpArr[1].Contains("tunnels lead to valves"))
                strTmpArr = strTmpArr[1].Split("; tunnels lead to valves ");
            else
                strTmpArr = strTmpArr[1].Split("; tunnel leads to valve ");

            flow = int.Parse(strTmpArr[0]);
            nodes = strTmpArr[1];

            listOfInputTup.Add(new Tuple<string, string>(name, nodes));
            Nodes.Add(name, new Node(name, flow, index));
            if (flow > 0)
                AllOpen |= (1 << index);
            index++;
        }
        string[] nodesArr;

        foreach (Tuple<string, string> tup in listOfInputTup)
        {
            name = tup.Item1;
            nodesArr = tup.Item2.Split(",");
            Node baseNode = Nodes[name];
            foreach (string str in nodesArr)
                baseNode.AddAdjenced(Nodes[str.Trim()]);
        }

    }

    internal int GetMaxPre(string name, int time, int gain, long opened)
    {

        if (time == 30)
            return 0;
        if (Memo.ContainsKey(name + " " + time + " " + opened))
            return Memo[name + " " + time + " " + opened];

        int maxGain = 0;
        int tmpGain;
        Node node = Nodes[name];
        if (node.IsOpened == false && node.Flow > 0)
        {
            node.IsOpened = true;
            tmpGain = GetMaxPre(name, time + 1, gain + node.Flow, opened | (1 << node.Index));
            maxGain = Math.Max(tmpGain, maxGain);
            node.IsOpened = false;
        }
        foreach (Node adj in node.Adjenced)
        {
            tmpGain = GetMaxPre(adj.Name, time + 1, gain, opened);
            maxGain = Math.Max(tmpGain, maxGain);
        }

        Memo.Add(name + " " + time + " " + opened, maxGain + gain);
        return maxGain + gain;
    }

    internal int GetMaxPre2(string name, string name2, int time, int gain, long opened)
    {
        if (time == 26)
            return 0;

        if (opened == AllOpen)
            return GetMaxPre2(name, name2, time + 1, gain, opened) + gain;

        if (Memo.ContainsKey(name + " " + name2 + " " + time + " " + opened))
            return Memo[name + " " + name2 + " " + time + " " + opened];
        if (Memo.ContainsKey(name2 + " " + name + " " + time + " " + opened))
            return Memo[name2 + " " + name + " " + time + " " + opened];

        int maxGain = 0;
        int tmpGain;
        Node node = Nodes[name];
        Node node2 = Nodes[name2];
        if (node.IsOpened == false && node.Flow > 0 && node2.IsOpened == false && node2.Flow > 0 && node.Name != node2.Name)
        {
            node.IsOpened = true;
            node2.IsOpened = true;
            tmpGain = GetMaxPre2(name, name2, time + 1, gain + node.Flow + node2.Flow, opened | (1 << node.Index) | (1 << node2.Index));
            maxGain = Math.Max(tmpGain, maxGain);
            node.IsOpened = false;
            node2.IsOpened = false;
        }

        if (node.IsOpened == false && node.Flow > 0)
        {
            node.IsOpened = true;

            foreach (Node adj2 in node2.Adjenced)
            {
                tmpGain = GetMaxPre2(name, adj2.Name, time + 1, gain + node.Flow, opened | (1 << node.Index));
                maxGain = Math.Max(tmpGain, maxGain);
            }
            node.IsOpened = false;
        }

        if (node2.IsOpened == false && node2.Flow > 0)
        {
            node2.IsOpened = true;

            foreach (Node adj in node.Adjenced)
            {
                tmpGain = GetMaxPre2(adj.Name, name2, time + 1, gain + node2.Flow, opened | (1 << node2.Index));
                maxGain = Math.Max(tmpGain, maxGain);
            }
            node2.IsOpened = false;
        }

        foreach (Node adj in node.Adjenced)
        {
            tmpGain = GetMaxPre2(adj.Name, name2, time + 1, gain, opened);
            maxGain = Math.Max(tmpGain, maxGain);
        }

        foreach (Node adj2 in node2.Adjenced)
        {
            tmpGain = GetMaxPre2(name, adj2.Name, time + 1, gain, opened);
            maxGain = Math.Max(tmpGain, maxGain);
        }

        foreach (Node adj in node.Adjenced)
        {
            foreach (Node adj2 in node2.Adjenced)
            {
                tmpGain = GetMaxPre2(adj.Name, adj2.Name, time + 1, gain, opened);
                maxGain = Math.Max(tmpGain, maxGain);

            }
        }

        Memo.Add(name + " " + name2 + " " + time + " " + opened, maxGain + gain);
        return maxGain + gain;
    }

    internal void ResetNodes()
    {
        foreach (Node node in Nodes.Values)
        {
            node.Distance = -1;
            node.Attended = false;
        }
    }
}