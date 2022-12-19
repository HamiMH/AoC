using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class StateOfSim : IComparable<StateOfSim>
    {
        public const int NOfStates = 8;
        public int[] states = new int[NOfStates];

        public StateOfSim(int[] ores, int[] oresMiners)
        {
            states[0] = oresMiners[3];
            states[1] = oresMiners[2];
            states[2] = oresMiners[1];
            states[3] = oresMiners[0];
            states[4] = ores[3];
            states[5] = ores[2];
            states[6] = ores[1];
            states[7] = ores[0];
        }

        public static int CompareBeforeAdd(StateOfSim a, StateOfSim b)
        {
            for (int i = 0; i < NOfStates; i++)
            {
                if (a.states[i] > b.states[i])
                    return -1;
            }

            return 0;
        }



        public int CompareTo(StateOfSim? x)
        {
            for (int i = 0; i < NOfStates; i++)
            {
                if (x.states[i] > this.states[i])
                    return -1;
            }
            return 0;
        }

        internal bool IsLessThen(StateOfSim inSS)
        {
            bool isLessThen = false;
            for (int i = 0; i < NOfStates; i++)
            {
                if (this.states[i] < inSS.states[i])
                    isLessThen = true;
                if (this.states[i] > inSS.states[i])
                    return false;
            }
            return isLessThen;
        }
    }
}
