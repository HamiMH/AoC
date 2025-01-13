

namespace Day25cs
{
	internal class KeyLockAnalyser
	{
		public KeyLockAnalyser()
		{
		}

		internal long GetAllValidComb(KeyLockDatabase kld)
		{
			long sum = 0;
			foreach (List<int> key in kld.Keys)
			{
				foreach (List<int> lock0 in kld.Locks)
				{
					if (IsValidComb(key, lock0))
						sum++;
				}
			}
			return sum;
		}

		private bool IsValidComb(List<int> key, List<int> lock0)
		{
			for(int i = 0; i < key.Count; i++)
				if (key[i] +lock0[i] >7)
					return false;
			 return true;
		}
	}
}