using System;
using System.Collections.Generic;

namespace Day18
{

    public class Node
    {
        public Node prev;
        public Node next;
        public string item;

        public Node(Node _prev, Node _next, string _item)
        {
            prev = _prev;
            next = _next;
            item = _item;
        }
    }

    class MyLinkedList
    {
        public Node begin;
        public Node end;

        public void addToEnd(string str)
        {
            Node newEnd = new Node(end, null, str);
            end.next = newEnd;
            end = newEnd;
        }

        public void addToBegin(string str)
        {
            Node newBegin = new Node(null, begin, str);
            begin.prev = newBegin;
            begin = newBegin;
        }

        public MyLinkedList(string str)
        {
            Node nd = new Node(null, null, str);
            begin = nd;
            end = nd;
        }

    }
    class Program
    {
        static MyLinkedList mll;
        static void Main(string[] args)
        {
            
            List<string> inputCol = new List<string>();
            string lineIn1;
            while ((lineIn1 = Console.ReadLine()) != null)
            {
                if (lineIn1 == "")
                    break;
                inputCol.Add(lineIn1);
            }
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            int i, j, k;
            int inputColLen = inputCol.Count;
            /*part 1
            string pracString = inputCol[0];
            int pracStringLen = pracString.Length;

            mll = new MyLinkedList(pracString[0].ToString());
            for (j = 1; j < pracStringLen; j++)
                mll.addToEnd(pracString[j].ToString());

            for (i = 1; i < inputColLen; i++)
            {
                mll.addToBegin("[");
                mll.addToEnd(",");
                pracString = inputCol[i];
                pracStringLen = pracString.Length;

                for (j = 0; j < pracStringLen; j++)
                    mll.addToEnd(pracString[j].ToString());
                mll.addToEnd("]");

                printmml();
                while (check()) ;
            }
            */
            string pracString;
            int pracStringLen;
            long outVal;
            long outValMax = 0;
            for (i = 0; i < inputColLen; i++)
            {
                for (j = 0; j < inputColLen; j++)
                {
                    if (i == j)
                        continue;
                    pracString = inputCol[i];
                    pracStringLen = pracString.Length;

                    mll = new MyLinkedList(pracString[0].ToString());
                    for (k = 1; k < pracStringLen; k++)
                        mll.addToEnd(pracString[k].ToString());


                    mll.addToBegin("[");
                    mll.addToEnd(",");
                    pracString = inputCol[j];
                    pracStringLen = pracString.Length;

                    for (k = 0; k < pracStringLen; k++)
                        mll.addToEnd(pracString[k].ToString());
                    mll.addToEnd("]");

                    while (check()) ;
                    globalNd = mll.begin;
                    outVal = computeVal();

                    if (outVal > outValMax)
                    {
                        outValMax = outVal;
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("outVal = {0}", outValMax);
        }

        private static Node globalNd;
        private static long computeVal()
        {
            globalNd = globalNd.next;
            long firstVal=0;
            long secondVal=0;
            int index = 1;
            string strPrac;
            while (true)
            {
                strPrac = globalNd.item;
                if (strPrac == ",")
                {
                    index = 2;
                    globalNd = globalNd.next;
                    continue;
                }
                if (index == 1)
                {
                    if (strPrac == "[")
                    {
                        firstVal = computeVal();
                    }
                    else
                    {
                        firstVal = Convert.ToInt64(globalNd.item);
                    }
                }
                else
                {
                    if (strPrac == "[")
                    {
                        secondVal = computeVal();
                    }
                    else if (strPrac == "]")
                    {
                        return 3 * firstVal + 2 * secondVal;
                    }
                    else
                    {
                        secondVal = Convert.ToInt64(globalNd.item);
                    }
                }
                globalNd = globalNd.next;
            }

        }

        private static bool check()
        {
            Node iter = mll.begin;
            int depth = 0;
            int thisVal = 0;

            while (iter != mll.end)
            {
                if (iter.item == ("["))
                    depth++;
                if (iter.item == ("]"))
                    depth--;
                if (depth > 4 && iter.item == ",")
                {
                    if (int.TryParse(iter.prev.item, out _) && int.TryParse(iter.prev.item, out _))
                    {
                        explode(iter);
                        return true;
                    }
                }
                iter = iter.next;
            }
            iter = mll.begin;
            while (iter != mll.end)
            {                
                if (int.TryParse(iter.item, out thisVal))
                {
                    if (thisVal >= 10)
                    {
                        split(iter);
                        return true;
                    }
                }

                iter = iter.next;
            }

            return false;
        }

        private static bool checkOld()
        {
            Boolean noReduction = true;
            Node iter = mll.begin;
            int depth = 0;
            int thisVal = 0;

            while (iter != mll.end)
            {
                if (iter.item == ("["))
                    depth++;
                if (iter.item == ("]"))
                    depth--;
                if (depth > 4 && iter.item == ",")
                {
                    if (int.TryParse(iter.prev.item, out _) && int.TryParse(iter.prev.item, out _))
                    {
                        explode(iter);
                        return true;
                        /*noReduction = false;
                        break;*/
                    }
                }
                if (int.TryParse(iter.item, out thisVal))
                {
                    if (thisVal >= 10)
                    {
                        split(iter);
                        return true;
                        /*noReduction = false;
                        break;*/
                    }
                }

                iter = iter.next;
            }

            return false;
        }
        private static void printmml()
        {
            Node iter = mll.begin;
            while (iter != mll.end)
            {
                Console.Write("{0}", iter.item);
                iter = iter.next;
            }
            Console.Write("{0}", iter.item);
            Console.WriteLine("");
        }

        private static void split(Node iter)
        {
            int leftVal = Convert.ToInt32(iter.item) / 2;
            int rightVal = Convert.ToInt32(iter.item) / 2 + Convert.ToInt32(iter.item) % 2;

            Node nd0 = iter.prev;
            Node nd1 = new Node(null, null, "[");
            Node nd2 = new Node(null, null, leftVal+"");
            Node nd3 = new Node(null, null, ",");
            Node nd4 = new Node(null, null, rightVal+"");
            Node nd5 = new Node(null, null, "]");
            Node nd6 = iter.next;

            nd0.next = nd1;
            nd1.next = nd2;
            nd2.next = nd3;
            nd3.next = nd4;
            nd4.next = nd5;
            nd5.next = nd6;

            nd6.prev = nd5;
            nd5.prev = nd4;
            nd4.prev = nd3;
            nd3.prev = nd2;
            nd2.prev = nd1;
            nd1.prev = nd0;
        }


        private static void explode(Node iter)
        {
            Node iterLeft = iter;
            Node iterRight = iter;
            int valLeft;
            int valRight;
            iterLeft = iterLeft.prev;
            iterRight = iterRight.next;
            valLeft = Convert.ToInt32(iterLeft.item, 10);
            valRight = Convert.ToInt32(iterRight.item, 10);
            int thisVal;
            iterLeft = iterLeft.prev;
            iterRight = iterRight.next;
            while (iterLeft != mll.begin)
            {
                if (int.TryParse(iterLeft.item, out thisVal))
                {
                    iterLeft.item = (thisVal + valLeft) + "";
                    break;
                }
                iterLeft = iterLeft.prev;
            }
            while (iterRight != mll.end)
            {
                if (int.TryParse(iterRight.item, out thisVal))
                {
                    iterRight.item = (thisVal + valRight) + "";
                    break;
                }
                iterRight = iterRight.next;
            }

            Node newNode0 = new Node(iterLeft, iterRight, "0");

            iterLeft = iter.prev.prev.prev;
            iterRight = iter.next.next.next;
            iterLeft.next = newNode0;
            newNode0.prev = iterLeft;
            iterRight.prev= newNode0;
            newNode0.next = iterRight;
        }
    }
}
