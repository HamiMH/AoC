using System.Diagnostics;

namespace Day05cs
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

		private static DirectionalGraph LoadGraph(List<string> inputCol)
		{
			int n = inputCol.Count;
			int i = 0;
			string str;
			DirectionalGraph graph = new DirectionalGraph();

			
			for (i = 0; i < n; i++)
			{
				str = inputCol[i];
				if (str == null || str == "")
					break;
				graph.AddEdge(str);
			}
			i++;
			for (; i < n; i++)
			{
				str = inputCol[i];
				graph.AddRule(str);
			}
			return graph;
		}
		private static string GetResult1(List<string> inputCol)
		{
			DirectionalGraph graph = LoadGraph(inputCol);
			return graph.ValidateRulesNoSwap().ToString();
		}

		private static string GetResult2(List<string> inputCol)
		{
			DirectionalGraph graph = LoadGraph(inputCol);
			return graph.ValidateRulesWithSwap().ToString();
		}
	}
}
