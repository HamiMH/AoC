
namespace Day18cs
{
	internal class BFGrid
	{
		List<(int, int)> Grid;
		int Dimension;
		public BFGrid(List<string> inputCol, int dim)
		{
			Grid = inputCol.Select(x => { var y = x.Split(","); return (int.Parse(y.First()), int.Parse(y.Last())); }).ToList();
			Dimension = dim;
		}

		private int Heuristic((int, int) a)
		{
			return 2*Dimension-a.Item1-a.Item2;
		}
		public int RunSimulation( int fallen, ref (int, int) coord)
		{
			coord = Grid.Take(fallen).Last();
			HashSet <(int, int)> gridPoints=Grid.Take(fallen).ToHashSet();
			PriorityQueue<(int, int, int), int> priorityQueue = new PriorityQueue<(int, int, int), int>();
			priorityQueue.Enqueue((0, 0, 0), Heuristic((0,0)));
			HashSet<(int, int)> visited = new HashSet<(int, int)>();

			while (priorityQueue.Count > 0)
			{
				var (x, y, steps) = priorityQueue.Dequeue();
				if (x == Dimension - 1 && y == Dimension - 1)
					return steps;

				if (visited.Contains((x, y)))
					continue;
				visited.Add((x, y));
				if (gridPoints.Contains((x, y)))
					continue;
				if (x < 0 || y < 0 || x >= Dimension || y >= Dimension)
					continue;

				priorityQueue.Enqueue((x - 1, y, steps + 1), steps + 1+Heuristic((x-1,y)));
				priorityQueue.Enqueue((x + 1, y, steps + 1), steps + 1 + Heuristic((x + 1, y)));
				priorityQueue.Enqueue((x, y - 1, steps + 1), steps + 1 + Heuristic((x , y-1)));
				priorityQueue.Enqueue((x, y + 1, steps + 1), steps + 1 + Heuristic((x , y+1)));
			}

			return -1;
		}

		public void PrintGrid()
		{
			for (int i = 0; i < Dimension; i++)
			{
				for (int j = 0; j < Dimension; j++)
				{
					if (Grid.Contains((j, i)))
						Console.Write("#");
					else
						Console.Write(".");
				}
				Console.WriteLine();
			}
		}
	}
}