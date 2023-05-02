using Microsoft.EntityFrameworkCore;

namespace TopologyProject
{
    public sealed class FeaturesDbContext: DbContext
    {
        public DbSet<FeatureDbModel> Features { get; set; } = null!;

        ~FeaturesDbContext() => Dispose();

        public FeaturesDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();

            Database.EnsureCreated();
        }

        public void Clear() => Features.RemoveRange(Features);
    }
}
