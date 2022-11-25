using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public class TreeLayout
    {
        private List<List<bool>> TreeList { get; set; } = new List<List<bool>>();

        private int SizeY { get => TreeList.Count(); }
        private int SizeX  { get => TreeList.First().Count(); }

        public bool GetCoordinate(int x, int y)
        {
            return TreeList[y][x % SizeX];
        }
        public bool IsInGrid(int x, int y)
        {
            if (y>=SizeY)
                return false;
            else
                return true;
        }
        public void AddLine(string line)
        {
            List<bool> boolLine = new List<bool>();
            foreach (char c in line)
            {
                switch (c)
                {
                    case '.':
                        boolLine.Add(false);
                        break;
                    case '#':
                        boolLine.Add(true);
                        break;
                    default:
                        throw new Exception("Fatal Error");
                }
            }
            TreeList.Add(boolLine);
        }
    }
}
