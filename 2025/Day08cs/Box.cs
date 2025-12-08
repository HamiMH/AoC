using System;
using System.Collections.Generic;
using System.Text;

namespace Day08cs
{
    internal class Box
    {
        public int Id { get; set; }

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public List<int> Neigh { get; set; } = new List<int>();

        public long DistanceSqTo(Box other)
        {
            long dx = this.X - other.X;
            long dy = this.Y - other.Y;
            long dz = this.Z - other.Z;
            return dx * dx + dy * dy + dz * dz;
        }

    }
}
