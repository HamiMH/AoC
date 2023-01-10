using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class State
    {
        public static int NOfStates = 8;
        public int[] Robots = new int[BluePrint.NOfGeodes];
        public int[] Material = new int[BluePrint.NOfGeodes];



        public State(State oldState)
        {
            Material[0] = oldState.Material[0];
            Material[1] = oldState.Material[1];
            Material[2] = oldState.Material[2];
            Material[3] = oldState.Material[3];
            Robots[0] = oldState.Robots[0];
            Robots[1] = oldState.Robots[1];
            Robots[2] = oldState.Robots[2];
            Robots[3] = oldState.Robots[3];
        }

        public State(int[] ores, int[] robots)
        {
            Material[0] = ores[0];
            Material[1] = ores[1];
            Material[2] = ores[2];
            Material[3] = ores[3];
            Robots[0] = robots[0];
            Robots[1] = robots[1];
            Robots[2] = robots[2];
            Robots[3] = robots[3];
        }

        public override int GetHashCode()
        {
            return (Material[0] + "," + Material[1] + "," + Material[2] + "," + Material[3] + "," + Robots[0] + "," + Robots[1] + "," + Robots[2] + "," + Robots[3]).GetHashCode();
        }

        public override string ToString()
        {
            return "R(" + Robots[0] + ", " + Robots[1] + ", " + Robots[2] + ", " + Robots[3] + "); " + "M(" + Material[0] + ", " + Material[1] + ", " + Material[2] + ", " + Material[3] + ")";
        }
        public int MaterialHash()
        {
            return  Material[0] |  ((Material[1])<<8) | ((Material[2]) << 16) |  ((Material[3]) << 24);
            //return  Material[0] + 100* Material[1] + 10000 *Material[2] + 1000000* Material[3] ;
        }

        internal State Build(int type, int nofMoves)
        {
            State newState = new State(this);
            for (int i = 0; i < 4; i++)
                newState.Material[i] = newState.Material[i] + newState.Robots[i] * nofMoves - BluePrint.Cost[type, i];

            newState.Robots[type]++;

            return newState;
        }
    }
}