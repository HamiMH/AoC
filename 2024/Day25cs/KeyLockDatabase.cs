


namespace Day25cs
{
	internal class KeyLockDatabase
	{
		internal List<List<int>> Keys = new List<List<int>>();
		internal List<List<int>> Locks = new List<List<int>>();
		public KeyLockDatabase(List<string> inputCol)
		{
			List<string> tmp = new List<string>();
			foreach (string col in inputCol)
			{
				if (col == "")
				{
					AddItem(tmp);
					tmp.Clear();
				}
				else
				{
					tmp.Add(col);
				}
			}
			AddItem(tmp);
		}

		private void AddItem(List<string> tmp)
		{
			if (tmp.First() == "#####")
				AddKey(tmp);
			else
				AddLock(tmp);
		}

		private void AddKey(List<string> tmp)
		{
			List<int> key = new List<int>();
			for (int i = 0; i < tmp.First().Length; i++)
			{
				int keyPos = 0;
				for (int j = 0; j < tmp.Count; j++)
				{
					if (tmp[j][i] != '#')
					{
						key.Add(keyPos);
						break;
					}
					keyPos++;
				}
			}
			Keys.Add(key);
		}
		private void AddLock(List<string> tmp)
		{
			List<int> lock0 = new List<int>();
			for (int i = 0; i < tmp.First().Length; i++)
			{
				int lockPos = 0;
				for (int j = tmp.Count - 1; j >= 0; j--)
				{
					if (tmp[j][i] != '#')
					{
						lock0.Add(lockPos);
						break;
					}
					lockPos++;
				}
			}
			Locks.Add(lock0);
		}
	}
}
