using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18.Cmd
{
    public class CmdLb : CmdBase
    {
        public override long ExecCmd(long first, long second)
        {
            throw new NotImplementedException();
        }

        public override char GetCmdType()
        {
            return '(';
        }

        public CmdLb():base(0) {}
    }
}
