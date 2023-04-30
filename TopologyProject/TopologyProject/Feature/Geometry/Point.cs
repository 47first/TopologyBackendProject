namespace TopologyProject
{
    public class Point: Geometry
    {
        public override IEnumerable<Coordinate> Coordinates
        {
            get {
                yield return coordinate;
            }
        }

        public Coordinate coordinate;
    }
}
