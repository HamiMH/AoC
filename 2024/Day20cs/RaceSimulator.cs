

using System.Numerics;

namespace Day20cs
{
	internal class RaceSimulator
	{
		private static readonly List<(int, int)> Directions = new List<(int, int)>
		{
			( 0,  1),
			( 0, -1),
			( 1,  0),
			(-1,  0),
		};
		private (int, int) _start;
		private	(int, int) _end;
		private List<List<int>> _maze = new List<List<int>>();

		public RaceSimulator(List<string> inputCol)
		{
			for (int i = 0; i < inputCol.Count; i++)
			{
				List<int> row = new List<int>();
				for (int j = 0; j < inputCol[i].Length; j++)
				{
					switch (inputCol[i][j])
					{
						case '#':
							row.Add(2);
							break;
						case 'S':
							_start = (j, i);
							row.Add(0);
							break;
						case 'E':
							_end = (j, i);
							row.Add(0);
							break;
						default:
							row.Add(0);
							break;
					}
				}
				_maze.Add(row);
			}
		}
		internal long CalcAllCheats(int diff)
		{
			HashSet<(int, int)> visited = new HashSet<(int, int)>();
			Dictionary<(int, int), long> distances = new Dictionary<(int, int), long>();
			int stackSize = 16 * 1024 * 1024; ;
			Thread thread = new Thread(() => RunDfS(_start, visited, distances, 0), stackSize);
			thread.Start();
			thread.Join();
			long sum = 0;
			foreach (KeyValuePair<(int, int), long> kvp in distances)
			{
				for (int i = -diff; i <= diff; i++)
					for (int j = -diff; j <= diff; j++)
					{
						if (Math.Abs(i) + Math.Abs(j) > diff)
							continue;
						(int, int) newPos = (kvp.Key.Item1 + i, kvp.Key.Item2 + j);
						if (distances.ContainsKey(newPos))
						{
							long savedTime = distances[newPos] - distances[kvp.Key] - Math.Abs(i) - Math.Abs(j);
							if (savedTime >= 100)						//test 50 result 285
								sum++;

						}
					}
			}
			return sum;
		}

		private bool RunDfS((int, int) posNow, HashSet<(int, int)> visited, Dictionary<(int, int), long> distances, int cost)
		{
			if (visited.Contains(posNow))
				return false;
			if (_maze[posNow.Item2][posNow.Item1] == 2)
				return false;
			visited.Add(posNow);

			if (posNow == _end)
			{
				distances[posNow] = cost;
				return true;
			}
			foreach ((int, int) dir in Directions)
			{
				(int, int) newPos = (posNow.Item1 + dir.Item1, posNow.Item2 + dir.Item2);
				if (RunDfS(newPos, visited, distances, cost + 1))
				{
					distances[posNow] = cost;
					return true;
				}
			}
			return false;
		}
	}
}