namespace TopologyProject
{
    public abstract class Geometry
    {
        public abstract IEnumerable<Coordinate> Coordinates { get; }

        public bool MatchAnyCoordinate(IEnumerable<Coordinate> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                if (Coordinates.Any(coordinated => coordinated == coordinate))
                {
                    Console.Write($"+");
                    return true;
                }

                Console.Write($"-");
            }

            return false;
        }
    }
}
