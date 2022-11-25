using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Simulator4D
    {
        private int InitSize { get => _initState.Count; }

        private List<List<bool>> _initState = new List<List<bool>>();
        private int SimulSize { get => InitSize + 2 * _numOfSteps + 4; }



        private bool[,,,,] SimState { get; set; } = new bool[1, 1, 1, 1, 1];

        private int _numOfSteps;

        public Simulator4D(int numOfSt, List<string> initState)
        {
            _numOfSteps = numOfSt;


            foreach (string line in initState)
            {
                List<bool> boolLine = new List<bool>();
                foreach (char c in line)
                {
                    if (c == '#')
                        boolLine.Add(true);
                    else
                        boolLine.Add(false);
                }
                _initState.Add(boolLine);
            }
        }

        public int ActiveCount()
        {
            int count = 0;
            for (int i = 0; i < SimulSize; i++)
                for (int j = 0; j < SimulSize; j++)
                    for (int k = 0; k < SimulSize; k++)
                        for (int l = 0; l < SimulSize; l++)
                            if (SimState[_numOfSteps, i, j, k, l])
                                count++;

            return count;
        }
        public bool Simulate()
        {
            InitSimState();
            FillSimState();

            for (int nOfSim = 1; nOfSim <= _numOfSteps; nOfSim++)
                for (int i = 1; i < SimulSize - 1; i++)
                    for (int j = 1; j < SimulSize - 1; j++)
                        for (int k = 1; k < SimulSize - 1; k++)
                            for (int l = 1; l < SimulSize - 1; l++)
                                SimState[nOfSim, i, j, k, l] = ResultOfNeib(nOfSim - 1, i, j, k, l);

            return true;
        }

        //private bool ResultOfNeib(int step, int ii, int jj, int kk)
        //{
        //    int count = 0;
        //    for (int i = -1; i <= 1; i++)
        //        for (int j = -1; j <= 1; j++)
        //            for (int k = -1; k <= 1; k++)
        //                if (SimState[step, ii + i, jj + j, kk + k])
        //                    count++;

        //    if (count == 3)
        //        return true;
        //    else
        //        return false;
        //}
        private bool ResultOfNeib(int step, int ii, int jj, int kk, int ll)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    for (int k = -1; k <= 1; k++)
                    for (int l = -1; l <= 1; l++)
                    {
                        if (i == 0 && j == 0 && k == 0 && l == 0)
                            continue;

                        if (SimState[step, ii + i, jj + j, kk + k, ll + l])
                            count++;
                    }
            if (SimState[step, ii, jj, kk, ll])
            {
                if (count == 2 || count == 3)
                    return true;
                else
                    return false;
            }
            else
            {
                if (count == 3)
                    return true;
                else
                    return false;
            }
        }

        private void FillSimState()
        {
            for (int i = 0; i < _initState.Count; i++)
            {
                List<bool> lBool = _initState[i];
                for (int j = 0; j < lBool.Count; j++)
                {
                    SimState[0, _numOfSteps + 1 + i, _numOfSteps + 1 + j, _numOfSteps + 1, _numOfSteps + 1] = lBool[j];
                }
            }
        }

        private void InitSimState()
        {
            SimState = new bool[_numOfSteps + 1, SimulSize, SimulSize, SimulSize, SimulSize];
        }
    }
}
