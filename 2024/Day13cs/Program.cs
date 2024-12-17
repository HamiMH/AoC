using System.Diagnostics;

namespace Day13cs
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

		private static (long, long) Vals(string str,char splC,long increasePrice)
		{
			str = str.Split(':').Last().Trim();
			List<string> splitL = str.Split(',').Select(x => x.Trim()).ToList();
			return (increasePrice+long.Parse(splitL.First().Split(splC).Last()), increasePrice+ long.Parse(splitL.Last().Split(splC).Last()));
		}

		private static List<ButtonCalculator> GetButtons(List<string> inputCol,long increasePrice)
		{
			List<ButtonCalculator> buttons = new List<ButtonCalculator>();

			for (int i = 0;i< inputCol.Count; i+=4)
				buttons.Add(new ButtonCalculator(Vals(inputCol[i], '+',0), Vals(inputCol[i+1], '+',0), Vals(inputCol[i+2], '=', increasePrice)));

			return buttons;
		}
		private static string GetResult1(List<string> inputCol)
		{
			List<ButtonCalculator> buttons = GetButtons(inputCol,0);
			return buttons.Select(x=>x.GetPrice()).Sum().ToString();
		}

	

		private static string GetResult2(List<string> inputCol)
		{
			List<ButtonCalculator> buttons = GetButtons(inputCol, 10000000000000);
			return buttons.Select(x => x.GetPrice()).Sum().ToString();
		}
	}
}
