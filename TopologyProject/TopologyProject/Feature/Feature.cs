namespace TopologyProject
{
    public class Feature
    {
        public string type { get; set; }
        public string id { get; set; }
        public Dictionary<string, string> properties { get; set; }
        public IGeometry geometry { get; set; }
    }
}
