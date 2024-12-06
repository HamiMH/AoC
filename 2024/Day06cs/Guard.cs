namespace Day06cs;

internal class Guard
{
	internal int X { get; set; }
	internal int Y { get; set; }
	internal int DirecId
	{
		get => _direcId;
		set
		{ _direcId = value % 4; }
	}
	private int _direcId;

	internal Guard()
	{
		X = 0;
		Y = 0;
		DirecId = 0;
	}
	internal Guard(Guard guard)
	{
		X = guard.X;
		Y = guard.Y;
		DirecId = guard.DirecId;
	}

	internal void CopyFrom(Guard guard)
	{
		X = guard.X;
		Y = guard.Y;
		DirecId = guard.DirecId;
	}

	internal void Move()
	{
		X += Direct.Item1;
		Y += Direct.Item2;
	}

	internal Tuple<int, int> Direct
	{
		get
		{
			switch (this._direcId)
			{
				case 0: return Tuple.Create<int, int>(0, -1);
				case 1: return Tuple.Create<int, int>(1, 0);
				case 2: return Tuple.Create<int, int>(0, 1);
				case 3: return Tuple.Create<int, int>(-1, 0);
				default: throw new Exception();
			}
		}
	}
}