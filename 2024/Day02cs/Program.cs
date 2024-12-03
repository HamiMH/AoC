using System.Diagnostics;

namespace Day02cs
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<string> inputCol = new List<string>();
			string inp = Console.ReadLine();
			//while ((lineIn1 = Console.ReadLine()) != null)
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

		private static bool IsMonotonicV2(Stack<int>que,List<int> intArr,int index, int errors, int maxErrors, bool increasing)
		{
			int diff;
			int last;
			int now;
			for (int i = index; i < intArr.Count; i++)
			{
				now = intArr[i];
				if (que.Count ==0)
				{
					que.Push(now);
					continue;
				}
				last = que.Peek();

				diff = now - last;
				if (!increasing)
				{
					diff = -diff;
				}
				if (diff <= 0 || diff > 3)
				{
					if (errors < maxErrors)
					{
						Stack<int> temp1 = new Stack<int>(que.ToArray().Reverse());
						temp1.Pop();
						bool res1 = IsMonotonicV2(temp1, intArr, i, errors + 1, maxErrors, increasing);

						Stack<int> temp2 = new Stack<int>(que.ToArray().Reverse());
						bool res2 = IsMonotonicV2(temp2, intArr, i+1, errors + 1, maxErrors, increasing); ;
						return res1 || res2;
					}
					else
					{
						return false;
					}
				}
				que.Push(now);
			}
			return true;
		}
		private static bool IsMonotonic(List<int> intArr, int maxErrors, bool increasing)
		{
			return IsMonotonicV2(new Stack<int>(), intArr, 0, 0, maxErrors, increasing);
			//int diff;
			//int last = intArr.First();
			//int errors = 0;

			//foreach (int now in intArr.Skip(1))
			//{
			//	diff = now - last;
			//	if (!increasing)
			//	{
			//		diff = -diff;
			//	}
			//	if (diff <= 0 || diff > 3)
			//	{
			//		if (errors < maxErrors)
			//		{
			//			errors++;
			//			continue;
			//		}
			//		else
			//		{
			//			return false;
			//		}
			//	}
			//	last = now;
			//}
			//return true;
		}
		private static bool IsSafe(string str, int maxErrors)
		{
			string[] strArr = str.Split(" ");
			List<int> intArr = strArr.Select(int.Parse).ToList();
			if (IsMonotonic(intArr, maxErrors, true) || IsMonotonic(intArr, maxErrors, false))
			{
				return true;
			}
			return false;
		}


		private static string GetResult1(List<string> inputCol)
		{
			long sum = 0;
			foreach (string s in inputCol)
			{
				if (IsSafe(s, 0))
				{
					sum += 1;
				}
			}


			return sum.ToString();
		}
		private static string GetResult2(List<string> inputCol)
		{

			long sum = 0;
			foreach (string s in inputCol)
			{
				if (IsSafe(s, 1))
				{
					sum += 1;
					Console.WriteLine(s + ", true.");
				}
				else
				{
					Console.WriteLine(s + ", false.");
				}
			}
			return sum.ToString();
		}
	}
}
