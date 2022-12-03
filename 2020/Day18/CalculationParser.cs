internal class CalculationParser
{
    private char[] StringToCalculate { get; set; }

    private Stack<char> Cmds { get; set; }
    private Stack<long> Values { get; set; }

    public CalculationParser(string tmpStr)
    {
        this.StringToCalculate = tmpStr.ToCharArray();
        this.Cmds = new Stack<char>();
        this.Values = new Stack<long>();
    }



    internal long Calculate()
    {
        this.Cmds = new Stack<char>();
        this.Values = new Stack<long>();

        foreach (char c in StringToCalculate)
        {
            if (c >= '0' && c <= '9')
            {
                Values.Push(c-'0');
            }
            else
            {
                switch (c)
                {
                    case '+':

                        
                        Cmds.Push(c);
                        break;
                    case '*':
                        while (Cmds.Any() && PreviousCmdHasPrio(c))
                        {
                            Values.Push(ExecCmd(Cmds.Pop(), Values.Pop(), Values.Pop()));
                        }
                        Cmds.Push(c);
                        break;
                    case '(':
                        Cmds.Push(c);
                        break;
                    case ')':
                        while (Cmds.Peek()!='(')
                        {
                            Values.Push(ExecCmd(Cmds.Pop(), Values.Pop(), Values.Pop()));
                        }
                        Cmds.Pop();
                        break;
                }

            }
        }
        while (Cmds.Any() )
        {
            Values.Push(ExecCmd(Cmds.Pop(), Values.Pop(), Values.Pop()));
        }
        return Values.Pop();
    }

    private long ExecCmd(char cmd, long v1, long v2)
    {
        switch (cmd)
        {
            case '+':
                return v1 + v2;
            case '*':
                return v1 * v2;
            default:
                throw new Exception("No case in ExecCmd");
        }
    }

    private bool PreviousCmdHasPrio(char c)
    {
        if (Cmds.Peek() == '+' && c == '*')
            return true;
        else
            return false;
    }
}