using System.Text.Json.Serialization;

namespace TopologyProject
{
    public class Feature
    {
        public string Type { get; set; } = null!;
        public string Id { get; set; } = null!;
        public Dictionary<string, string>? Properties { get; set; }
        [JsonConverter(typeof(GeomentryJsonConverter))]public Geometry Geometry { get; set; } = null!;
    }
}
