using System.Diagnostics;

namespace Day18cs
{
	internal class Program
	{
		static string inp;
		static void Main(string[] args)
		{
			List<string> inputCol = new List<string>();
			inp = Console.ReadLine();
			using (StreamReader file = new StreamReader("..\\..\\..\\" + inp + ".txt"))
			{
				string? ln;

				while ((ln = file.ReadLine()) != null)
				{
					inputCol.Add(ln);
				}
			}
			Stopwatch sw = new Stopwatch();
			sw.Start();
			string result1 = GetResult1(inputCol);
			sw.Stop();
			Console.WriteLine("Result 1:"); ;
			Console.WriteLine(result1);
			Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");

			Console.WriteLine("");
			Console.WriteLine("");

			sw.Reset();
			sw.Start();
			string result2 = GetResult2(inputCol);
			sw.Stop();
			Console.WriteLine("Result 2:"); ;
			Console.WriteLine(result2);
			Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
		}
		private static string GetResult1(List<string> inputCol)
		{
			BFGrid computerSimulator;
			int dimension;
			int steps;
			if (inp == "t")
			{
				dimension = 7;
				steps = 12;
			}
			else
			{
				dimension = 71;
				steps = 1024;
			}
			computerSimulator = new BFGrid(inputCol, dimension);
			(int, int) coord = (0, 0);
			return computerSimulator.RunSimulation(steps, ref coord).ToString();
		}

		private static string GetResult2(List<string> inputCol)
		{
			BFGrid computerSimulator;
			int dimension;
			if (inp == "t")
			{
				dimension = 7;
			}
			else
			{
				dimension = 71;
			}
			computerSimulator = new BFGrid(inputCol, dimension);
			(int, int) coord = (0, 0);
			int min = 1;
			int max = inputCol.Count;
			while (min < max)
			{
				int mid = (min + max) / 2;
				computerSimulator = new BFGrid(inputCol, dimension);
				if (computerSimulator.RunSimulation(mid, ref coord) == -1)
				{
					max = mid;
				}
				else
				{
					min = mid + 1;
				}
			}
			return coord.Item1 + "," + coord.Item2;
		}
	}
}
