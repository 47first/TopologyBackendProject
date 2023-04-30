namespace TopologyProject
{
    public abstract class Geometry
    {
        public abstract IEnumerable<Coordinate> Coordinates { get; }

        public bool MatchAnyCoordinate(IEnumerable<Coordinate> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                if (Coordinates.Contains(coordinate))
                    return true;
            }

            return false;
        }
    }
}
