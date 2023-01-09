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


    internal void ResetNodes()
    {
        foreach (Node node in Nodes.Values)
        {
            node.Distance = -1;
            node.Attended = false;
        }
    }
}