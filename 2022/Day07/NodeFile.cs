using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    internal class NodeFile : NodeBase
    {
        public NodeFile(string name, int size) : base(name, size)
        {
        }

        internal override void AddChild(string strTmp, int v)
        {
            throw new Exception("NodeFile.AddChild");
        }

        internal override NodeBase GetChild(string strTmp)
        {
            throw new NotImplementedException();
        }

        internal override long SmartDfs(ref long result)
        {
            return Size;
        }

        internal override void SmartDfsSmallest(ref long result, long spaceToFree)
        {
            return;
        }
    }
}
