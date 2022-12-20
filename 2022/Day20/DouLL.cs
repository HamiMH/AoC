using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class DouLL
    {
        public Node[] InitStates;
        public static long LenOfLL;

        public DouLL(List<string> inputCol, long dk)
        {
            LenOfLL = inputCol.Count;
            InitStates = new Node[LenOfLL];
            int i;
            for (i = 0; i < LenOfLL; i++)
            {
                //if (int.Parse(inputCol[i]) == 0)
                //     b = 4;
                InitStates[i] = new Node(long.Parse(inputCol[i]) * dk);

            }
            for (i = 0; i < LenOfLL; i++)
            {
                InitStates[i].Previous = InitStates[(i - 1 + LenOfLL) % LenOfLL];
                InitStates[i].Next = InitStates[(i + 1) % LenOfLL];
            }
        }

        public long Simulate(int nOfIter = 1)
        {
            long ret = 0;
            int i;
            int iter;
            for (iter = 0; iter < nOfIter; iter++)
                for (i = 0; i < LenOfLL; i++)
                    if (InitStates[i].Value != 0)
                        Node.MoveNode(InitStates[i], InitStates[i].Value);

            Node first = Node.GetFirst(InitStates[0]);

            ret += Node.NthNodeAfter(first, 1000).Value;
            ret += Node.NthNodeAfter(first, 2000).Value;
            ret += Node.NthNodeAfter(first, 3000).Value;

            return ret;
        }

    }

    internal class Node
    {
        public Node Next;
        public Node Previous;
        public long Value;

        public Node(long val)
        {
            Value = val;
        }

        public static Node NthNodeAfter(Node now, long nOfMoves)
        {
            while (nOfMoves != 0)
            {
                if (nOfMoves > 0)
                {
                    now = now.Next;
                    nOfMoves--;
                }
                else
                {
                    now = now.Previous;
                    nOfMoves++;
                }
            }
            return now;
        }

        public static Node GetFirst(Node now)
        {
            while (true)
            {
                if (now.Value == 0)
                    return now;
                now = now.Next;
            }
        }

        public static Node RemoveNext(Node now)
        {
            Node next = now.Next;
            Node nextNext = next.Next;
            now.Next = nextNext;
            nextNext.Previous = now;
            return next;
        }
        public static void MoveNode(Node now, long inNOfMoves)
        {
            long l1 = 1;
            long nOfMoves = inNOfMoves % (DouLL.LenOfLL - l1);

            Node prev = now.Previous;
            RemoveNext(prev);

            while (nOfMoves != 0)
            {
                if (nOfMoves > 0)
                {
                    prev = prev.Next;
                    nOfMoves--;
                }
                else
                {
                    prev = prev.Previous;
                    nOfMoves++;
                }
            }
            PlaceAfter(prev, now);
        }

        private static void PlaceAfter(Node prev, Node now)
        {
            Node prevNext = prev.Next;

            prev.Next = now;
            now.Next = prevNext;
            now.Previous = prev;
            prevNext.Previous = now;
        }
    }
}
