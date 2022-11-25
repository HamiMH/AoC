//using System;
//using System.Numerics;
//using System.Text;

//namespace Day20
//{
//    public class Node : IComparable<Node>, IEquatable<Node>
//    {
//        public string str = "";
//        public int price;

//        public override string ToString()
//        {
//            return "(" + price + "," + str + ")";
//        }
//        public int CompareTo(Node p1)
//        {
//            if (p1 == null)
//            {
//                return 1;
//            }
//            /*if (p1.price < this.price)
//                return 1;
//            if (p1.price > this.price)
//                return -1;
//            */
//            int ret = this.price - p1.price;

//            /*if (p1.x > this.x)
//                return 1;
//            if (p1.x < this.x)
//                return -1;
//            */
//            if (ret == 0)
//                return (this.str).CompareTo(p1.str);
//            else
//                return ret;


//        }



//        public bool Equals(Node p1)
//        {
//            if (this.CompareTo(p1) == 0)
//                return true;
//            else
//                return false;
//        }

//        public Node(int _price, string _str)
//        {
//            this.price = _price;
//            this.str = _str;
//        }
//    }
//    class Program
//    {

//        public static int[,] FloydWarshall(int[,] graph)
//        {
//            int verticesCount = lenOfConfig;
//            int[,] distance = new int[verticesCount, verticesCount];

//            for (int i = 0; i < verticesCount; ++i)
//                for (int j = 0; j < verticesCount; ++j)
//                {
//                    if (graph[i, j] == 0)
//                        distance[i, j] = 1000;
//                    else
//                        distance[i, j] = graph[i, j];
//                }


//            for (int k = 0; k < verticesCount; ++k)
//            {
//                for (int i = 0; i < verticesCount; ++i)
//                {
//                    for (int j = 0; j < verticesCount; ++j)
//                    {
//                        if (distance[i, k] + distance[k, j] < distance[i, j])
//                            distance[i, j] = distance[i, k] + distance[k, j];
//                    }
//                }
//            }
//            return distance;

//        }
//        static int[,] fwMat;

//        static int[,] adjMat ={
//            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
//            {1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//  0, 0}, {0, 2, 0, 1, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//  0, 0, 0}, {0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//  0, 0, 0, 0}, {0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//  0, 0, 0, 0, 0}, {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//  0, 0, 0, 0, 0, 0}, {0, 2, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0,
//  0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 2, 0, 1, 0, 0, 2, 0, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0,
//  0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//  0, 2, 0, 1, 0, 0, 2, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0,
//  0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0,
//  0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0,
//  0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0}, {0, 0, 0, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1, 0, 0, 2, 0}, {0, 0, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0}, {0, 0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0}, {0, 0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, {0,
//  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0,
//  1}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//  1, 0}};

//        static string finalConfig = "..AAAA.BBBB.CCCC.DDDD..";
//        static string emptyConfig = "..aaaa.bbbb.cccc.dddd..";
//        static int lenOfConfig;
//        static void v01(string line1, string line2)
//        { }

//        static HashSet<string> inCloud = new HashSet<string>();
//        static Dictionary<string, Node> memo = new Dictionary<string, Node>();
//        static SortedSet<Node> prioritQ = new SortedSet<Node>();

//        static int heuristic(string strIn0)
//        {
//            StringBuilder strIn = new StringBuilder(strIn0);
//            StringBuilder pracString = new StringBuilder(emptyConfig);
//            char charI;
//            int outpuVal = 0;

//            Boolean aIsHome = isHome('A', 2, strIn.ToString());
//            Boolean bIsHome = isHome('B', 7, strIn.ToString());
//            Boolean cIsHome = isHome('C', 12, strIn.ToString());
//            Boolean dIsHome = isHome('D', 17, strIn.ToString());

//            if (aIsHome)
//                for (int i = 2; i < 6; i++)
//                {
//                    if (strIn[i] == 'A')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'A';
//                    }
//                }
//            else
//                for (int i = 2; i < 6; i++)
//                {
//                    if (strIn[i] == 'A')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'A';
//                        outpuVal += 2 * i;
//                    }
//                }
//            if (bIsHome)
//                for (int i = 7; i < 11; i++)
//                {
//                    if (strIn[i] == 'B')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'B';
//                    }
//                }
//            else
//                for (int i = 7; i < 11; i++)
//                {
//                    if (strIn[i] == 'B')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'B';
//                        outpuVal += (2 * (i - 5) * 10);
//                    }
//                }
//            if (cIsHome)
//                for (int i = 12; i < 16; i++)
//                {
//                    if (strIn[i] == 'C')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'C';
//                    }
//                }
//            else
//                for (int i = 12; i < 16; i++)
//                {
//                    if (strIn[i] == 'C')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'C';
//                        outpuVal += (2 * (i - 10) * 100);
//                    }
//                }
//            if (dIsHome)
//                for (int i = 17; i < 21; i++)
//                {
//                    if (strIn[i] == 'D')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'D';
//                    }
//                }
//            else
//                for (int i = 17; i < 21; i++)
//                {
//                    if (strIn[i] == 'D')
//                    {
//                        strIn[i] = '.';
//                        pracString[i] = 'D';
//                        outpuVal += (2 * (i - 15) * 1000);
//                    }
//                }

//            char targetChar;
//            Boolean notCompleted = true;
//            while (notCompleted)
//            {

//                for (int i = 0; i < lenOfConfig; i++)
//                {
//                    notCompleted = true;
//                    charI = strIn[i];
//                    if (charI == '.')
//                    {
//                        notCompleted = false;
//                        continue;
//                    }

//                    /* if (charI == 'A' && i >= 2 && i <= 5)
//                     {
//                         outpuVal += costHeurist(i, 1, charI);
//                         break;
//                     }
//                     if (charI == 'B' && i >= 7 && i <= 10)
//                     {
//                         outpuVal += costHeurist(i, 6, charI);
//                         break;
//                     }
//                     if (charI == 'C' && i >= 12 && i <= 5)
//                     {
//                         outpuVal += costHeurist(i, 11, charI);
//                         break;
//                     }
//                     if (charI == 'D' && i >= 17 && i <= 20)
//                     {
//                         outpuVal += costHeurist(i, 16, charI);
//                         break;
//                     }*/
//                    targetChar = charI.ToString().ToLower()[0];
//                    for (int j = 0; j < lenOfConfig; j++)
//                    {
//                        if (pracString[j] == targetChar)
//                        {
//                            pracString[j] = charI;
//                            strIn[j] = '.';
//                            outpuVal += costHeurist(i, j, charI);
//                            break;
//                        }
//                    }
//                    notCompleted = false;
//                }
//            }


//            return outpuVal;
//        }
//        static int costHeurist(int i, int j, char charP)
//        {
//            int val = fwMat[i, j];
//            if (charP == 'A')
//            {
//                return val;
//            }
//            else if (charP == 'B')
//            {
//                return 10 * val;
//            }
//            else if (charP == 'C')
//            {
//                return 100 * val;
//            }
//            else if (charP == 'D')
//            {
//                return 1000 * val;
//            }

//            throw new Exception();
//        }

//        static int v02()
//        {
//            /*inCloud = new HashSet<string>();
//            memo = new Dictionary<string, Node>();
//            prioritQ= new SortedSet<Node>();*/
//            //test


//            string startConfig = "..BDDA.CCBD.BBAC.DACA..";

//            //myinput
//            //string startConfig = "..BDDC.DCBD.CBAB.AACA..";
//            Console.WriteLine(startConfig);
//            //startConfig = swap(0, 2, startConfig);
//            //Console.WriteLine(startConfig);
//            lenOfConfig = startConfig.Length;
//            //int output = recursion(startConfig);
//            fwMat = FloydWarshall(adjMat);
//            int testHeur = heuristic("..AAAB.BBBA.CCCC.DDDD..");
//            int output = dijkstra(startConfig);


//            return output;
//        }



//        private static int dijkstra(string inStr)
//        {
//            Node nd = new(0, inStr);
//            prioritQ.Add(nd);
//            memo.Add(inStr, nd);

//            if (prioritQ == null)
//                return 0;
//            while (prioritQ.Count > 0)
//            {
//                nd = prioritQ.Min;
//                if (nd.str == finalConfig)
//                    return nd.price;
//                inCloud.Add(nd.str);
//                oneStep(nd.str, nd.price);
//                prioritQ.Remove(nd);
//            }


//            return 0;
//        }

//        private static void oneStep(string stringIn, int priceNow)
//        {
//            int i, j;
//            char charI, charJ;
//            string prac;
//            for (i = 0; i < lenOfConfig; i++)
//            {

//                charI = stringIn[i];
//                if (charI == '.')
//                    continue;
//                for (j = 0; j < 23; j++)
//                {
//                    if (adjMat[i, j] == 0)
//                        continue;

//                    prac = new string(stringIn);
//                    charJ = prac[j];
//                    if (charJ != '.')
//                        continue;

//                    if (isHomeIndex(i))
//                    {
//                        if (isHomeIndex(j))
//                        {
//                            if (isHome(charI, j, prac) && (j > i))
//                            {
//                                zpracujZmenu(i, j, prac, charI);
//                                continue;
//                            }
//                            if (!isHome(charI, j, prac) && (j < i))
//                            {
//                                zpracujZmenu(i, j, prac, charI);
//                                continue;
//                            }
//                            continue;
//                        }
//                        else
//                        {
//                            if (!isHome(charI, i, prac))
//                            {
//                                zpracujZmenu(i, j, prac, charI);
//                                continue;
//                            }
//                            continue;
//                        }
//                    }
//                    else
//                    {
//                        if (isHomeIndex(j))
//                        {
//                            if (isHome(charI, j, prac))
//                            {
//                                zpracujZmenu(i, j, prac, charI);
//                                continue;
//                            }
//                            continue;
//                        }
//                        else
//                        {
//                            zpracujZmenu(i, j, prac, charI);
//                            continue;
//                        }
//                    }



//                }
//            }
//        }

//        private static void zpracujZmenu(int i, int j, string prac, char charI)
//        {
//            Node oldNode = memo[prac];
//            int oldPrice = oldNode.price;

//            prac = swap(i, j, prac);
//            int costInto = oldNode.price + cost(i, j, charI);
//            Node newNode;
//            if (memo.ContainsKey(prac))
//            {
//                newNode = memo[prac];
//                if (newNode.price > costInto)
//                {
//                    prioritQ.Remove(newNode);
//                    newNode.price = costInto;
//                    prioritQ.Add(newNode);
//                }
//                else
//                {
//                    return;
//                }
//            }
//            else
//            {
//                newNode = new Node(costInto, prac);
//                memo.Add(prac, newNode);
//                prioritQ.Add(newNode);
//            }
//        }
//        private static int cost(int i, int j, char charP)
//        {
//            int val = adjMat[i, j];
//            /*if (i > j)
//                i = i - j;
//            else
//                i = j - i;
            
//            */
//            if (charP == 'A')
//            {
//                return val;
//            }
//            else if (charP == 'B')
//            {
//                return 10 * val;
//            }
//            else if (charP == 'C')
//            {
//                return 100 * val;
//            }
//            else if (charP == 'D')
//            {
//                return 1000 * val;
//            }

//            throw new Exception();
//        }

//        /*private static int[] range(char charP)
//        {
//            if (charP == 'A')
//            {
//                int[] ret = { 2, 5 };
//                return ret;
//            }
//            else if (charP == 'B')
//            {
//                int[] ret = { 7, 10 };
//                return ret;
//            }
//            else if (charP == 'C')
//            {
//                int[] ret = { 12, 15 };
//                return ret;
//            }
//            else if (charP == 'D')
//            {
//                int[] ret = { 17, 20 };
//                return ret;
//            }
//            return null;
//        }*/

//        private static string swap(int from, int into, string stringIn)
//        {
//            char tmpInto = stringIn[into];
//            char tmpFrom = stringIn[from];

//            StringBuilder sb = new StringBuilder(stringIn);
//            sb[into] = tmpFrom;
//            sb[from] = tmpInto;
//            return sb.ToString();
//        }
//        private static bool isHome(char charP, int index, string stringIn)
//        {
//            int startPos = 0;
//            if (charP == 'A')
//                startPos = 2;
//            else if (charP == 'B')
//                startPos = 7;
//            else if (charP == 'C')
//                startPos = 12;
//            else if (charP == 'D')
//                startPos = 17;

//            if (index < startPos || index >= startPos + 4)
//                return false;

//            for (int i = startPos; i < startPos + 4; i++)
//                if (!(stringIn[i] == charP || stringIn[i] == '.'))
//                    return false;
//            return true;
//        }

//        private static bool isHomeIndex(int index)
//        {
//            if ((index >= 2 && index <= 5) || (index >= 7 && index <= 10) || (index >= 12 && index <= 15) || (index >= 17 && index <= 20))
//                return true;
//            else
//                return false;
//        }

//        static void Main(string[] args)
//        {
//            //string line1 = Console.ReadLine();
//            //string line2 = Console.ReadLine();
//            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
//            stopwatch.Start();
//            //v01(line1, line2);
//            int result = v02();
//            Console.WriteLine("result = {0}", result);
//            stopwatch.Stop();
//            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
//        }
//    }
//}


///*
// * private static int recursion(string stringIn)
//        {
//            if (stringIn.Equals(finalConfig))
//                return 0;
//            if (memo.ContainsKey(stringIn))
//            {
//                return memo[stringIn];
//            }

//            if (hsUsed.Contains(stringIn))
//                return 1000000;

//            hsUsed.Add(stringIn);
//            //string stringConfig = 
//            int i, j;
//            char charI, charJ;
//            string prac;
//            int minPrice = 1000000;
//            int price;
//            for (i = 0; i < lenOfConfig; i++)
//            {

//                charI = stringIn[i];
//                if (charI == '.')
//                    continue;
                
//                for (j = 0; j < 23; j++)
//                {
//                    if (adjMat[i, j] == 0)
//                        continue;

//                    prac = new string(stringIn);
//                    charJ = prac[j];
//                    if (charJ != '.')
//                        continue;

//                    if (isHomeIndex(i))
//                    {
//                        if (isHomeIndex(j))
//                        {
//                            if (isHome(charI, j, prac) && (j > i))
//                            {
//                                prac = swap(i, j, prac);
//                                price = cost(i, j, charI) + recursion(prac);
//                                if (price < minPrice)
//                                    minPrice = price;
//                                continue;
//                            }
//                            if (!isHome(charI, j, prac) && (j < i))
//                            {
//                                prac = swap(i, j, prac);
//                                price = cost(i, j, charI) + recursion(prac);
//                                if (price < minPrice)
//                                    minPrice = price;
//                                continue;
//                            }
//                            continue;
//                        }
//                        else 
//                        {
//                            if (!isHome(charI, i, prac))
//                            {
//                                prac = swap(i, j, prac);
//                                price = cost(i, j, charI) + recursion(prac);
//                                if (price < minPrice)
//                                    minPrice = price;
//                                continue;
//                            }
//                            continue;
//                        }
//                    }
//                    else
//                    {
//                        if (isHomeIndex(j))
//                        {
//                            if (isHome(charI, j, prac))
//                            {
//                                prac = swap(i, j, prac);
//                                price = cost(i, j, charI) + recursion(prac);
//                                if (price < minPrice)
//                                    minPrice = price;
//                                continue;
//                            }
//                            continue;
//                        }
//                        else
//                        {
//                            prac = swap(i, j, prac);
//                            price = cost(i, j, charI) + recursion(prac);
//                            if (price < minPrice)
//                                minPrice = price;
//                            continue;
//                        }
//                    }

//                    if (isHomeIndex(j))
//                    {
//                        if (!isHome(charI, j, prac))
//                        {
//                            if (isHomeIndex(i) && (j < i))
//                            {
//                                prac = swap(i, j, prac);
//                                price = cost(i, j, charI) + recursion(prac);
//                                if (price < minPrice)
//                                    minPrice = price;
//                                continue;
//                            }
//                            else
//                                continue;
//                        }

//                        if (isHome(charI, i, prac))
//                        {
//                            if (j > i)
//                            {
//                                prac = swap(i, j, prac);
//                                price = cost(i, j, charI) + recursion(prac);
//                                if (price < minPrice)
//                                    minPrice = price;
//                            }
//                            else
//                                continue;
//                        }
//                        else
//                        {
//                            prac = swap(i, j, prac);
//                            price = cost(i, j, charI) + recursion(prac);
//                            if (price < minPrice)
//                                minPrice = price;
//                        }
//                    }
//                    else
//                    {
//                        prac = swap(i, j, prac);
//                        price = cost(i, j, charI) + recursion(prac);
//                        if (price < minPrice)
//                            minPrice = price;
//                    }

//                }
//            }

//            hsUsed.Remove(stringIn);
//            memo.Add(stringIn, minPrice);
//            return minPrice;
//        }*/