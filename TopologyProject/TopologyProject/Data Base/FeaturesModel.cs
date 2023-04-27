using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TopologyProject
{
    [Table("Features")]
    public class FeatureModel
    {
        public string Id { get; set; } = null!;
        public string? Json { get; set; }

        public Feature ToFeature() => JsonSerializer.Deserialize<Feature>(Json);
    }
}
