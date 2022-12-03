using Day18.Cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public abstract class CmdBase
    {
        public int Prio { get; }
        public abstract long ExecCmd(long first, long second);
        public abstract char GetCmdType();

        public CmdBase(int prio)
        {
            Prio = prio;
        }

        public static CmdBase Create(char c)
        {
            switch (c)
            {
                case '+':
                    return new CmdAdd();
                case '-':
                    return new CmdSub();
                case '*':
                    return new CmdMul();
                case '/':
                    return new CmdDiv();
                case '^':
                    return new CmdPow();
                case '(':
                    return new CmdLb(); 
                case ')':
                    return new CmdRb();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
