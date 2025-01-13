using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21cs
{
	internal class DeterSimulator
	{
		NumberPadDeter np = new NumberPadDeter();

		public long Simulate(string inString, int depth)
		{
			List<int> targetSequence = inString.Select(c => c == 'A' ? 10 : c - '0').ToList();

			int oldB = 10;
			long sum = 0;
			foreach (int newB in targetSequence)
			{
				sum += np.PressesOnArrow(oldB, newB, depth);
				oldB = newB;
			}

			//Console.WriteLine($"sum = {sum}");
			return sum;
		}
	}
	internal class NumberPadDeter
	{
		public static Dictionary<int, (int, int)> KeyPosition = new()
		{
			{ 0,(2,1)},
			{ 1,(1,2)},
			{ 2,(2,2)},
			{ 3,(3,2)},
			{ 4,(1,3)},
			{ 5,(2,3)},
			{ 6,(3,3)},
			{ 7,(1,4)},
			{ 8,(2,4)},
			{ 9,(3,4)},
			{ 10,(3,1)},
		};
		public ArrowPadDeter ap = new ArrowPadDeter();
		public long PressesOnArrow(int keySymFrom, int keySymTo, int depth)
		{
			(int, int) keyFrom = KeyPosition[keySymFrom];
			(int, int) keyTo = KeyPosition[keySymTo];
			long nOfMoves = ap.MoveBy(keyFrom, keyTo, depth);
			//Console.WriteLine($"{keySymFrom}->{keySymTo}={nOfMoves}");
			return nOfMoves;
		}
	}

	internal class ArrowPadDeter
	{
		public static byte LEFT = 4;
		public static byte RIGHT = 2;
		public static byte UP = 8;
		public static byte DOWN = 1;
		public static byte A = 16;

		public static Dictionary<byte, (int, int)> KeyPosition = new()
		{
			{ LEFT,(1,1)},
			{ RIGHT,(3,1)},
			{ UP,(2,2)},
			{ DOWN,(2,1)},
			{ A,(3,2)},
		};
		private void AddNewKey((int, int) newKeyFrom, (int, int) keyInto, (int, int) forbiddenPos,byte dir, List<List<byte>> dirsNew)
		{
			if (newKeyFrom != forbiddenPos)
			{
				foreach (List<byte> dirsOld in GenerateDirs(newKeyFrom, keyInto, forbiddenPos))
					dirsNew.Add((new List<byte>() { dir }).Concat(dirsOld).ToList());
			}
		}
		private List<List<byte>> GenerateDirs((int, int) keyFrom, (int, int) keyInto,(int,int) forbiddenPos)
		{
			if (keyFrom.Item1 == keyInto.Item1 && keyFrom.Item2 == keyInto.Item2)
			{
				return new List<List<byte>>() { new List<byte>() { A } };
			}
			List<List<byte>> dirsNew = new List<List<byte>>();

			if (keyFrom.Item1 > keyInto.Item1)
			{
				(int, int) newKeyFrom = (keyFrom.Item1 - 1, keyFrom.Item2);
				AddNewKey(newKeyFrom, keyInto, forbiddenPos,LEFT, dirsNew);
			}
			if (keyFrom.Item1 < keyInto.Item1)
			{
				(int, int) newKeyFrom = (keyFrom.Item1 + 1, keyFrom.Item2);
				AddNewKey(newKeyFrom, keyInto, forbiddenPos, RIGHT, dirsNew);
			}
			if (keyFrom.Item2 > keyInto.Item2)
			{
				(int, int) newKeyFrom = (keyFrom.Item1, keyFrom.Item2 - 1);
				AddNewKey(newKeyFrom, keyInto, forbiddenPos, DOWN, dirsNew);
			}
			if (keyFrom.Item2 < keyInto.Item2)
			{
				(int, int) newKeyFrom = (keyFrom.Item1, keyFrom.Item2 + 1);
				AddNewKey(newKeyFrom, keyInto, forbiddenPos, UP, dirsNew);
			}
			return dirsNew;
		}


		internal long MoveBy((int, int) keyFrom, (int, int) keyInto, int depth)
		{
			List<List<byte>> dirs = GenerateDirs(keyFrom, keyInto, (1, 1));

			long result = long.MaxValue;
			foreach (List<byte> dir in dirs)
			{
				dir.Insert(0, A);
				//PrintDirList(dir);
				result = Math.Min(result, LenByRecursion2(dir, 0, depth));

			}
			//Console.WriteLine($"keyFrom = {keyFrom}, keyInto = {keyInto}, result = {result}");
			return result;
		}

		private List<List<byte>> SplitList(List<byte> inList, byte splitter)
		{
			List<List<byte>> result = new();
			List<byte> tmpList = new() { A };
			foreach (byte b in inList.Skip(1))
			{
				tmpList.Add(b);
				if (b == A)
				{
					result.Add(tmpList);
					tmpList = new() { A };
				}
			}
			return result;
		}

		private long ListHash(List<byte> inList) => inList.Aggregate(0L, (sum, b) => sum * 64 + b);
		public Dictionary<(long, int), long> _hashDict = new();
		private long LenByRecursion2(List<byte> dirs, int depth, int maxDepth)
		{
			if (depth == maxDepth)
				return dirs.Count - 1;

			long inListHash = ListHash(dirs);
			if (_hashDict.ContainsKey((inListHash, depth)))
				return _hashDict[(inListHash, depth)];

			long sum = 0;
			List<List<byte>> splitted = SplitList(dirs, A);
			foreach (List<byte> subDirsSplit in splitted)
			{
				List<byte> subDirsStart = new List<byte>() { A };
				List<List<byte>> subDirsOld = new List<List<byte>>() { subDirsStart };
				for (int i = 1; i < subDirsSplit.Count; i++)
				{
					List<List<byte>> subDirsNew = new List<List<byte>>();
					foreach (List<byte> dirsFromDict in GenerateDirs(KeyPosition[subDirsSplit[i - 1]],KeyPosition[subDirsSplit[i ]],(1,2)) )
					{
						foreach (List<byte> subDirOld in subDirsOld)
						{
							subDirsNew.Add(subDirOld.Concat(dirsFromDict).ToList());
						}
					}
					subDirsOld = subDirsNew;
				}
				long min = long.MaxValue;
				foreach (List<byte> subDirs in subDirsOld)
				{
					long len = LenByRecursion2(subDirs, depth + 1, maxDepth);
					min = Math.Min(min, len);
				}

				sum += min;
			}
			_hashDict[(inListHash, depth)] = sum;
			return sum;
		}

		private void PrintDirList(List<byte> sol2)
		{
			foreach (int i in sol2)
			{
				if (i == A)
					Console.Write("A");
				if (i == LEFT)
					Console.Write("<");
				if (i == RIGHT)
					Console.Write(">");
				if (i == UP)
					Console.Write("^");
				if (i == DOWN)
					Console.Write("v");
			}
			Console.WriteLine();
		}
	}

}
