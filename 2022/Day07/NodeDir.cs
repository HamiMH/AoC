using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    internal class NodeDir : NodeBase
    {
        protected List<NodeBase> ListOfNodes { get; set; }

        public NodeDir(string name, int size) : base(name, size)
        {
            ListOfNodes = new List<NodeBase>();
        }

        internal override void AddChild(string name, int size)
        {
            NodeBase newNode;
            if (size == 0)
                newNode = new NodeDir(name, size);
            else
                newNode = new NodeFile(name, size);

            newNode.Ancestor = this;
            ListOfNodes.Add(newNode);
        }

        internal override NodeBase GetChild(string strTmp)
        {
            foreach (NodeBase node in ListOfNodes)
            {
                if (node.Name == strTmp)
                    return node;
            }
            throw new Exception("NodeDir.GetChild");
        }

        internal override long SmartDfs(ref long result)
        {
            long toReturn = 0;
            foreach (NodeBase node in ListOfNodes)
            {
                toReturn += node.SmartDfs(ref result);
            }

            if (toReturn <= 100000)
                result += toReturn;
            Size = toReturn;
            return toReturn;
        }

        internal override void SmartDfsSmallest(ref long result, long spaceToFree)
        {
            if (Size < spaceToFree)
                return;
            if(Size<result)
                result= Size;
            foreach (NodeBase node in ListOfNodes)
            {
                node.SmartDfsSmallest(ref result, spaceToFree);
            }
        }
    }
}
