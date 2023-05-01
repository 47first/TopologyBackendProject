using System.Text.Json;
using System.Text.Json.Serialization;

namespace TopologyProject
{
    public partial class GeomentryJsonConverter : JsonConverter<Geometry>
    {
        public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.GetType().Name);

            WriteCoordinates(writer, value);

            writer.WriteEndObject();
        }

        private void WriteCoordinates(Utf8JsonWriter writer, Geometry geometry)
        {
            writer.WritePropertyName("coordinates");

            if (geometry is Point point)
                WritePointCoordinates(writer, point);

            else if (geometry is LineString lineString)
                WriteLineStringCoordinates(writer, lineString);

            else
                throw new JsonException();
        }

        private void WritePointCoordinates(Utf8JsonWriter writer, Point point)
        {
            WriteCoordinate(writer, point.coordinate);
        }

        private void WriteLineStringCoordinates(Utf8JsonWriter writer, LineString lineString)
        {
            writer.WriteStartArray();

            foreach (var coordinate in lineString.points)
                WriteCoordinate(writer, coordinate);

            writer.WriteEndArray();
        }

        private void WriteCoordinate(Utf8JsonWriter writer, Coordinate coordinates)
        {
            writer.WriteStartArray();

            writer.WriteNumberValue(coordinates.x);
            writer.WriteNumberValue(coordinates.y);

            writer.WriteEndArray();
        }
    }
}
