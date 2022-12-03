using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    internal class UltimateParser
    {
        private char[] StringToCalculate { get; set; }

        private Stack<CmdBase> Cmds { get; set; }
        private Stack<long> Values { get; set; }

        public UltimateParser(string tmpStr)
        {
            this.StringToCalculate = tmpStr.ToCharArray();
            this.Cmds = new Stack<CmdBase>();
            this.Values = new Stack<long>();
        }



        internal long Calculate()
        {
            this.Cmds = new Stack<CmdBase>();
            this.Values = new Stack<long>();

            CmdBase c;
            bool workingWithNumber=false;
            int negative = 1;
            int inpNumber = 0;
            char? lastCc=null;
            foreach (char cc in StringToCalculate)
            {
                if (cc=='-' &&(lastCc == null || lastCc == '('))
                {
                    negative = -1;
                    lastCc = cc;
                    continue;
                }
                if (cc >= '0' && cc <= '9')
                {
                    //Values.Push(cc - '0');
                    inpNumber *= 10;
                    inpNumber += (cc - '0');
                    workingWithNumber=true;

                    lastCc = cc;
                    continue;
                }
                if(cc == '(' && (lastCc == '-'))
                {
                    Cmds.Push(CmdBase.Create('*'));
                    Values.Push(-1);
                }

                if (workingWithNumber)
                {
                    Values.Push(negative*inpNumber);
                }

                workingWithNumber = false;
                inpNumber = 0;
                negative = 1;

                c = CmdBase.Create(cc);

                switch (cc)
                {
                    case '+':
                    case '*':
                                            
                        while (Cmds.Any() && PreviousCmdHasPrio(c))
                        {
                            Values.Push(Cmds.Pop().ExecCmd(Values.Pop(), Values.Pop()));
                        }
                        Cmds.Push(c);
                        break;
                    case '(':
                        Cmds.Push(c);
                        break;
                    case ')':
                        while (Cmds.Peek().GetCmdType() != '(')
                        {
                            Values.Push(Cmds.Pop().ExecCmd(Values.Pop(), Values.Pop()));
                        }
                        Cmds.Pop();
                        break;
                }
                lastCc = cc;
            }
            if (workingWithNumber)
            {
                Values.Push(negative*inpNumber);
            }
            while (Cmds.Any())
            {
                Values.Push(Cmds.Pop().ExecCmd(Values.Pop(), Values.Pop()));
            }
            return Values.Pop();
        }

        private bool PreviousCmdHasPrio(CmdBase c)
        {
            if(Cmds.Peek().Prio==0)
                return false;
            if (Cmds.Peek().Prio<c.Prio)
                return true;
            else
                return false;
        }
    }
}
