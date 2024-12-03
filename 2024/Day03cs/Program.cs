using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day03cs
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

		private static string GetResult2(List<string> inputCol)
		{
			 string all = string.Join("", inputCol);
			string pattern = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)";
			MatchCollection matches = Regex.Matches(all, pattern);

			long multiplier = 1;
			return matches.Sum(x => 
			{ 
				switch(x.Value)
				{
					case "do()":
						multiplier=1;
						return 0;
					case "don't()":
						multiplier = 0;
						return 0;
					default:
						return multiplier* long.Parse(x.Groups[1].Value) * long.Parse(x.Groups[2].Value);
				}
			}
			).ToString();
		}

		private static string GetResult1(List<string> inputCol)
		{		
			string all = string.Join("", inputCol);
			string pattern = @"mul\((\d+),(\d+)\)";
			MatchCollection matches = Regex.Matches(all, pattern);

			return matches.Sum(x => { return long.Parse(x.Groups[1].Value) * long.Parse(x.Groups[2].Value); }).ToString();
		}
	}
}
