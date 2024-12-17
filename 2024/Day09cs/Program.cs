using System.Diagnostics;

namespace Day09cs
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

			PrintInputStats(inputCol);

			DiskMap diskMap = new DiskMap(inputCol);
			diskMap.RunCompression();
			return diskMap.CheckSum().ToString();
		}

		private static void PrintInputStats(List<string> inputCol)
		{
			string s = inputCol.First();
			long sum = 0;
			for (int i = 0; i < s.Length; i++)
			{
				sum += s[i] - '0';
			}
			Console.WriteLine("Sum of all numbers in the first line: " + sum);
			Console.WriteLine("Length of input: " + inputCol.First().Length);

		}

		private static string GetResult2(List<string> inputCol)
		{

			DiskMap2 diskMap = new DiskMap2(inputCol);
			diskMap.RunCompression();
			return diskMap.CheckSum().ToString();
		}
	}
}
