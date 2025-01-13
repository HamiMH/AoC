using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _001_MyDFT
{
	internal class DiscreteFourier
	{

		public DiscreteFourier() { }


		private double CalcSumOfFreqs(List<double> inVals, int freq)
		{
			double sum = 0;
			for (int i = 0; i < inVals.Count; i++)
			{
				sum += inVals[i] * Math.Sin((Math.PI / 2 + Math.PI * i) / freq);
			}
			return sum/inVals.Count;
		}

		public List<double> DFT(List<double> inVals)
		{
			List<double> outVals = new List<double>();
			int N = inVals.Count;

			for(int i=0;i<N;i++)
			{
				outVals.Add(CalcSumOfFreqs(inVals,i+1));
			}
			return outVals;
		}
	}
}
