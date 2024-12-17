using System.Diagnostics;
using System.Text;

namespace Day17cs
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
			ComputerSimulator cs = new ComputerSimulator(inputCol);		
			return PrintList(cs.Run());
		}

		private static string PrintList(List<int> list)
		{
			StringBuilder sb = new StringBuilder();
			foreach (int i in list)
			{
				if(sb.Length>0)
					sb.Append(",");
				sb.Append(i);
			}
			return sb.ToString();
		}

		private static string GetResult2(List<string> inputCol)
		{
			ComputerSimulator cs = new ComputerSimulator(inputCol);

			return cs.FindCorrectADetermin();
		}
	}
}
