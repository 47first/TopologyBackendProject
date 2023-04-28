using Microsoft.EntityFrameworkCore;

namespace TopologyProject
{
    public sealed class FeaturesDbContext: DbContext
    {
        public DbSet<FeatureModel> Features { get; set; } = null!;

        ~FeaturesDbContext()
        {
            Dispose();
        }

        public FeaturesDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public void FillDataFeatureCollection(FeatureCollection featureCollection)
        {
            foreach (var feature in featureCollection.Features)
                Features.Add(feature.ToFeatureModel());
        }

        public void ClearFeatures()
        {
            foreach (var featureModel in Features)
                Features.Remove(featureModel);
        }
    }
}
