using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection.PortableExecutable;

namespace TopologyProject
{
    public class GeomentryJsonConverter : JsonConverter<IGeometry>
    {
        public override bool CanConvert(Type typeToConvert) => typeof(IGeometry).IsAssignableFrom(typeToConvert);

        public override IGeometry? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string geometryType = GetGeometryType(ref reader);
            IGeometry geometry = GetGeometryByString(geometryType);

            FillGeometryData(ref reader, geometry);

            return geometry;
        }

        private string GetGeometryType(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            string? propertyName = reader.GetString();
            if (propertyName != "type")
                throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            return reader.GetString();
        }

        private IGeometry GetGeometryByString(string geometryType)
        {
            return geometryType switch
            {
                "LineString" => new LineString(),
                "Point" => new Point(),
                _ => throw new JsonException()
            };
        }

        private void FillGeometryData(ref Utf8JsonReader reader, IGeometry geometry)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();

                    if (propertyName != "coordinates")
                        throw new JsonException();

                    reader.Read();

                    if (geometry is Point point)
                        FillPoint(ref reader, point);

                    else if (geometry is LineString lineString)
                        FillLineString(ref reader, lineString);
                }
            }

            throw new JsonException();
        }

        private void FillLineString(ref Utf8JsonReader reader, LineString lineString)
        {
            if(reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    return;
            }
        }

        private void FillPoint(ref Utf8JsonReader reader, Point point)
        {

        }

        public override void Write(Utf8JsonWriter writer, IGeometry value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
