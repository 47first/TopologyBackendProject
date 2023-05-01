namespace TopologyProject
{
    public class LineString : Geometry
    {
        public override IEnumerable<Coordinate> Coordinates => points;

        public readonly List<Coordinate> points = new();
    }
}
