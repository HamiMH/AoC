using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class ActiveReac
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }

        public ActiveReac(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        //public bool Equals(ActiveReac x, ActiveReac y)
        //{
        //    return x.X == y.X && x.Y == y.Y && x.Z == y.Z && x.W == y.W;
        //}

        public override bool Equals(object? obj)
        {
            return Equals1(this, (ActiveReac)obj);
        }
        public static bool Equals1(ActiveReac x, ActiveReac y)
        {
            return x.X == y.X && x.Y == y.Y && x.Z == y.Z && x.W == y.W;
        }

        public int GetHashCode(ActiveReac x)
        {
            return (x.X + 10) + (x.Y + 10) * 100 + (x.Z + 10) * 1000 + (x.W + 10) * 10000;
        }

        public override int GetHashCode()
        {
            return (this.X + 10) + (this.Y + 10) * 100 + (this.Z + 10) * 1000 + (this.W + 10) * 10000;
        }

    }
}
