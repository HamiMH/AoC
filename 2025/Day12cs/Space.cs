


using System.Collections.Generic;

namespace Day12cs
{
    internal class Space
    {
        public int Xsize;
        public int Ysize;
        public List<int> Counts;
        public bool[,] Grid;
        public string Line;
        public Space(string line)
        {
            Line = line;
            string[] parts = line.Split(':');

            string[] sizes = parts[0].Trim().Split('x');
            Xsize = int.Parse(sizes[0]);
            Ysize = int.Parse(sizes[1]);

            string[] counts = parts[1].Trim().Split(" ");
            Counts = counts.Select(c => int.Parse(c)).ToList();
            Grid = new bool[Ysize, Xsize];
        }

        public bool CanAddShape(List<string> shape, int x, int y)
        {
            if (x + shape[0].Length > Xsize || y + shape.Count > Ysize)
            {
                return false;
            }
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].Length; j++)
                {
                    if (shape[i][j] == '#')
                    {
                        if (Grid[y + i, x + j])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public void AddShape(List<string> shape, int x, int y)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].Length; j++)
                {
                    if (shape[i][j] == '#')
                    {
                        Grid[y + i, x + j] = true;
                    }
                }
            }
        }
        public void RemoveShape(List<string> shape, int x, int y)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].Length; j++)
                {
                    if (shape[i][j] == '#')
                    {
                        Grid[y + i, x + j] = false;
                    }
                }
            }
        }

        internal long ResolveSpace(List<Shape> shapes)
        {
            List<int> shapesToPlace = new List<int>();
            List<Stack<(int, int)>> placements = new();
            for (int i = 0; i < Counts.Count; i++)
            {

                placements.Add(new Stack<(int, int)>());
                placements[i].Push((0, 0));
                for (int j = 0; j < Counts[i]; j++)
                {
                    shapesToPlace.Add(i);
                }
            }
            if (HeurSpaceTooSmall(shapes))
            {
                return 0;
            }

            return ResolveSpaceRecursive(0, shapesToPlace, shapes, placements) ? 1 : 0;
        }

        private bool ResolveSpaceRecursive(int index, List<int> shapesToPlace, List<Shape> shapes, List<Stack<(int, int)>> placements)
        {
            if (index >= shapesToPlace.Count)
            {
                return true;
            }
            int shapeIndex = shapesToPlace[index];
            Shape shape = shapes[shapeIndex];
            (int startY, int startX) = placements[shapeIndex].Peek();

            for (int y = startY; y < Ysize - 2; y++)
            {
                for (int x = startX; x < Xsize - 2; x++)
                {
                    foreach (var shapeVar in shape.Variants)
                        if (CanAddShape(shapeVar, x, y))
                        {
                            AddShape(shapeVar, x, y);
                            placements[shapeIndex].Push((y, x));
                            if (ResolveSpaceRecursive(index + 1, shapesToPlace, shapes, placements))
                            {
                                return true;
                            }
                            placements[shapeIndex].Pop();
                            RemoveShape(shapeVar, x, y);
                        }
                }
                startX = 0;
            }
            return false;
        }

        internal long ResolveHeuristicSpace(List<Shape> shapes)
        {
            if (HeurSpaceTooSmall(shapes))
                return 1;

            if(HeurFitWithoutFitting(shapes))
                return 2;

            return 0;
        }

        private bool HeurFitWithoutFitting(List<Shape> shapes)
        {
            long totalCount = Counts.Sum();
            return  (Xsize / 3) * (Ysize / 3) >= totalCount;
        }
        private bool HeurSpaceTooSmall(List<Shape> shapes)
        {
            long totalSize = 0;
            for (int i = 0; i < Counts.Count; i++)
            {
                totalSize += shapes[i].Size * Counts[i];
            }
            return totalSize > Xsize * Ysize;
        }

    }
}