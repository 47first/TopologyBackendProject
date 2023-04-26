using System.Text.Json.Serialization;

namespace TopologyProject
{
    public class Feature
    {
        public string type { get; set; }
        public string id { get; set; }
        public Dictionary<string, string> properties { get; set; }
        [JsonConverter(typeof(GeomentryJsonConverter))]public Geometry geometry { get; set; }
    }
}
