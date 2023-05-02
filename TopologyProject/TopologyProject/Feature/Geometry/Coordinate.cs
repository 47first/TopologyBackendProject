using System;

namespace TopologyProject
{
    public struct Coordinate
    {
        public double x, y;

        public Coordinate(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public string GetSimplified() => $"({x:0.00}:{y:0.00})";

        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return a.x != b.x || a.y != b.y;
        }
    }
}
