using System;

namespace Day02
{
    class Program
    {
        static void StepOne()
        {
            string line;
            string[] lineArr = new string[2];
            string direct;
            int len;
            int x = 0;
            int y = 0;
            while ((line = Console.ReadLine()) != null)
            {
                if (line == "")
                    break;
                lineArr = line.Split(" ");
                direct = lineArr[0];
                int.TryParse(lineArr[1], out len);
                if (direct == "forward")
                {
                    x += len;
                }
                else if (direct == "down")
                {
                    y += len;
                }
                else if (direct == "up")
                {
                    y -= len;
                }
            }
            Console.WriteLine("Multiply of x = {0} and y = {1}: {2}", x, y, x * y);
        }
        static void StepTwo()
        {
            string line;
            string[] lineArr = new string[2];
            string direct;
            long len;
            long x = 0;
            long y = 0;
            long aim = 0;
            while ((line = Console.ReadLine()) != null)
            {
                if (line == "")
                    break;
                lineArr = line.Split(" ");
                direct = lineArr[0];
                long.TryParse(lineArr[1], out len);
                if (direct == "forward")
                {
                    x += len;
                    y += aim * len;
                }
                else if (direct == "down")
                {
                    aim += len;
                }
                else if (direct == "up")
                {
                    aim -= len;
                }
                //Console.WriteLine("x = {0}, y = {1}, aim = {2}.", x, y, aim);
            }
            Console.WriteLine("Multiply of x = {0} and y = {1}: {2}", x, y, x * y);
        }
        static void Main(string[] args)
        {
            StepTwo();
        }
    }
}
