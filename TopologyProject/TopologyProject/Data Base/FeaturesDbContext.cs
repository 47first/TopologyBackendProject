using Microsoft.EntityFrameworkCore;

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
}
