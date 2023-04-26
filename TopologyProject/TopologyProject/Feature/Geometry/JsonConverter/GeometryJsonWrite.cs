using System.Text.Json;
using System.Text.Json.Serialization;

namespace TopologyProject
{
    public partial class GeomentryJsonConverter : JsonConverter<Geometry>
    {
        public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
