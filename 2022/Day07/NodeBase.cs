using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    internal abstract class NodeBase
    {
        public string Name {  get; set; }
        public long Size { get; set; }
        public NodeBase? Ancestor { get; set; }

        public NodeBase(string name, int size)
        {
            Name = name;
            Size = size;
        }

        internal NodeBase GetAncestor()
        {
            if (Ancestor == null)
                throw new Exception("GetAncestor");
            return Ancestor;
        }

        internal abstract void AddChild(string strTmp, int v);
        internal abstract NodeBase GetChild(string strTmp);
        internal abstract long SmartDfs(ref long result);
        internal abstract void SmartDfsSmallest(ref long result, long spaceToFree);
    }
}
