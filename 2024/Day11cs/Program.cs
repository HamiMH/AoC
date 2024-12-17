using System.Diagnostics;

namespace Day11cs
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


		private static long PowerOf10(long exp)
		{
			if (exp == 0)
			{
				return 1;
			}
			if (exp == 1)
			{
				return 10;
			}
			if (exp % 2 == 0)
			{
				long half = PowerOf10(exp / 2);
				return half * half;
			}
			else
			{
				long half = PowerOf10(exp / 2);
				return half * half * 10;
			}
		}

		private static long NumbOfDigits(long lo)
		{
			long logLo = (long)Math.Log10(lo) + 1;
			return logLo;
		}

		private static (long, long) SplitEvenDigits(long lo)
		{
			long nOfDig = NumbOfDigits(lo);
			long half = nOfDig / 2;
			long powHalf = PowerOf10(half);
			return (lo / powHalf, lo % powHalf);
		}

		private static void IncrementDict(Dictionary<long, long> dict, long key, long amount)
		{
			if (dict.ContainsKey(key))
			{
				dict[key] += amount;
			}
			else
			{
				dict.Add(key, amount);
			}
		}

		private static long RunSimulation(List<long> oldArr, int nOfSteps)
		{
			Dictionary<long, long> oldDict = oldArr.Select(x => new KeyValuePair<long, long>(x, 1)).ToDictionary();
			for (int i = 0; i < nOfSteps; i++)
			{
				Dictionary<long, long> newDict = new Dictionary<long, long>();
				foreach (KeyValuePair<long, long> keyValuePair in oldDict)
				{
					long l = keyValuePair.Key;
					long amount = keyValuePair.Value;

					if (l == 0)
					{
						IncrementDict(newDict, 1, amount);
					}
					else if (NumbOfDigits(l) % 2 == 0)
					{
						(long, long) pair = SplitEvenDigits(l);
						IncrementDict(newDict,pair.Item1,amount);
						IncrementDict(newDict,pair.Item2,amount);
					}
					else
					{
						IncrementDict(newDict,l * 2024L,amount);
					}
				}
				oldDict = newDict;
			}
			return oldDict.Select(kvp=>kvp.Value).Sum();
		}

		private static string GetResult1(List<string> inputCol)
		{
			List<long> oldArr = inputCol.First().Split(' ').Select(long.Parse).ToList();
			return RunSimulation(oldArr, 25).ToString();
		}

		private static string GetResult2(List<string> inputCol)
		{
			List<long> oldArr = inputCol.First().Split(' ').Select(long.Parse).ToList();
			return RunSimulation(oldArr, 75).ToString();
		}
	}
}
