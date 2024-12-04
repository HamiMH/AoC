using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day04cs
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<string> inputCol = new List<string>();
			string inp = Console.ReadLine();
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
			string strTemplate = "XMAS";
			List<Tuple<int, int>> gridTemplate = new List<Tuple<int, int>>()
			{
				new Tuple<int, int>(0, 0),
				new Tuple<int, int>(1, 0),
				new Tuple<int, int>(2, 0),
				new Tuple<int, int>(3, 0)
			};
			XmasSolver solver = new XmasSolver(inputCol, gridTemplate, strTemplate, true, true);
			return solver.GetAmount().ToString();
		}

		private static string GetResult2(List<string> inputCol)
		{
			string strTemplate = "MSAMS";
			List<Tuple<int, int>> gridTemplate = new List<Tuple<int, int>>()
			{
				new Tuple<int, int>(0, 0),
				new Tuple<int, int>(2, 0),
				new Tuple<int, int>(1, 1),
				new Tuple<int, int>(0, 2),
				new Tuple<int, int>(2, 2)
			};
			XmasSolver solver = new XmasSolver(inputCol, gridTemplate, strTemplate,false,true);
			return solver.GetAmount().ToString();
		}
	}
}
