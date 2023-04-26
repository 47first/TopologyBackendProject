using System.Text.Json.Serialization;

namespace TopologyProject
{
    public partial class GeomentryJsonConverter : JsonConverter<Geometry>
    {
        public override bool CanConvert(Type typeToConvert) => typeof(Geometry).IsAssignableFrom(typeToConvert);
    }
}
