using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopologyProject
{
    public sealed class FeaturesDbContext: DbContext
    {
        public DbSet<FeaturesModel> Features { get; set; } = null!;
        public FeaturesDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }

    [Table("Features")]
    public class FeaturesModel
    {
        public string Id { get; set; }
        public string? Json { get; set; }
    }
}
