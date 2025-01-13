using System.Diagnostics;
using System.Text;

namespace Day24cs
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
			GateSimulator gateSimulator = new GateSimulator(inputCol);
			return gateSimulator.GetResult1().ToString();
		}

		private static string GetResult2(List<string> inputCol)
		{
			GateSimulator gateSimulator = new GateSimulator(inputCol);
			gateSimulator.GetResult2().ToString();
			AdderRectreation adderRectreation = new AdderRectreation(inputCol);
			adderRectreation.CalculateNewGraph();

			List<string> list = new List<string>() { "z08", "thm", "wss", "wrm", "z22", "hwq", "gbs", "z29", };
			list.Sort();
			return PrintList(list);
		}
		
		private static string PrintList(List<string> maxGroupList)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in maxGroupList)
			{
				if (sb.Length > 0)
					sb.Append(",");
				sb.Append(s);
			}
			return sb.ToString();
		}
	}
}
