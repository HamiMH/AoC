using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day21cs
{



	internal class ArrowPad
	{
		public static byte LEFT = 4;
		public static byte RIGHT = 2;
		public static byte UP = 8;
		public static byte DOWN = 1;
		public static byte A = 16;

		public static Dictionary<int, long> PressesToGet = new()
		{
			{LEFT,7 },
			{LEFT+UP,7 },
			{LEFT+DOWN,7 },
			{RIGHT,3 },
			{RIGHT+UP,5 },
			{RIGHT+DOWN,5 },
			{UP,3 },
			{DOWN,5 },
		};

		public static Dictionary<int, (int, int)> KeyPosition = new()
		{
			{ LEFT,(1,1)},
			{ RIGHT,(3,1)},
			{ UP,(2,2)},
			{ DOWN,(2,1)},
			{ A,(3,2)},
		};

		public static Dictionary<(byte, byte), List<byte>> _solutionDict = new()
		{
			{(LEFT,LEFT),new List<byte>(){A } },
			{(LEFT,RIGHT),new List<byte>(){RIGHT,RIGHT,A } },
			{(LEFT,UP),new List<byte>(){RIGHT,UP,A } },
			{(LEFT,DOWN),new List<byte>(){RIGHT,A } },
			{(LEFT,A),new List<byte>(){RIGHT,RIGHT,UP,A } },
			{(RIGHT,LEFT),new List<byte>(){LEFT,LEFT,A } },
			{(RIGHT,RIGHT),new List<byte>(){A } },
			{(RIGHT,UP),new List<byte>(){ UP, LEFT,A } },
			{(RIGHT,DOWN),new List<byte>(){LEFT,A} },
			{(RIGHT,A),new List<byte>(){UP,A } },
			{(UP,LEFT),new List<byte>(){DOWN,LEFT,A } },
			{(UP,RIGHT),new List<byte>(){ DOWN, RIGHT,A } },
			{(UP,UP),new List<byte>(){A } },
			{(UP,DOWN),new List<byte>(){DOWN ,A} },
			{(UP,A),new List<byte>(){RIGHT,A } },
			{(DOWN,LEFT),new List<byte>(){LEFT,A } },
			{(DOWN,RIGHT),new List<byte>(){RIGHT,A } },
			{(DOWN,UP),new List<byte>(){UP,A } },
			{(DOWN,DOWN),new List<byte>(){A } },
			{(DOWN,A),new List<byte>(){UP,RIGHT,A } },
			{(A,LEFT),new List<byte>(){DOWN,LEFT,LEFT,A } },
			{(A,RIGHT),new List<byte>(){DOWN,A } },
			{(A,UP),new List<byte>(){LEFT,A } },
			{(A,DOWN),new List<byte>(){ LEFT, DOWN, A } },
			{(A,A),new List<byte>(){A } },
			};

		public static Dictionary<(byte, byte), List<List<byte>>> _solutionDict2 = new()
		{
			{(LEFT,LEFT),new List<List<byte>>(){new(){A } } },
			{(LEFT,RIGHT),new List<List<byte>>(){ new() { RIGHT,RIGHT,A } } },
			{(LEFT,UP),new List<List<byte>>(){ new() { RIGHT,UP,A }}  },
			{(LEFT,DOWN),new List<List<byte>>(){ new() { RIGHT,A }}  },
			{(LEFT,A),new List<List<byte>>(){ new() { RIGHT, RIGHT, UP, A } , new() { RIGHT,  UP, RIGHT, A } }  },
			{(RIGHT,LEFT),new List<List<byte>>(){ new() { LEFT,LEFT,A }}  },
			{(RIGHT,RIGHT),new List<List<byte>>(){ new() { A }}  },
			{(RIGHT,UP),new List<List<byte>>(){ new() { UP, LEFT,A }, new() { LEFT,UP, A } } },
			{(RIGHT,DOWN),new List<List<byte>>(){ new() { LEFT,A} } },
			{(RIGHT,A),new List<List<byte>>(){ new() { UP,A } } },
			{(UP,LEFT),new List<List<byte>>(){ new() { DOWN,LEFT,A }}  },
			{(UP,RIGHT),new List<List<byte>>(){ new() { DOWN, RIGHT, A } , new() { RIGHT, DOWN, A } }  },
			{(UP,UP),new List<List<byte>>(){ new() { A }}  },
			{(UP,DOWN),new List<List<byte>>(){ new() { DOWN ,A} } },
			{(UP,A),new List<List<byte>>(){ new() { RIGHT,A }}  },
			{(DOWN,LEFT),new List<List<byte>>(){ new() { LEFT,A }}  },
			{(DOWN,RIGHT),new List<List<byte>>(){ new() { RIGHT,A }}  },
			{(DOWN,UP),new List<List<byte>>(){ new() { UP,A }}  },
			{(DOWN,DOWN),new List<List<byte>>(){ new() { A }}  },
			{(DOWN,A),new List<List<byte>>(){ new() { UP, RIGHT, A }, new() { RIGHT, UP,  A } } },
			{(A,LEFT),new List<List<byte>>(){ new() { DOWN, LEFT, LEFT, A } , new() { LEFT, DOWN, LEFT, A } } },
			{(A,RIGHT),new List<List<byte>>(){ new() { DOWN,A } } },
			{(A,UP),new List<List<byte>>(){ new() { LEFT,A }}  },
			{(A,DOWN),new List<List<byte>>(){ new() { LEFT, DOWN, A } , new() { DOWN, LEFT, A } }  },
			{(A,A),new List<List<byte>>(){ new() { A }}  },
			};

		internal long MoveBy2((int, int) diff)
		{
			int direction = 0;
			if (diff.Item1 > 0)
				direction += RIGHT;
			else if (diff.Item1 < 0)
				direction += LEFT;
			if (diff.Item2 > 0)
				direction += UP;
			else if (diff.Item2 < 0)
				direction += DOWN;
			long absDiff = Math.Abs(diff.Item1) + Math.Abs(diff.Item2);
			long pressesToNavig = ArrowPad.PressesToGet[direction];

			return pressesToNavig + absDiff;
		}

		internal long MoveBy1((int, int) diff)
		{
			foreach (List<byte> list in _solutionDict.Values)
				list.Sort();
			List<byte> sol = new List<byte>() { A };
			int direction = 0;
			sol.AddRange(Enumerable.Repeat(RIGHT, Math.Max(0, diff.Item1)));
			sol.AddRange(Enumerable.Repeat(LEFT, Math.Max(0, -diff.Item1)));
			sol.AddRange(Enumerable.Repeat(UP, Math.Max(0, diff.Item2)));
			sol.AddRange(Enumerable.Repeat(DOWN, Math.Max(0, -diff.Item2)));
			sol.Add(A);

			List<byte> sol2 = new List<byte>() { A };
			for (int i = 1; i < sol.Count; i++)
			{
				sol2.AddRange(_solutionDict[(sol[i - 1], sol[i])]);
			}

			List<byte> sol3 = new List<byte>();
			for (int i = 1; i < sol2.Count; i++)
			{
				sol3.AddRange(_solutionDict[(sol2[i - 1], sol2[i])]);
			}

			PrintDirList(sol);
			PrintDirList(sol2);
			PrintDirList(sol3);
			return sol3.Count;
		}
		internal long MoveBy((int, int) keyFrom, (int, int) keyInto, int depth)
		{
			(int, int) diff = (keyInto.Item1 - keyFrom.Item1, keyInto.Item2 - keyFrom.Item2);
			//foreach (List<byte> list in _solutionDict.Values)
			//	list.Sort();
			List<byte> sol = new List<byte>() { A };

			if (keyFrom.Item2 == 1 && keyInto.Item1 == 1)
			{
				sol.AddRange(Enumerable.Repeat(DOWN, Math.Max(0, -diff.Item2)));
				sol.AddRange(Enumerable.Repeat(UP, Math.Max(0, diff.Item2)));
				sol.AddRange(Enumerable.Repeat(RIGHT, Math.Max(0, diff.Item1)));
				sol.AddRange(Enumerable.Repeat(LEFT, Math.Max(0, -diff.Item1)));
			}
			else if (keyFrom.Item1 == 1 && keyInto.Item2 == 1)
			{
				sol.AddRange(Enumerable.Repeat(UP, Math.Max(0, diff.Item2)));
				sol.AddRange(Enumerable.Repeat(RIGHT, Math.Max(0, diff.Item1)));
				sol.AddRange(Enumerable.Repeat(LEFT, Math.Max(0, -diff.Item1)));
				sol.AddRange(Enumerable.Repeat(DOWN, Math.Max(0, -diff.Item2)));
			}
			else
			{
				sol.AddRange(Enumerable.Repeat(DOWN, Math.Max(0, -diff.Item2)));
				sol.AddRange(Enumerable.Repeat(RIGHT, Math.Max(0, diff.Item1)));
				sol.AddRange(Enumerable.Repeat(LEFT, Math.Max(0, -diff.Item1)));
				sol.AddRange(Enumerable.Repeat(UP, Math.Max(0, diff.Item2)));
			}
			sol.Add(A);
			long result = LenByRecursion2(sol, 0, depth);
			Console.WriteLine($"keyFrom = {keyFrom}, keyInto = {keyInto}, result = {result}");
			return result;

			//for (int j = 0; j < 25; j++)
			//{
			//	List<byte> solNew = new List<byte>() { A };
			//	for (int i = 1; i < sol.Count; i++)
			//	{
			//		solNew.AddRange(_solutionDict[(sol[i - 1], sol[i])]);
			//	}
			//	sol = solNew;
			//}
			//return sol.Count - 1;



			//List<int> sol2 = new List<int>() { A };
			//for (int i = 1; i < sol.Count; i++)
			//{
			//	sol2.AddRange(_solutionDict[(sol[i - 1], sol[i])]);
			//}

			//List<int> sol3 = new List<int>() { A} ;
			//for (int i = 1; i < sol2.Count; i++)
			//{
			//	sol3.AddRange(_solutionDict[(sol2[i - 1], sol2[i])]);
			//}

			//PrintDirList(sol);
			//PrintDirList(sol2);
			//PrintDirList(sol3);
			//return sol3.Count-1;
		}

		private List<List<byte>> GenerateDirs((int, int) keyFrom, (int, int) keyInto)
		{
			if(keyFrom.Item1==keyInto.Item1 && keyFrom.Item2 == keyInto.Item2)
			{
				return new List<List<byte>>() { new List<byte>() { A} };
			}
			List<List<byte>> dirsNew = new List<List<byte>>();

			if (keyFrom.Item1 > keyInto.Item1)
			{
				(int, int) newKeyFrom = (keyFrom.Item1 - 1, keyFrom.Item2);
				if (newKeyFrom != (1, 1))
				{
					foreach (List<byte> dirsOld in GenerateDirs(newKeyFrom, keyInto))
						dirsNew.Add((new List<byte>() { LEFT }).Concat(dirsOld).ToList());
				}
			}
			if (keyFrom.Item1 < keyInto.Item1)
			{
				(int, int) newKeyFrom = (keyFrom.Item1 + 1, keyFrom.Item2);
				if (newKeyFrom != (1, 1))
				{
					foreach (List<byte> dirsOld in GenerateDirs(newKeyFrom, keyInto))
						dirsNew.Add((new List<byte>() { RIGHT }).Concat(dirsOld).ToList());
				}
			}
			if (keyFrom.Item2 > keyInto.Item2)
			{
				(int, int) newKeyFrom = (keyFrom.Item1, keyFrom.Item2 - 1);
				if (newKeyFrom != (1, 1))
				{
					foreach (List<byte> dirsOld in GenerateDirs(newKeyFrom, keyInto))
						dirsNew.Add((new List<byte>() { DOWN }).Concat(dirsOld).ToList());
				}
			}
			if (keyFrom.Item2 < keyInto.Item2)
			{
				(int, int) newKeyFrom = (keyFrom.Item1, keyFrom.Item2 + 1);
				if (newKeyFrom != (1, 1))
				{
					foreach (List<byte> dirsOld in GenerateDirs(newKeyFrom, keyInto))
						dirsNew.Add((new List<byte>() { UP }).Concat(dirsOld).ToList());
				}
			}

			return dirsNew;
		}

		internal long MoveBy0((int, int) keyFrom, (int, int) keyInto, int depth)
		{
			List<List<byte>> dirs = GenerateDirs(keyFrom, keyInto);

			long result = long.MaxValue;
			foreach (List<byte> dir in dirs)
			{
				dir.Insert(0, A);
				PrintDirList(dir);
				result = Math.Min(result, LenByRecursion2(dir, 0, depth));

			}
			Console.WriteLine($"keyFrom = {keyFrom}, keyInto = {keyInto}, result = {result}");
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

		private long ListHash(List<byte> inList)
		{
			long sum = 0;
			foreach (byte b in inList)
			{
				sum *= 64;
				sum += b;
			}
			return sum;
		}
		public Dictionary<(long, int), long> _hashDict = new();
		private long LenByRecursion(List<byte> dirs, int depth, int maxDepth)
		{
			if (depth == maxDepth)
				return dirs.Count - 1;

			long inListHash = ListHash(dirs);
			if (_hashDict.ContainsKey((inListHash, depth)))
				return _hashDict[(inListHash, depth)];

			long sum = 0;
			List<List<byte>> splitted = SplitList(dirs, A);
			foreach (List<byte> subDirs in splitted)
			{
				List<byte> subDirsNew = new List<byte>() { A };
				for (int i = 1; i < subDirs.Count; i++)
					subDirsNew.AddRange(_solutionDict[(subDirs[i - 1], subDirs[i])]);

				sum += LenByRecursion(subDirsNew, depth + 1, maxDepth);
			}
			_hashDict[(inListHash, depth)] = sum;
			return sum;
		}

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
					foreach (List<byte> dirsFromDict in _solutionDict2[(subDirsSplit[i - 1], subDirsSplit[i])])
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
					long len = LenByRecursion(subDirs, depth + 1, maxDepth);
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

	internal class NumberPad
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
		public ArrowPad ap = new ArrowPad();
		public long PressesOnArrow(int keySymFrom, int keySymTo, int depth)
		{
			(int, int) keyFrom = KeyPosition[keySymFrom];
			(int, int) keyTo = KeyPosition[keySymTo];
			(int, int) diff = (keyTo.Item1 - keyFrom.Item1, keyTo.Item2 - keyFrom.Item2);

			long nOfMoves = ap.MoveBy0(keyFrom, keyTo, depth);
			//long nOfMoves = ap.MoveBy(diff);
			Console.WriteLine($"{keySymFrom}->{keySymTo}={nOfMoves}");
			return nOfMoves;
		}
	}
	internal class PanelSimulator
	{
		NumberPad np = new NumberPad();
		public Dictionary<(long, int), long> GetMEMO()
		{ return np.ap._hashDict; }

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

			Console.WriteLine($"sum = {sum}");
			return sum;
		}

	}
}

