namespace TopologyProject
{
    public class LineString : Geometry
    {
        public override IEnumerable<Coordinate> Coordinates => lines;

        public readonly List<Coordinate> lines = new();
    }
}
