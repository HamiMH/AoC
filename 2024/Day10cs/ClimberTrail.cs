

namespace Day10cs
{
	internal class ClimberTrail
	{
		private static List<(int, int)> DIRECTIONS = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };
		private Random _random = new Random();
		private List<List<int>> _grid;
		private Dictionary<(int, int), HashSet<(int, int)>> _trails = new Dictionary<(int, int), HashSet<(int, int)>>();

		public ClimberTrail(List<string> inputCol)
		{
			_grid = inputCol.Select(x => x.ToCharArray().Select(y => (y - '0')).ToList()).ToList();

			for (int i = 0; i < _grid.Count; i++)
				for (int j = 0; j < _grid[i].Count; j++)
					if (_grid[i][j] == 0)
						_trails.Add((i, j), new HashSet<(int, int)>());
		}



		public long NumbOfTrails()
		{
			long sum = 0;
			foreach (var trail in _trails)
			{
				FindTrails(0, trail.Key, trail.Value);
				sum += trail.Value.Count;
			}

			return sum;
		}

		private void FindTrails(int currVal, (int, int) pos, HashSet<(int, int)> value)
		{
			if (currVal == 9)
			{

				//value.Add(pos);
				value.Add((_random.Next(int.MaxValue), _random.Next(int.MaxValue)));
				return;
			}

			foreach ((int, int) dir in DIRECTIONS)
			{
				(int, int) newPos = (pos.Item1 + dir.Item1, pos.Item2 + dir.Item2);
				if (newPos.Item1 < 0 || newPos.Item1 >= _grid.Count || newPos.Item2 < 0 || newPos.Item2 >= _grid[0].Count)
					continue;
				if (_grid[newPos.Item1][newPos.Item2] == currVal + 1)
				{
					//value.Add(newPos);
					FindTrails(currVal + 1, newPos, value);
				}
			}
		}
	}
}