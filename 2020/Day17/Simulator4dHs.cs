using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Simulator4dHs
    {
        private int InitSize { get => _initState.Count; }

        private List<List<bool>> _initState = new List<List<bool>>();
        //private int SimulSize { get => InitSize + 2 * _numOfSteps + 4; }



        private List<HashSet<ActiveReac>> SimState { get; set; } = new List<HashSet<ActiveReac>>();

        private int _numOfSteps;

        public Simulator4dHs(int numOfSt, List<string> initState)
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
            HashSet<ActiveReac> final = SimState[_numOfSteps];
            return final.Count;
        }
        public bool Simulate()
        {
            InitSimState();
            FillSimState();


            HashSet<ActiveReac> workking;
            HashSet<ActiveReac> attended = new HashSet<ActiveReac>();



            for (int nOfSim = 1; nOfSim <= _numOfSteps; nOfSim++)
            {
                workking = SimState[nOfSim - 1];
                attended.Clear();

                foreach (ActiveReac activeReac in workking)
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            for (int k = -1; k <= 1; k++)
                                for (int l = -1; l <= 1; l++)
                                {
                                    ActiveReac ar = new ActiveReac(activeReac.X + i, activeReac.Y + j, activeReac.Z + k, activeReac.W + l);
                                    if (attended.Contains(ar))
                                        continue;
                                    attended.Add(ar);
                                    ResultOfNeib(nOfSim, activeReac.X + i, activeReac.Y + j, activeReac.Z + k, activeReac.W + l);
                                }
                }
            }

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
        private void ResultOfNeib(int step, int ii, int jj, int kk, int ll)
        {
            HashSet<ActiveReac> workking = SimState[step - 1];
            int das;
            int count = 0;
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    for (int k = -1; k <= 1; k++)
                        for (int l = -1; l <= 1; l++)
                        {
                            if (i == 0 && j == 0 && k == 0 && l == 0)
                                continue;
                            if (ii + i == 0 && jj + j == 1 && kk + k == 0 && ll + l == 0)
                                 das= 0;
                            if (workking.Contains(new ActiveReac(ii + i, jj + j, kk + k, ll + l)))
                                count++;
                        }
            if (workking.Contains(new ActiveReac(ii, jj, kk, ll)))
            {
                if (count == 2 || count == 3)
                    SimState[step].Add(new ActiveReac(ii, jj, kk, ll));
            }
            else
            {
                if (count == 3)
                    SimState[step].Add(new ActiveReac(ii, jj, kk, ll));
            }
            //if (workking.Contains(new ActiveReac(ii, jj, kk, ll)))
            //{
            //    if (count == 2 || count == 3)
            //        return true;
            //    else
            //        return false;
            //}
            //else
            //{
            //    if (count == 3)
            //        return true;
            //    else
            //        return false;
            //}
        }

        private void FillSimState()
        {
            HashSet<ActiveReac> init = SimState[0];
            for (int i = 0; i < _initState.Count; i++)
            {
                List<bool> lBool = _initState[i];
                for (int j = 0; j < lBool.Count; j++)
                {
                    if (lBool[j])
                        init.Add(new ActiveReac(i, j, 0, 0));
                }
            }
        }

        private void InitSimState()
        {
            for (int i = 0; i <= _numOfSteps; i++)
                SimState.Add(new HashSet<ActiveReac>());

            // SimState = new bool[_numOfSteps + 1, SimulSize, SimulSize, SimulSize, SimulSize];
        }
    }
}
