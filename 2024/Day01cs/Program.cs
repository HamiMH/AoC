using System.Collections;
using System.Diagnostics;

namespace Day01cs
{
	class MultiSet<T> : IEnumerable<KeyValuePair<T, long>>
	{
		private Dictionary<T, long> _dict = new Dictionary<T, long>();

		public void Add(T item)
		{
			if (_dict.ContainsKey(item))
			{
				_dict[item]++;
			}
			else
			{
				_dict.Add(item, 1);
			}
		}
		public void Clear() {
			_dict.Clear();
		}
		public long Count(T item)
		{
			if (_dict.ContainsKey(item))
			{
				return _dict[item];
			}
			return 0;
		}

		public IEnumerator<KeyValuePair<T, long>> GetEnumerator()
		{
			return _dict.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _dict.GetEnumerator();
		}
	}

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

		private static string GetResult1(List<string> inputCol)
		{
			List<long> left = new List<long>();
			List<long> right = new List<long>();
			long sum = 0;
			foreach (string s in inputCol)
			{
				string []split = s.Split("   ");
				left.Add(long.Parse(split[0]));
				right.Add(long.Parse(split[1]));
			}
			left.Sort();
			right.Sort();
			for (int i = 0; i < left.Count; i++)
			{
				sum += Math.Abs( left[i] - right[i]);
			}

			return sum.ToString();
		} 
		private static string GetResult2(List<string> inputCol)
		{
			MultiSet<long> left = new MultiSet<long>();
			MultiSet<long> right = new MultiSet<long>();

			long sum = 0;
			foreach (string s in inputCol)
			{
				string []split = s.Split("   ");
				left.Add(long.Parse(split[0]));
				right.Add(long.Parse(split[1]));
			}
			foreach (KeyValuePair<long, long> kvp in left)
			{
				long k = kvp.Key;
				long v = kvp.Value;
				sum += k*v*right.Count(k);
			}

			return sum.ToString();
		}
	}
}
