using System.Text.Json;
using System.Text.Json.Serialization;

namespace TopologyProject
{
    [Serializable]
    public class Feature
    {
        public static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public string Type { get; set; } = null!;
        public string Id { get; set; } = null!;
        public int? GroupId { get; set; }
        public Dictionary<string, string>? Properties { get; set; }
        [JsonConverter(typeof(GeomentryJsonConverter))]public Geometry Geometry { get; set; } = null!;

        public FeatureDbModel ToFeatureModel()
        {
            var featureModel = new FeatureDbModel();

            featureModel.Id = Id;
            featureModel.GroupId = GroupId;
            featureModel.Json = JsonSerializer.Serialize(this, SerializerOptions);

            return featureModel;
        }
    }
}
