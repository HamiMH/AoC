using System.Xml.Linq;

public class MonkeyGraph
{
    public Dictionary<string, Node> DictOfNodes = new Dictionary<string, Node>();

    public MonkeyGraph(List<string> inputCol)
    {
        string[] strArr;
        Node mainNode;
        Node prevNode1;
        Node prevNode2;
        foreach(string str in inputCol)
        {
            strArr = str.Split(":");
            mainNode=GetNode(strArr[0].Trim());

            if (strArr[1].Contains('+'))
            {
                strArr=strArr[1].Trim().Split('+');
                prevNode1=GetNode(strArr[0].Trim());
                prevNode2=GetNode(strArr[1].Trim());
                mainNode.Set(prevNode1, prevNode2, '+');
            }
            else if (strArr[1].Contains('-'))
            {
                strArr = strArr[1].Trim().Split('-');
                prevNode1 = GetNode(strArr[0].Trim());
                prevNode2 = GetNode(strArr[1].Trim());
                mainNode.Set(prevNode1, prevNode2, '-');
            }
            else if (strArr[1].Contains('*'))
            {
                strArr = strArr[1].Trim().Split('*');
                prevNode1 = GetNode(strArr[0].Trim());
                prevNode2 = GetNode(strArr[1].Trim());
                mainNode.Set(prevNode1, prevNode2, '*');
            }
            else if (strArr[1].Contains('/'))
            {
                strArr = strArr[1].Trim().Split('/');
                prevNode1 = GetNode(strArr[0].Trim());
                prevNode2 = GetNode(strArr[1].Trim());
                mainNode.Set(prevNode1, prevNode2, '/');
            }
            else
            {
                mainNode.Set(long.Parse(strArr[1].Trim()));
            }
        }
    }

    public Node GetNode(string name)
    {
        if(DictOfNodes.ContainsKey(name))
            return DictOfNodes[name];
        else
        {
            Node newNode = new Node(name);
            DictOfNodes.Add(name, newNode);
            return newNode;
        }
    }

    public long DFS(Node mainNode)
    {
        if (mainNode.Value != -1)
        {
            return mainNode.Value;
        }
        else
        {
            switch (mainNode.Oper)
            {
                case '+':
                    return DFS(mainNode.Prev1) + DFS(mainNode.Prev2);
                case '*':
                    return DFS(mainNode.Prev1) * DFS(mainNode.Prev2);
                case '-':
                    return DFS(mainNode.Prev1) - DFS(mainNode.Prev2);
                default:
                    return DFS(mainNode.Prev1) / DFS(mainNode.Prev2);
            }
        }
    }

    internal long DFS2(Node mainNode)
    {
        if (mainNode.ItIsMe)
            return -2;

        if (mainNode.Value > -1)
        {
            return mainNode.Value;
        }
        else
        {
            long prevVal1 = DFS2(mainNode.Prev1);
            long prevVal2 = DFS2(mainNode.Prev2);
            if (prevVal1 == -2 || prevVal2 == -2)
                return -2;
            switch (mainNode.Oper)
            {
                case '+':
                    mainNode.Value = prevVal1 + prevVal2;
                    return prevVal1 + prevVal2;
                case '*':
                    mainNode.Value = prevVal1 * prevVal2;
                    return prevVal1 * prevVal2;
                case '-':
                    mainNode.Value = prevVal1 - prevVal2;
                    return prevVal1 - prevVal2;
                default:
                    mainNode.Value = prevVal1 / prevVal2;
                    return prevVal1 / prevVal2;
            }
        }
    }

    internal void DFCalculation(Node mainNode)
    {
        if (mainNode.Prev1 == null || mainNode.Prev2 == null)
            return;

        Node prev1=mainNode.Prev1;
        Node prev2=mainNode.Prev2;

        if (prev1.Value == -1 && prev2.Value == -1)
            throw new Exception("prev1.Value==-1 && prev2.Value==-1");
        if (prev1.Value == -1)
        {
            switch (mainNode.Oper)
            {
                case '=':
                    prev1.Value =  prev2.Value;
                    DFCalculation(prev1);
                    return;
                case '+':
                    prev1.Value=mainNode.Value - prev2.Value;
                    DFCalculation(prev1);
                    return;
                case '*':
                    prev1.Value = mainNode.Value / prev2.Value;
                    DFCalculation(prev1);
                    return;
                case '-':
                    prev1.Value = mainNode.Value + prev2.Value;
                    DFCalculation(prev1);
                    return;
                default:
                    prev1.Value = mainNode.Value * prev2.Value;
                    DFCalculation(prev1);
                    return;
            }
        }
        else
        {
            switch (mainNode.Oper)
            {
                case '=':
                    prev2.Value = prev1.Value;
                    DFCalculation(prev1);
                    return;
                case '+':
                    prev2.Value = mainNode.Value - prev1.Value;
                    DFCalculation(prev2);
                    return;
                case '*':
                    prev2.Value = mainNode.Value / prev1.Value;
                    DFCalculation(prev2);
                    return;
                case '-':
                    prev2.Value =   prev1.Value- mainNode.Value;
                    DFCalculation(prev2);
                    return;
                default:
                    prev2.Value = prev1.Value/mainNode.Value ;
                    DFCalculation(prev2);
                    return;
            }
        }
    }
}

public class Node
{
    public string Name;
    public long Value = -1;
    public Node? Prev1;
    public Node? Prev2;
    public char? Oper;
    public bool ItIsMe = false;


    public Node(string name)
    {
        Name = name;
    }

    public void Set(Node prev1, Node prev2,char c)
    {
        Prev1 = prev1;
        Prev2 = prev2;
        Oper = c;
    }
     public void Set(long value)
    {
        Value = value;
        Value = value;
    }


}