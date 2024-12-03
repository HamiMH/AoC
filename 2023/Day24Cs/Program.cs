using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Day24Cs
{
    internal class Program
    {
        static decimal MinP = 200000000000000.0M;
        static decimal MaxP = 400000000000000.0M;

        //static double MinP = 7.0;
        //static double MaxP = 27.0;

        static List<Line> Lines = new List<Line>();
        static long FindIntersect2D()
        {
            long sum = 0;
            int size = Lines.Count;
            for (int i = 0; i < size; i++)
                for (int j = i + 1; j < size; j++)
                    if (Lines[i].IntersectLine2D(Lines[j], MinP, MaxP))
                        sum++;
            return sum;
        }

        static void ResolveInput(List<string> vec)
        {
            foreach (string s in vec)
            {
                Lines.Add(new Line(s));
            }
        }
        static string MySolution1(List<string> vec)
        {
            long sum = 0;

            ResolveInput(vec);        

            sum = FindIntersect2D();
            return sum.ToString();

        }

        static string MySolution2(List<string> vec)
        {
            long sum = 0;

            int itermax = 4;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Res =Solve[");
            for (int iter = 0; iter < itermax; iter++)
            {
                if (iter != 0)
                    Console.WriteLine(" && ");

                Console.WriteLine(Lines[iter].X.Item1 + "+t" + iter + " * " + Lines[iter].Vec.Item1 + " == " + "Px + " + "Sx * t" + iter + " && ");
                Console.WriteLine(Lines[iter].X.Item2 + "+t" + iter + " * " + Lines[iter].Vec.Item2 + " == " + "Py + " + "Sy * t" + iter + " && ");
                Console.WriteLine(Lines[iter].X.Item3 + "+t" + iter + " * " + Lines[iter].Vec.Item3 + " == " + "Pz + " + "Sz * t" + iter + " ");
            }
            Console.WriteLine(",");
            Console.Write("{Px,Py,Pz,Sx,Sy,Sz");
            for (int iter = 0; iter < itermax; iter++)
            {
                Console.Write(",t" + iter);
            }
            Console.Write("}];"); 
            Console.WriteLine("");
            Console.Write("Res[[1, 1, 2]]+Res[[1, 2, 2]]+Res[[1, 3, 2]]");
            Console.WriteLine("");
            Console.WriteLine("");


            return sum.ToString();

        }

        static void Main(string[] args)
        {

            string inp = Console.ReadLine();
            List<string> vec = new List<string>();

            using (StreamReader file = new StreamReader("..\\..\\..\\" + inp + ".txt"))
            {
                int counter = 0;
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    vec.Add(ln);
                }            
            }



            string result = MySolution1(vec);
            Console.WriteLine("Difs: " + Line.diffs);
            Console.WriteLine("Result1: " + result);
            result = MySolution2(vec);
            Console.WriteLine("Result2: " + result);
        }

    }
}
