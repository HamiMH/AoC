

using System.Collections;
using System.Threading.Tasks;

internal class Node: IComparable<Node>
{

    public List<Node> Children { get; set; }
    public int value = -1;
    public bool ToFind;
    public string? Mask;

    public Node(string inStr, ref int index, int length,bool toFind=false)
    {
        if (index == 0)
            Mask = inStr;
        ToFind = toFind;
        Children = new List<Node>();
        int val = 0;
        bool lastNumber = false;
        if ((inStr[index] >= '0' && inStr[index] <= '9') || inStr[index] == '[')
        {
            for (; index < length; index++)
            {
                if (inStr[index] >= '0' && inStr[index] <= '9')
                {
                    val *= 10;
                    val += (inStr[index] - '0');
                    lastNumber = true;
                }
                else if (inStr[index] == ',')
                {
                    Children.Add(new Node(val));
                    val = 0;
                    lastNumber = false;
                    continue;
                }
                else if (inStr[index] == '[')
                {
                    index++;
                    Children.Add(new Node(inStr, ref index, length));
                    if (index< length && inStr[index] != ',')
                        index--;
                    lastNumber = false;
                    continue;
                }
                else if (inStr[index] == ']')
                {
                    index++;
                    if(lastNumber)
                        Children.Add(new Node(val));
                    lastNumber = false;
                    return;
                }
                else
                {
                    throw new Exception("Node/Node: if (inStr[index]>='0' && inStr[index] <= '9')");
                }
            }
        }
        else if (inStr[index] == ']')
        {
            index++;
            return;
        }
        else
        {
            throw new Exception("Node/Node: if");
        }
    }
    public Node(int val)
    {
        Children = new List<Node>();
        value = val;
    }
    private void ConvertValNodeIntoListNode()
    {
        Children.Add(new Node(value));
        value = -1;

    }

    internal static bool? RightOrder(Node first, Node second)
    {
        if (first.value != -1 && second.value != -1)
        {
            switch (first.value - second.value)
            {
                case < 0:
                    return true;
                case 0:
                    return null;
                case > 0:
                    return false;
            }
        }
        if (first.value != -1)
            first.ConvertValNodeIntoListNode();
        if (second.value != -1)
            second.ConvertValNodeIntoListNode();

        int firstLen = first.Children.Count;
        int secondLen = second.Children.Count;

        int minOfLen = Min(firstLen, secondLen);
        bool? boolRes;
        for (int i = 0; i < minOfLen; i++)
        {
            boolRes = RightOrder(first.Children[i], second.Children[i]);
            if (boolRes == null)
                continue;
            else
                return boolRes;
        }
        if (firstLen < secondLen)
            return true;
        else if (firstLen > secondLen)
            return false;
        else
            return null;
    }

    private static int Min(int firstLen, int secondLen)
    {
        if (firstLen < secondLen)
            return firstLen;
        else
            return secondLen;
    }


    int IComparable<Node>.CompareTo(Node? other)
    {
        if (RightOrder(this, (Node)other) != false)
            return -1;
        else
            return 1;
    }
}