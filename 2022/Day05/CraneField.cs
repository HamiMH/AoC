using System.Text;

internal class CraneField
{

    private List<Stack<char>> Stacks;
    private Stack<char> _tmpStack;
    public CraneField(List<char[]> list)
    {
        _tmpStack=new Stack<char>();
        Stacks =new List<Stack<char>>();
        Stacks.Add(new Stack<char>());

        int i, j;
        int listRows=list.Count;
        int listCols = list[0].Count();

        for(i = 0; i < listCols; i++)
        {
            Stack<char> stack = new Stack<char>();
            Stacks.Add(stack);

            for (j = listRows - 1; j >= 0; j--)
            {
                if (list[j][i] == '.')
                    break;
                stack.Push(list[j][i]);
            }
        }
    }

    internal void MakeMove(string strIn)
    {
        string[] strArr = strIn.Split("from");
        int nOfBoxes = int.Parse(strArr[0].Replace("move", "").Trim());
        string[] strArr1 = strArr[1].Split("to");
        int from = int.Parse(strArr1[0].Trim());
        int into = int.Parse(strArr1[1].Trim());

        for (int i = 0; i < nOfBoxes; i++)
        {
            Stacks[into].Push(Stacks[from].Pop());
        }

    }
    internal void MakeMoveFast(string strIn)
    {
        string[] strArr = strIn.Split("from");
        int nOfBoxes = int.Parse(strArr[0].Replace("move", "").Trim());
        string[] strArr1 = strArr[1].Split("to");
        int from = int.Parse(strArr1[0].Trim());
        int into = int.Parse(strArr1[1].Trim());

        for (int i = 0; i < nOfBoxes; i++)
        {
            _tmpStack.Push(Stacks[from].Pop());
        }
        for (int i = 0; i < nOfBoxes; i++)
        {
            Stacks[into].Push(_tmpStack.Pop());
        }
        

    }

    internal string GetTops()
    {
        StringBuilder stb=new StringBuilder();
        for (int i = 1; i < 10; i++)
        {
            if (Stacks[i].Count>0)
            stb.Append(Stacks[i].Peek());
        }
        return stb.ToString();
    }
}