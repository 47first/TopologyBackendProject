﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace TopologyProject
{
    public class Feature
    {
        public string Type { get; set; } = null!;
        public string Id { get; set; } = null!;
        public Dictionary<string, string>? Properties { get; set; }
        [JsonConverter(typeof(GeomentryJsonConverter))]public Geometry Geometry { get; set; } = null!;

        public FeatureModel ToFeatureModel()
        {
            var featureModel = new FeatureModel();

            featureModel.Id = Id;
            featureModel.Json = JsonSerializer.Serialize(this);

            return featureModel;
        }
    }
}
