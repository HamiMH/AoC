using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18.Cmd
{
    public class CmdAdd : CmdBase
    {
        public override long ExecCmd(long first, long second)
        {
            return first + second;
        }

        public override char GetCmdType()
        {
            return '+';
        }

        public CmdAdd():base(3) {}
    }
}
