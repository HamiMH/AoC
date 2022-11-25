using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Day19
{
    public class Point : IComparable<Point>, IEquatable<Point>
    {
        public int x;
        public int y;
        public int z;


        public static Point add(Point p1, Point p2)
        {
            return new Point(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
        }
        public static Point sub(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
        }
        public static Point operator +(Point p) => p;
        public static Point operator -(Point p) => new Point(-p.x,-p.y,-p.z);

        public static Point operator +(Point p1, Point p2)
            => new  Point(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);

        public static Point operator -(Point p1, Point p2)
            => new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);

        public override string ToString()
        {
            return "(" + x + "," + y + "," + z + ")";
        }

        private static int abs(int a)
        {
            if (a >= 0)
                return a;
            else
                return -a;
        }

        public int Norm1()
        {
            return abs(this.x) + abs(this.y) + abs(this.z);
        }
       
        public int CompareTo(Point p1)
        {

            if (p1.x > this.x)
                return 1;
            if (p1.x < this.x)
                return -1;

            if (p1.y > this.y)
                return 1;
            if (p1.y < this.y)
                return -1;

            if (p1.z > this.z)
                return 1;
            if (p1.z < this.z)
                return -1;

            return 0;
        }

        public Point rotByNthMat(int index)
        {
            int i, j;
            int[] arrIn = { this.x, this.y, this.z };
            int[] arrOut = { 0, 0, 0 };
         
            for (i = 0; i < 3; i++)
                for (j = 0; j < 3; j++)
                    arrOut[i] += Program.rotMatrix[index][i, j] * arrIn[j];



            return new Point(arrOut[0], arrOut[1], arrOut[2]);
        }

        public bool Equals(Point p1)
        {
            if (this.CompareTo(p1) == 0)
                return true;
            else
                return false;
        }

        public Point(int _x, int _y, int _z)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
        }
    }
    class Program
    {


        public static List<int[,]> rotMatrix = new List<int[,]>();
        //static int[][,] rotMatrix = new int[24][,];
        /* {
             {{-1, 0, 0}, {0, -1, 0}, {0, 0, 1}},
             {{-1, 0, 0}, {0, 0, -1}, {0, -1, 0}},
             {{-1, 0, 0}, {0, 0, 1}, {0, 1, 0}},
             {{-1, 0, 0}, {0, 1, 0}, {0, 0, -1}},
             {{0, -1, 0}, {-1, 0, 0}, {0, 0, -1}},
             {{0, -1, 0}, {0, 0, -1}, {1, 0, 0}},
             {{0, -1, 0}, {0, 0, 1}, {-1, 0, 0}},
             {{0, -1, 0}, {1, 0, 0}, {0, 0, 1}},
             {{0, 0, -1}, {-1, 0, 0}, {0, 1, 0}},
             {{0, 0, -1}, {0, -1, 0}, {-1, 0, 0}},
             {{0, 0, -1}, {0, 1, 0}, {1, 0, 0}},
             {{0, 0, -1}, {1, 0, 0}, {0, -1, 0}},
             {{0, 0, 1}, {-1, 0, 0}, {0, -1, 0}},
             {{0, 0, 1}, {0, -1, 0}, {1, 0, 0}},
             {{0, 0, 1}, {0, 1, 0}, {-1, 0, 0}},
             {{0, 0, 1}, {1,  0, 0}, {0, 1, 0}},
             {{0, 1, 0}, {-1, 0, 0}, {0, 0, 1}},
             {{0, 1, 0}, {0, 0, -1}, {-1, 0, 0}},
             {{0, 1, 0}, {0, 0, 1}, {1, 0, 0}},
             {{0, 1, 0}, {1, 0, 0}, {0, 0, -1}},
             {{1, 0, 0}, {0, -1, 0}, {0, 0, -1}},
             {{1, 0, 0}, {0, 0, -1}, {0, 1, 0}}, 
             {{1, 0, 0}, {0, 0, 1}, {0, -1, 0}},
             {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}}
         };*/
        static void initRoMatrix()
        {
             int[,] pracMat23 = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            rotMatrix.Add(pracMat23);
            int[,] pracMat0 = { { -1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } };
            rotMatrix.Add(pracMat0);
            int[,] pracMat1 = { { -1, 0, 0 }, { 0, 0, -1 }, { 0, -1, 0 } };
            rotMatrix.Add(pracMat1);
            int[,] pracMat2 = { { -1, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } };
            rotMatrix.Add(pracMat2);
            int[,] pracMat3 = { { -1, 0, 0 }, { 0, 1, 0 }, { 0, 0, -1 } };
            rotMatrix.Add(pracMat3);
            int[,] pracMat4 = { { 0, -1, 0 }, { -1, 0, 0 }, { 0, 0, -1 } };
            rotMatrix.Add(pracMat4);
            int[,] pracMat5 = { { 0, -1, 0 }, { 0, 0, -1 }, { 1, 0, 0 } };
            rotMatrix.Add(pracMat5);
            int[,] pracMat6 = { { 0, -1, 0 }, { 0, 0, 1 }, { -1, 0, 0 } };
            rotMatrix.Add(pracMat6);
            int[,] pracMat7 = { { 0, -1, 0 }, { 1, 0, 0 }, { 0, 0, 1 } };
            rotMatrix.Add(pracMat7);
            int[,] pracMat8 = { { 0, 0, -1 }, { -1, 0, 0 }, { 0, 1, 0 } };
            rotMatrix.Add(pracMat8);
            int[,] pracMat9 = { { 0, 0, -1 }, { 0, -1, 0 }, { -1, 0, 0 } };
            rotMatrix.Add(pracMat9);
            int[,] pracMat10 = { { 0, 0, -1 }, { 0, 1, 0 }, { 1, 0, 0 } };
            rotMatrix.Add(pracMat10);
            int[,] pracMat11 = { { 0, 0, -1 }, { 1, 0, 0 }, { 0, -1, 0 } };
            rotMatrix.Add(pracMat11);
            int[,] pracMat12 = { { 0, 0, 1 }, { -1, 0, 0 }, { 0, -1, 0 } };
            rotMatrix.Add(pracMat12);
            int[,] pracMat13 = { { 0, 0, 1 }, { 0, -1, 0 }, { 1, 0, 0 } };
            rotMatrix.Add(pracMat13);
            int[,] pracMat14 = { { 0, 0, 1 }, { 0, 1, 0 }, { -1, 0, 0 } };
            rotMatrix.Add(pracMat14);
            int[,] pracMat15 = { { 0, 0, 1 }, { 1, 0, 0 }, { 0, 1, 0 } };
            rotMatrix.Add(pracMat15);
            int[,] pracMat16 = { { 0, 1, 0 }, { -1, 0, 0 }, { 0, 0, 1 } };
            rotMatrix.Add(pracMat16);
            int[,] pracMat17 = { { 0, 1, 0 }, { 0, 0, -1 }, { -1, 0, 0 } };
            rotMatrix.Add(pracMat17);
            int[,] pracMat18 = { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 0, 0 } };
            rotMatrix.Add(pracMat18);
            int[,] pracMat19 = { { 0, 1, 0 }, { 1, 0, 0 }, { 0, 0, -1 } };
            rotMatrix.Add(pracMat19);
            int[,] pracMat20 = { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, -1 } };
            rotMatrix.Add(pracMat20);
            int[,] pracMat21 = { { 1, 0, 0 }, { 0, 0, -1 }, { 0, 1, 0 } };
            rotMatrix.Add(pracMat21);
            int[,] pracMat22 = { { 1, 0, 0 }, { 0, 0, 1 }, { 0, -1, 0 } };
            rotMatrix.Add(pracMat22);
           
        }


        static List<List<SortedSet<Point>>> allBeaconsAndRot = new List<List<SortedSet<Point>>>();
        static List<Point> localShift = new List<Point>();
        static List<int> rightRotation = new List<int>();
        static List<bool> completedSearch = new List<bool>();
        static SortedSet<Point> foundBeacon = new SortedSet<Point>();
        static int numOfScan;
        static int numOfRot = 24;

        static void Main(string[] args)
        {
            int i;
            List<string> inputCol = new List<string>();
            string lineIn1;
            string []strArr;
            while ((lineIn1 = Console.ReadLine()) != null)
            {
                if (lineIn1 == "")
                    continue;
                if (lineIn1 == "eof")
                    break;
                inputCol.Add(lineIn1);
            }

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            initRoMatrix();

            List<SortedSet<Point>> thisBeaconAllRot = new List<SortedSet<Point>>();
            SortedSet<Point> thisBeaconOneRot = new SortedSet<Point>();
            foreach (string str in inputCol)
            {
                if (!str.Contains(","))
                {
                    allBeaconsAndRot.Add(new List<SortedSet<Point>>());
                    localShift.Add(new Point(0, 0, 0));
                    rightRotation.Add(-1);
                    completedSearch.Add(false);
                    thisBeaconAllRot = allBeaconsAndRot[allBeaconsAndRot.Count - 1];

                    for(i=0;i<numOfRot;i++)
                        thisBeaconAllRot.Add(new SortedSet<Point>());
                    
                    thisBeaconOneRot = thisBeaconAllRot[0];
                }
                else
                {
                    strArr = str.Split(",");
                    thisBeaconOneRot.Add( new Point( Int32.Parse(strArr[0]), Int32.Parse(strArr[1]), Int32.Parse(strArr[2]) )  );
                }
            }
            numOfScan = allBeaconsAndRot.Count;
          

            generateAllRotations();
            rightRotation[0] = 0;
            foreach(Point pt in allBeaconsAndRot[0][0])
                foundBeacon.Add(pt);

            stopwatch.Stop();
            Console.WriteLine("Preprocess: Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            stopwatch.Start();

            processInput();

            int farestScan=findFarestScan();
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Elapsed Time is {0} s", stopwatch.ElapsedMilliseconds/1000);
            Console.WriteLine("numb of beacon = {0}, farest scan = {1}", foundBeacon.Count, farestScan);
        }

        private static int findFarestScan()
        {
            int farest = 0;
            int norm;
            foreach (Point p1 in localShift)
                foreach (Point p2 in localShift)
                {
                    norm = (p1 - p2).Norm1();
                    if (norm > farest)
                        farest = norm;
                }
                    

            return farest;
        }

        private static void processInput()
        {
            int i;
            while (notCompleted())
            {
                for (i = 0; i < numOfScan; i++)
                {
                    if (rightRotation[i] != -1 && completedSearch[i]==false)
                    {
                        searchThisScanner(i);
                    }
                }
            }
        }

        private static void searchThisScanner(int scanIndex)
        {
            int i,j;
            bool found;
            for (i = 0; i < numOfScan; i++)
            {
                if (rightRotation[i] != -1)
                    continue;
                else
                {
                    for (j = 0; j < numOfRot; j++)
                    {
                        found = pairScannersAndRot(scanIndex,i,j);
                        if (found)
                        {
                            break;
                        }
                    }
                }
            }
            completedSearch[scanIndex] = true;
        }

        private static bool pairScannersAndRot(int scanFrom, int scanTo, int indOfRot)
        {
            int numOfEql;
            SortedSet<Point> ssFrom = allBeaconsAndRot[scanFrom][rightRotation[scanFrom]];
            SortedSet<Point> ssTo = allBeaconsAndRot[scanTo][indOfRot];
            int remainingssFrom = ssFrom.Count;
            int remainingssTo;
            int ssToCount = ssTo.Count;
            foreach (Point ptFrom in ssFrom) {
                if (remainingssFrom < 12)//perform. optim.
                    break;
                remainingssFrom--;
                foreach (Point ptTo in ssTo)
                {
                    Point shift = ptTo - ptFrom;
                    numOfEql = 0;
                    /* foreach (Point ptFromInner in allBeaconsAndRot[scanFrom][rightRotation[scanFrom]])
                         foreach (Point ptToInner in allBeaconsAndRot[scanTo][indOfRot])
                         {
                             if (ptFromInner.Equals(ptToInner - shift))
                                 numOfEql++;
                         }*/
                    remainingssTo = ssToCount;
                    foreach (Point ptToInner in ssTo)
                    {
                        if (remainingssTo < 12- numOfEql) //perform. optim.
                            break;
                        remainingssTo--;
                        if (ssFrom.Contains(ptToInner - shift))
                            numOfEql++;
                    }
                        

                    if (numOfEql >= 12)
                    {
                        rightRotation[scanTo] = indOfRot;
                        localShift[scanTo] = localShift[scanFrom] - shift;
                        foreach (Point ptToInner2 in ssTo)
                            foundBeacon.Add(ptToInner2 + localShift[scanTo]);
                        //upravit shift dle původního
                        return true;
                    }
                }
        }
            return false;
        }

        private static bool notCompleted()
        {
            foreach (int rotInd in rightRotation)
                if (rotInd == -1)
                    return true;
            return false;
        }

        private static void generateAllRotations()
        {
            int i;
            foreach(List<SortedSet<Point>> oneScanner in allBeaconsAndRot)
            {
                foreach(Point beacon in oneScanner[0])
                {
                    for(i = 1; i < numOfRot; i++)
                    {
                        oneScanner[i].Add(beacon.rotByNthMat(i));
                    }
                }
            }
        }
    }
    class Scanners
    {
        List<List<List<Point>>> scanners;
        public Scanners()
        {
            scanners = new List<List<List<Point>>>();
        }
    }
}
