using Microsoft.EntityFrameworkCore;

namespace TopologyProject
{
    public sealed class FeaturesDbContext: DbContext
    {
        public DbSet<FeatureDbModel> Features { get; set; } = null!;

        ~FeaturesDbContext() => Dispose();

        public FeaturesDbContext(DbContextOptions options) : base(options) => Database.EnsureCreated();

        public void ClearFeatures()
        {
            foreach (var featureModel in Features)
                Features.Remove(featureModel);
        }
    }
}
