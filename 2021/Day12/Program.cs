using System;
using System.Collections.Generic;

namespace Day12
{

    public class Node
    {
        public string name;
        public Boolean bigCave;
        public int reamin;
        public List<Node> adjen = new List<Node>();

        public void addNode(Node nd)
        {
           /* foreach(Node adjNode in adjen)
            {
                if (adjNode.name == nd.name)
                    return;
            }*/
            this.adjen.Add(nd);
        }

        public  Node(string _name)
        {
            name = _name;
            if (name[0] <= 'Z')
            {
                bigCave = true;
                reamin = int.MaxValue;
            }
            else
            {
                bigCave = false;
                reamin = 1;
            }
        }

    }

    class Program
    {

        static int count = 0;
        static void Main(string[] args)
        {
            string lineIn ;
            string[] lineArr ;
            //int len = lineArr.Length;
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            while ((lineIn = Console.ReadLine()) != null)
            {
                if (lineIn == "")
                    break;

                lineArr = lineIn.Split("-");

                if (nodes.ContainsKey(lineArr[0]) == false)
                    nodes.Add(lineArr[0], new Node(lineArr[0]));
                if (nodes.ContainsKey(lineArr[1]) == false)
                    nodes.Add(lineArr[1], new Node(lineArr[1]));
                Node nd0= nodes[lineArr[0]];
                Node nd1= nodes[lineArr[1]];
               // nodes.TryGetValue(lineArr[0], out nd0);
                //nodes.TryGetValue(lineArr[1], out nd1);
                nd0.addNode(nd1);
                nd1.addNode(nd0);
            }

            projdiGraf(nodes["start"],1,"");

            Console.WriteLine("count: {0}", count);
        }

        private static void projdiGraf(Node node, int buffer, string path)
        {
            if (node.name == "end")
            {
               // Console.WriteLine("{0}", path);
                count++;
                return;
            }


            if (node.reamin <= 0)
                if (buffer == 1 && node.name!="start")
                    buffer = 0;
                else
                    return;

            node.reamin--;
            foreach(Node adjNode in node.adjen)
            {
                projdiGraf(adjNode, buffer,path+adjNode.name);
            }
            node.reamin++;
        }
    }
}
