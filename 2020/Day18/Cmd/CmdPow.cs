using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18.Cmd
{
    public class CmdPow : CmdBase
    {
        public override long ExecCmd(long first, long second)
        {
            long ret = 1;
            for(int i =0; i<second;i++)
                ret*=first;

            return ret;
        }

        public override char GetCmdType()
        {
            return '^';
        }

        public CmdPow():base(1) {}
    }
}
