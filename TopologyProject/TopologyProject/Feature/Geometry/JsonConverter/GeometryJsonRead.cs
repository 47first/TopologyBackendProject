using System.Text.Json;
using System.Text.Json.Serialization;

namespace TopologyProject
{
    public partial class GeomentryJsonConverter : JsonConverter<Geometry>
    {
        public override Geometry? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? geometryType = GetGeometryStringType(ref reader);
            Geometry geometry = GetGeometryByString(geometryType);

            FillGeometryData(ref reader, geometry);

            return geometry;
        }

        private string? GetGeometryStringType(ref Utf8JsonReader reader)
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

        private Geometry GetGeometryByString(string? geometryType)
        {
            return geometryType switch
            {
                "LineString" => new LineString(),
                "Point" => new Point(),
                _ => throw new JsonException()
            };
        }

        private void FillGeometryData(ref Utf8JsonReader reader, Geometry geometry)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? propertyName = reader.GetString();

                    reader.Read();
                    if (propertyName == "coordinates")
                        FillCoordinates(ref reader, geometry);
                }
            }

            throw new JsonException();
        }

        private void FillCoordinates(ref Utf8JsonReader reader, Geometry geometry)
        {
            if (geometry is Point point)
                FillPointCoordinates(ref reader, point);

            else if (geometry is LineString lineString)
                FillLineStringCoordinates(ref reader, lineString);

            else
                throw new JsonException();
        }

        private void FillLineStringCoordinates(ref Utf8JsonReader reader, LineString lineString)
        {
            lineString.from = GetCoordinates(ref reader);

            lineString.to = GetCoordinates(ref reader);
        }

        private Coordinates GetCoordinates(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            Coordinates result = new();

            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException();

            result.x = reader.GetDouble();

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException();

            result.y = reader.GetDouble();

            reader.Read();
            if(reader.TokenType != JsonTokenType.EndArray)
                throw new JsonException();

            return result;
        }

        private void FillPointCoordinates(ref Utf8JsonReader reader, Point point)
        {
            point.point = GetCoordinates(ref reader);
        }
    }
}
