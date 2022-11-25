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


        //public property 

        public override string ToString()
        {
            return "(" + x + "," + y + "," + z + ")";
        }

        public override int GetHashCode()
        {
            return 128*128*z + 128 * y + x; // Or something like that
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


        static List<List<List<Point>>> allBeaconsAndRot = new List<List<List<Point>>>();
        static List<Point> localShift = new List<Point>();
        static List<bool> isCompleted = new List<bool>();
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
            initRoMatrix();

            List<List<Point>> thisBeaconAllRot = new List<List<Point>>();
            List<Point> thisBeaconOneRot = new List<Point>();
            foreach (string str in inputCol)
            {
                if (!str.Contains(","))
                {
                    allBeaconsAndRot.Add(new List<List<Point>>());
                    localShift.Add(new Point(0, 0, 0));
                    isCompleted.Add(false);
                    thisBeaconAllRot = allBeaconsAndRot[allBeaconsAndRot.Count - 1];

                    for(i=0;i<numOfRot;i++)
                        thisBeaconAllRot.Add(new List<Point>());
                    
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
            isCompleted[0] = true;


            Console.WriteLine("Hello World!");
        }

        private static void generateAllRotations()
        {
            int i;
            foreach(List<List<Point>> jedenScanner in allBeaconsAndRot)
            {
                foreach(Point beacon in jedenScanner[0])
                {
                    for(i = 1; i < numOfRot; i++)
                    {
                        jedenScanner[i].Add(beacon.rotByNthMat(i));
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
