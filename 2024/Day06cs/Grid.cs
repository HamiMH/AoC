

using System;

namespace Day06cs;

internal class Grid
{
	private List<List<int>> _grid = new List<List<int>>();
	private List<List<List<bool>>> _attended = new List<List<List<bool>>>();
	private Guard _guard = new Guard();


	private int InitPosition(char c, int x, int y)
	{
		switch (c)
		{
			case '^':
				_guard.DirecId = 0;
				_guard.X = x;
				_guard.Y = y;
				return 0;
			case '>':
				_guard.DirecId = 1;
				_guard.X = x;
				_guard.Y = y;
				return 0;
			case 'v':
				_guard.DirecId = 2;
				_guard.X = x;
				_guard.Y = y;
				return 0;
			case '<':
				_guard.DirecId = 3;
				_guard.X = x;
				_guard.Y = y;
				return 0;
			case '#':
				return -1;
			default:
				return 0;
		}
	}
	public Grid(List<string> inputCol)
	{
		for (int i = 0; i < inputCol.Count; i++)
		{
			List<int> row = new List<int>();
			List<List<bool>> rowAttended = new List<List<bool>>();
			for (int j = 0; j < inputCol.First().Length; j++)
			{
				row.Add(InitPosition(inputCol[i][j], j, i));
				rowAttended.Add(new List<bool>() { false, false, false, false });
			}
			_grid.Add(row);
			_attended.Add(rowAttended);
		}
	}

	private bool CheckMove()
	{
		while (true)
		{
			if (_guard.X < 0 || _guard.X >= _grid.First().Count || _guard.Y < 0 || _guard.Y >= _grid.Count)
				return false;
			int guardNextX = _guard.X + _guard.Direct.Item1;
			int guardNextY = _guard.Y + _guard.Direct.Item2;

			if (guardNextX < 0 || guardNextX >= _grid.First().Count || guardNextY < 0 || guardNextY >= _grid.Count)
			{
				return false;
			}
			else if (_grid[guardNextY][guardNextX] == -1)
			{
				_guard.DirecId++;
				continue;
			}
			return true;
		}
	}

	public int Simulate()
	{
		int nOfSteps = 0;

		while (true)
		{
			if (!MakeMove())
				break;

			if (_grid[_guard.Y][_guard.X] == 0)
				nOfSteps++;
			_grid[_guard.Y][_guard.X] = nOfSteps;

			_guard.Move();
		}
		//PrintGrid();
		return nOfSteps + 1;
	}

	public int SimulateGetNOfCircle()
	{
		int nOfCirles = 0;
		Guard guard = new Guard();
		while (true)
		{
			if (!MakeMove())
				break;
			int guardNextX = _guard.X + _guard.Direct.Item1;
			int guardNextY = _guard.Y + _guard.Direct.Item2;
			if (_grid[guardNextY][guardNextX] == 0)
			{

				_grid[guardNextY][guardNextX] = -1;
				guard.CopyFrom(_guard);
				if (TestCircle(guard))
				{
					nOfCirles++;
				}
				_grid[guardNextY][guardNextX] = 1;
			}


			_guard.Move();
		}
		//PrintGrid();
		return nOfCirles;
		//return correctPlacement.Count;
	}

	private bool TestCircle(Guard guard)
	{
		HashSet<Tuple<int, int, int>> visited = new HashSet<Tuple<int, int, int>>();
		while (true)
		{
			if (!MakeMove())
				break;

			if (visited.Contains(Tuple.Create(guard.X, guard.Y, guard.DirecId)))
				return true;
			visited.Add(Tuple.Create(guard.X, guard.Y, guard.DirecId));

			guard.Move();
		}

		return false;
	}

	public void PrintGrid()
	{
		for (int i = 0; i < _grid.Count; i++)
		{
			for (int j = 0; j < _grid.First().Count; j++)
			{
				Console.Write($"{_grid[i][j],3}");
			}
			Console.WriteLine();
		}
	}
}
