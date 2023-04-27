using System.ComponentModel.DataAnnotations.Schema;

namespace TopologyProject
{
    [Table("Features")]
    public class FeaturesModel
    {
        public string Id { get; set; } = null!;
        public string? Json { get; set; }
    }
}
