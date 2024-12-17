


using System.Linq;
using System.Text;

namespace Day14cs
{

	internal class Robot
	{
		public (int, int) Speed;
		public (int, int) Position;

		public Robot(string str)
		{
			string[] parts = str.Trim().Split(" ");
			string[] parts2 = parts.Select(x => x.Split('=').Last()).ToArray();

			string[] parts21 = parts2.First().Split(',');
			string[] parts22 = parts2.Last().Split(',');


			Position = (int.Parse(parts21[0]), int.Parse(parts21[1]));
			Speed = (int.Parse(parts22[0]), int.Parse(parts22[1]));
		}
	}

	internal class RobotSimulator
	{
		private (int, int) _areaDimenion;
		private List<Robot> _robots;

		public int GetQuadrant(Robot robot)
		{
			int qua = 1;

			if (_areaDimenion.Item1 % 2 == 1 && _areaDimenion.Item1 / 2 == robot.Position.Item1)
				return 0;
			if (_areaDimenion.Item2 % 2 == 1 && _areaDimenion.Item2 / 2 == robot.Position.Item2)
				return 0;

			if (robot.Position.Item1 >= _areaDimenion.Item1 / 2)
				qua++;
			if (robot.Position.Item2 >= _areaDimenion.Item2 / 2)
				qua += 2;

			return qua;
		}

		public RobotSimulator(List<string> inputCol, (int, int) value)
		{
			_areaDimenion = value;
			_robots = inputCol.Select(x => new Robot(x)).ToList();
		}

		public void RunSimulation(int finalTime)
		{

			for (int i = 0; i < finalTime; i++)
			{
				foreach (var robot in _robots)
				{
					MoveRobot(robot);
				}
			}

		}

		private static bool ValidateChristmasTree(List<string> inputCol)
		{

			int height = inputCol.Count;
			if (height < 3) return false; // Too short to be a tree

			int maxWidth = inputCol.Max(line => line.Length);
			int center = maxWidth / 2;

			// Check for triangular shape and symmetry
			for (int i = 0; i < height - 1; i++)
			{
				string line = inputCol[i].PadRight(maxWidth);
				int left = line.IndexOf('#');
				int right = line.LastIndexOf('#');

				if (left == -1 || right == -1 || left > center || right < center) return false; // Not a valid tree shape
				if (right - left != 2 * i) return false; // Not expanding correctly
			}

			// Check for trunk
			string trunk = inputCol[height - 1].Trim();
			if (trunk.Length != 1 || trunk[0] != '#') return false;

			return true;
		}

		public void RunSimulationAndPrint(int finalTime)
		{

			for (int i = 0; ; i++)
			{
				if (ValidatePossibility(100))
				{

					string s = PrintLayout();
					//if (ValidateChristmasTree(s.Split("\r").ToList()))
					{
						Console.WriteLine("Time: " + i);
						Console.WriteLine(s);
						Console.WriteLine("Press Enter to continue");
						Console.ReadLine();
					}
				}

				foreach (var robot in _robots)
				{
					MoveRobot(robot);
				}
			}

		}

		private bool ValidatePossibility(int threshold)
		{
			var hs = _robots.Select(x => x.Position).ToHashSet();
			int counter = 0;
			foreach (var robotPos in hs)
			{
				if (
					hs.Contains((robotPos.Item1 + 1, robotPos.Item2 + 1))
				||
					hs.Contains((robotPos.Item1 - 1, robotPos.Item2 + 1))
					)
					counter++;
			}
			if (counter > threshold)
				return true;
			return false;
		}

		public long CalculateResult()
		{
			int[] res = new int[5] { 0, 0, 0, 0, 0 };
			foreach (var robot in _robots)
			{
				res[GetQuadrant(robot)]++;
			}
			long resl = 1;
			for (int i = 1; i < 5; i++)
			{
				resl *= res[i];
			}
			return resl;
		}

		private void MoveRobot(Robot robot)
		{
			robot.Position.Item1 = (robot.Position.Item1 + robot.Speed.Item1 + _areaDimenion.Item1) % _areaDimenion.Item1;
			robot.Position.Item2 = (robot.Position.Item2 + robot.Speed.Item2 + _areaDimenion.Item2) % _areaDimenion.Item2;

		}

		internal string PrintLayout()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < _areaDimenion.Item2; i++)
			{
				for (int j = 0; j < _areaDimenion.Item1; j++)
				{
					if (_robots.Any(x => x.Position.Item1 == j && x.Position.Item2 == i))
						stringBuilder.Append("#");
					else
						stringBuilder.Append(".");
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
	}
}