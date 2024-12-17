
namespace Day07cs
{
	internal class LineTester
	{
		private long _targetValue;
		private List<long> _values;

		private long _longProp;
			private long LongProp { get { return _longProp; } set { _longProp = value; } }

		public LineTester(string col)
		{
			string[] firstSplit = col.Split(":");
			firstSplit[0] = firstSplit[0].Trim();
			firstSplit[1] = firstSplit[1].Trim();
			string[] secondSplit = firstSplit[1].Split(" ");
			_targetValue = long.Parse(firstSplit[0]);
			_values = secondSplit.Select(long.Parse).ToList();
			int[] arr = new int[_values.Count];
			arr.Sum();
		}

		private long TestValues(int index, long currValue, bool thirdType)
		{
			if (index == 0)
			{
				return TestValues(1, _values[index],thirdType);
			}
			if (index == _values.Count)
			{
				return currValue == _targetValue ? _targetValue : 0;
			}
			if (currValue > _targetValue)
			{
				return 0;
			}
			long firstTest = TestValues(index + 1, currValue + _values[index], thirdType);
			if (firstTest > 0)
			{
				return firstTest;
			}
			long secondTest = TestValues(index + 1, currValue * _values[index], thirdType);
			if (secondTest > 0)
			{
				return secondTest;
			}
			if (thirdType)
			{
				long expo = (long)Math.Log10(_values[index]);
				long newCurr = currValue*PowerOf10( expo+1);
				long thirdTest = TestValues(index + 1, newCurr + _values[index], thirdType);
				if (thirdTest > 0)
				{
					return thirdTest;
				}
			}
			return 0;
		}

		internal long GetResult2Types()
		{
			return TestValues(0, 0,false);
		}
		internal long GetResult3Types()
		{
			return TestValues(0, 0, true);
		}

		internal long PowerOf10(long exp)
		{
			if (exp == 0)
			{
				return 1;
			}
			if (exp == 1)
			{
				return 10;
			}
			if (exp % 2 == 0)
			{
				long half = PowerOf10(exp / 2);
				return half * half;
			}
			else
			{
				long half = PowerOf10(exp / 2);
				return half * half * 10; 
			}

		}
	}
}