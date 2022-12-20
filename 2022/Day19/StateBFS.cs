using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class StateBFS
    {
        public static int NOfStates = 8;
        public int[] Robots = new int[BluePrint.NOfGeodes];
        public int[] Material = new int[BluePrint.NOfGeodes];

        public StateBFS? Prev = null;


        public StateBFS(StateBFS oldState)
        {
            Material[0] = oldState.Material[0];
            Material[1] = oldState.Material[1];
            Material[2] = oldState.Material[2];
            Material[3] = oldState.Material[3];
            Robots[0] = oldState.Robots[0];
            Robots[1] = oldState.Robots[1];
            Robots[2] = oldState.Robots[2];
            Robots[3] = oldState.Robots[3];
            Prev = oldState;
        }

        public StateBFS(int[] ores, int[] robots)
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

        internal int GetHeur()
        {
            int toRet = 0;
            toRet+= Material[0];
            toRet+=2* Material[1];
            toRet+=10* Material[2];
            toRet+=200* Material[3];
            toRet+=3*Robots[0];
            toRet+=7*Robots[1];
            toRet+=100*Robots[2];
            toRet+=1000*Robots[3];
            return toRet;
        }

        public override int GetHashCode()
        {
            return (Material[0] + "," + Material[1] + "," + Material[2] + "," + Material[3] + "," + Robots[0] + "," + Robots[1] + "," + Robots[2] + "," + Robots[3]).GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            StateBFS oldState = (StateBFS)obj;


;            return Material[0] == oldState.Material[0]&&
            Material[1] == oldState.Material[1] &&
            Material[2] == oldState.Material[2] &&
            Material[3] == oldState.Material[3] &&
            Robots[0] == oldState.Robots[0] &&
            Robots[1] == oldState.Robots[1] &&
            Robots[2] == oldState.Robots[2] &&
            Robots[3] == oldState.Robots[3];
        }

        public void PrintHistory(ref int ind)
        {
            if(Prev!=null)
                Prev.PrintHistory(ref ind);
            Console.WriteLine(ind+": "+this.ToString());
            ind++;
        }

        public override string ToString()
        {
            return "R(" + Robots[0] + ", " + Robots[1] + ", " + Robots[2] + ", " + Robots[3] + "); "+ "M(" + Material[0] + ", " + Material[1] + ", " + Material[2] + ", " + Material[3] + ")";
        }
    }
}
