using System.Diagnostics;

namespace Day21cs
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
			sw.Reset();
			sw.Start();
			string result3 = GetResult3(inputCol);
			sw.Stop();
			Console.WriteLine("Result 3:"); ;
			Console.WriteLine(result3);
			Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
		}
		private static string GetResult1(List<string> inputCol)
		{
			DeterSimulator panelSimulator = new DeterSimulator();
			long simulVal;
			long result = 0;
			foreach (string s in inputCol)
			{
				simulVal = panelSimulator.Simulate(s, 2);
				//Console.WriteLine(simulVal);
				//Console.WriteLine();
				result += simulVal * long.Parse(s.Replace("A", ""));
			}
			return result.ToString();
		}
		private static string GetResult2(List<string> inputCol)
		{
			DeterSimulator panelSimulator = new DeterSimulator();
			long simulVal;
			long result = 0;
			foreach (string s in inputCol)
			{
				simulVal = panelSimulator.Simulate(s, 25);
				result += simulVal * long.Parse(s.Replace("A", ""));
			}

			//IEnumerable<long> keys = panelSimulator.GetMEMO().Keys.Select(x=>x.Item1);
			//Console.WriteLine(keys.Max());
			//Console.WriteLine(long.MaxValue);

			return result.ToString();
		}
		private static string GetResult3(List<string> inputCol)
		{
			DeterSimulator panelSimulator = new DeterSimulator();
			long simulVal;
			long result = 0;

			Console.WriteLine(long.MaxValue);
			foreach (string s in inputCol)
			{
				simulVal = panelSimulator.Simulate(s, 1000);
				Console.WriteLine(simulVal);
			}

			//IEnumerable<long> keys = panelSimulator.GetMEMO().Keys.Select(x=>x.Item1);
			//Console.WriteLine(keys.Max());
			//Console.WriteLine(long.MaxValue);

			return result.ToString();
		}
	}
}
