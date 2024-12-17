


using System.Reflection.Metadata;

namespace Day12cs
{
	internal class RegionCalculator
	{
		private List<string> _grid;
		private List<List<bool>> _visited;

		public RegionCalculator(List<string> inputCol)
		{
			_grid = inputCol;
			_visited = _grid.Select(x => x.Select(y => false).ToList()).ToList();
		}

		public long CalculateAllRegions(bool second = false)
		{
			long regionSum = 0;
			for (int i = 0; i < _grid.Count; i++)
			{
				for (int j = 0; j < _grid[i].Length; j++)
				{
					if (_visited[i][j])
						continue;
					if (second)
						regionSum += CalculateRegion2(i, j);
					else
						regionSum += CalculateRegion(i, j);

				}
			}
			return regionSum;
		}

		private long CalculateRegion(int i, int j)
		{
			char thisChar = _grid[i][j];
			long area = 0;
			long perimeter = 0;
			Queue<(int, int)> queue = new Queue<(int, int)>();
			queue.Enqueue((i, j));
			while (queue.Count > 0)
			{
				(int, int) current = queue.Dequeue();
				if (_grid[current.Item1][current.Item2] != thisChar)
					continue;
				if (_visited[current.Item1][current.Item2])
					continue;

				area++;
				perimeter += PerimeterOfPoint(current, thisChar);
				AddNeighbours(current, queue);
				_visited[current.Item1][current.Item2] = true;
			}
			return area * perimeter;
		}
		private long CalculateRegion2(int i, int j)
		{
			char thisChar = _grid[i][j];
			long area = 0;
			long perimeter = 0;
			Queue<(int, int)> queue = new Queue<(int, int)>();
			queue.Enqueue((i, j));
			HashSet<(int, int)> perimeterPointsL = new HashSet<(int, int)>();
			HashSet<(int, int)> perimeterPointsR = new HashSet<(int, int)>();
			HashSet<(int, int)> perimeterPointsU = new HashSet<(int, int)>();
			HashSet<(int, int)> perimeterPointsD = new HashSet<(int, int)>();
			while (queue.Count > 0)
			{
				(int, int) current = queue.Dequeue();
				if (_grid[current.Item1][current.Item2] != thisChar)
					continue;
				if (_visited[current.Item1][current.Item2])
					continue;

				area++;
				PerimeterOfPoint2(current, thisChar, perimeterPointsL, perimeterPointsR, perimeterPointsU, perimeterPointsD);
				AddNeighbours(current, queue);
				_visited[current.Item1][current.Item2] = true;
			}
			List<(int, int)> perimeterPointsListL = perimeterPointsL.ToList();
			List<(int, int)> perimeterPointsListR = perimeterPointsR.ToList(); 
			List<(int, int)> perimeterPointsListU = perimeterPointsU.ToList();
			List<(int, int)> perimeterPointsListD = perimeterPointsD.ToList();
			perimeterPointsListL.Sort();
			perimeterPointsListR.Sort();
			perimeterPointsListU.Sort();
			perimeterPointsListD.Sort();
			perimeter += CalculatePerimeterOf2(perimeterPointsListL);
			perimeter += CalculatePerimeterOf2(perimeterPointsListR);
			perimeter += CalculatePerimeterOf2(perimeterPointsListU);
			perimeter += CalculatePerimeterOf2(perimeterPointsListD);
			return area * perimeter;
		}

		private long CalculatePerimeterOf2(List<(int, int)> perimeterPointsLR)
		{
			long perim = 0;
			(int, int) tmpPoint = (-2, -2);
			foreach ((int, int) point in perimeterPointsLR)
			{
				if (tmpPoint.Item1 != point.Item1)
				{
					perim++;
				}
				else if (point.Item2 == tmpPoint.Item2 + 1)
				{
				}
				else
				{
					perim++;
				}
				tmpPoint = point;
			}
			return perim;
		}

		private void PerimeterOfPoint2((int, int) current, char thisChar,
			HashSet<(int, int)> perimeterPointsL, HashSet<(int, int)> perimeterPointsR, HashSet<(int, int)> perimeterPointsU, HashSet<(int, int)> perimeterPointsD)
		{
			int oldy = current.Item1;
			int oldx = current.Item2;
			foreach ((int, int) direction in DIRECTIONS)
			{
				int y = current.Item1 + direction.Item1;
				int x = current.Item2 + direction.Item2;
				if (y < 0 || y >= _grid.Count || x < 0 || x >= _grid[0].Length)
				{
					if (direction.Item1 == 0)
					{
						if (direction.Item2 == -1)
							perimeterPointsL.Add((oldx, oldy));
						if (direction.Item2 == 1)
							perimeterPointsR.Add((oldx, oldy));
					}
					else
					{
						if (direction.Item1 == -1)
							perimeterPointsU.Add((oldy, oldx));
						if (direction.Item1 == 1)
							perimeterPointsD.Add((oldy, oldx));
					}
				}
				else if (_grid[y][x] != thisChar)
				{
					if (direction.Item1 == 0)
					{
						if (direction.Item2 == -1)
							perimeterPointsL.Add((oldx, oldy));
						if (direction.Item2 == 1)
							perimeterPointsR.Add((oldx, oldy));
					}
					else
					{
						if (direction.Item1 == -1)
							perimeterPointsU.Add((oldy, oldx));
						if (direction.Item1 == 1)
							perimeterPointsD.Add((oldy, oldx));
					}
				}
			}
		}

		private static List<(int, int)> DIRECTIONS = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0), };
		private void AddNeighbours((int, int) current, Queue<(int, int)> queue)
		{
			foreach ((int, int) direction in DIRECTIONS)
			{
				int x = current.Item1 + direction.Item1;
				int y = current.Item2 + direction.Item2;
				if (x < 0 || x >= _grid.Count || y < 0 || y >= _grid[x].Length)
					continue;
				queue.Enqueue((x, y));
			}
		}

		private long PerimeterOfPoint((int, int) current, char thisChar)
		{
			long perimeter = 0;
			foreach ((int, int) direction in DIRECTIONS)
			{
				int x = current.Item1 + direction.Item1;
				int y = current.Item2 + direction.Item2;
				if (x < 0 || x >= _grid.Count || y < 0 || y >= _grid[x].Length)
				{
					perimeter++;
				}
				else if (_grid[x][y] != thisChar)
				{
					perimeter++;
				}
			}
			return perimeter;
		}
	}
}