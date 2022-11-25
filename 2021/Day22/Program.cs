using System;
using System.Collections.Generic;

namespace Day22
{
    public class Cube
    {

        public long xMin;
        public long xMax;
        public long yMin;
        public long yMax;
        public long zMin;
        public long zMax;
        static long minlong(long a, long b)
        {
            if (a < b)
                return a;
            else
                return b;
        }
        static long maxlong(long a, long b)
        {
            if (a > b)
                return a;
            else
                return b;
        }
        public static Cube intersection(Cube master, Cube slave)
        {
            if (master.xMax < slave.xMin || slave.xMax < master.xMin || master.yMax < slave.yMin || slave.yMax < master.yMin || master.zMax < slave.zMin || slave.zMax < master.zMin)
                return null;
            Cube cube = new Cube();
            cube.xMin = maxlong(master.xMin, slave.xMin);
            cube.xMax = minlong(master.xMax, slave.xMax);
            cube.yMin = maxlong(master.yMin, slave.yMin);
            cube.yMax = minlong(master.yMax, slave.yMax);
            cube.zMin = maxlong(master.zMin, slave.zMin);
            cube.zMax = minlong(master.zMax, slave.zMax);

            return cube;
        }
        public long Volume()
        {
            return (1 + this.xMax - this.xMin) * (1 + this.yMax - this.yMin) * (1 + this.zMax - this.zMin);

        }
        public Cube(long _xMin, long _xMax, long _yMin, long _yMax, long _zMin, long _zMax)
        {
            xMin = _xMin;
            xMax = _xMax;
            yMin = _yMin;
            yMax = _yMax;
            zMin = _zMin;
            zMax = _zMax;
        }
        public Cube() { }
    }
    public class RealCube
    {
        public Cube master;
        public List<RealCube> slaves;

        public long Volume()
        {
            long realVolume = master.Volume();
            foreach (RealCube rc in slaves)
            {
                realVolume -= rc.Volume();
            }

            return realVolume;
        }
        public void on(Cube cubeIn)
        {
            foreach(RealCube rlc in slaves)
            {
                Cube intersected = Cube.intersection(rlc.master, cubeIn);
                if(intersected != null)
                {
                    rlc.on(intersected);
                }
            }
            slaves.Add(new RealCube(cubeIn));            
        }

        public void off(Cube cubeIn)
        {
            foreach (RealCube rlc in slaves)
            {
                Cube intersected = Cube.intersection(rlc.master, cubeIn);
                if (intersected != null)
                {
                    rlc.on(intersected);
                }
            }
        }

        public RealCube(Cube _master)
        {
            master = _master;
            slaves = new List<RealCube>();
        }
    }

    public class Operation
    {
        public Cube cube;
        public string opera;

    }


    class Program
    {
       
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

            long lowBOund=-200000;
            long maxBound=200000;

            Cube mainCube = new Cube(lowBOund, maxBound, lowBOund, maxBound, lowBOund, maxBound);
            RealCube realcube = new RealCube(mainCube);
            string operation;
            long xmin, xmax, ymin, ymax, zmin, zmax;
            string[] strArr;
            string[] strArr1;
            foreach (String str in inputCol)
            {
                strArr = str.Split(" ");
                operation = strArr[0];
                strArr = strArr[1].Split(",");

                strArr1 = strArr[0].Split("=");
                strArr1 = strArr1[1].Split("..");
                xmin = long.Parse(strArr1[0]);
                xmax = long.Parse(strArr1[1]);

                strArr1 = strArr[1].Split("=");
                strArr1 = strArr1[1].Split("..");
                ymin = long.Parse(strArr1[0]);
                ymax = long.Parse(strArr1[1]);

                strArr1 = strArr[2].Split("=");
                strArr1 = strArr1[1].Split("..");
                zmin = long.Parse(strArr1[0]);
                zmax = long.Parse(strArr1[1]);

               
                if (xmin >= lowBOund && xmax <= maxBound && ymin >= lowBOund && ymax <= maxBound && zmin >= lowBOund && zmax <= maxBound)
                {
                    if (operation == "on")
                        realcube.on(new Cube(xmin, xmax, ymin, ymax, zmin, zmax));
                    else if (operation == "off")
                        realcube.off(new Cube(xmin, xmax, ymin, ymax, zmin, zmax));
                    else
                        throw new Exception();
                }else
                {
                    throw new Exception();
                }


            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Vol = {0}", realcube.master.Volume()-realcube.Volume());

        }
    }
}
