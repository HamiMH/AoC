using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    internal class ProgramRunner
    {
        private List<CommBase> _commList = new List<CommBase>();
        private CommBase[] _comms;

        public void AddComm(string str)
        {
            _commList.Add(CommBase.Create(str));
        }
        public void InputOver()
        {
            _comms=_commList.ToArray();
        }
        private void ResetAttendArr(CommBase[] commArr)
        {
            foreach (CommBase commBase in commArr)
            {
                commBase.Attended = false;
            }
        }
        public int ReparingExec()
        {
            int accum;
            int len = _comms.Count();
            for (int i = 0; i < len; i++)
            {
                ResetAttend();
                if (_comms[i].FastSwap(ref _comms[i]))
                {
                    accum = ExecProgram();
                    if (accum != 0)
                        return accum;
                    _comms[i].FastSwap(ref _comms[i]);
                }
            }
            //for (int i = 0; i < _comms.Count; i++)
            //{
            //    ResetAttend();
            //    if (_comms[i].Swappable())
            //    {
            //        _comms[i] = _comms[i].Swap();
            //        accum = ExecProgram();
            //        if (accum != 0)
            //            return accum;
            //        _comms[i] = _comms[i].Swap();
            //    }
            //}
            return 0;
        }

        private void ResetAttend()
        {
            foreach (CommBase commBase in _comms)
            {
                commBase.Attended = false;
            }
        }

        public int ExecProgram()
        {
            SimState simState = new SimState();
            int len = _comms.Count();
            while (true)
            {
                if (simState.Position == len)
                    return simState.Accum;
                if (_comms[simState.Position].ExecComm(simState) == false)
                    break;
            }
            return 0;
        }
    }
}
