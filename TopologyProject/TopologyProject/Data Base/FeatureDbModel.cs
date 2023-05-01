using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TopologyProject
{
    [Table("Features")]
    public class FeatureDbModel
    {
        public string Id { get; set; } = null!;
        public int? GroupId { get; set; }
        public string? Json { get; set; }

        public Feature ToFeature() => JsonSerializer.Deserialize<Feature>(Json);
    }
}
