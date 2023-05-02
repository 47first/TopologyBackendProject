using Microsoft.AspNetCore.Mvc;

namespace TopologyProject
{
    [Route("Features")]
    public class FeaturesGetterController : Controller
    {
        private FeaturesDbContext _featuresDb;
        public FeaturesGetterController(FeaturesDbContext db)
        {
            _featuresDb = db;
        }

        [Route("All")]
        public IActionResult GetAllFeatures()
        {
            var allFeatureModels = _featuresDb.Features;

            return FeatureCollection(allFeatureModels);
        }

        [Route("Group")]
        public IActionResult GetFeaturesByGroup(int id)
        {
            var groupedFeatureModels = _featuresDb.Features.Where
                (feature => feature.GroupId == id);

            return FeatureCollection(groupedFeatureModels);
        }

        private IActionResult FeatureCollection(IEnumerable<FeatureDbModel> featureModels)
        {
            var featureCollection = GetFeatureCollection(featureModels);

            return FeatureCollection(featureCollection);
        }

        private FeatureCollection GetFeatureCollection(IEnumerable<FeatureDbModel> featureModels)
        {
            var featureCollection = new FeatureCollection() { Features = new() };

            foreach (var featureModel in featureModels)
            {
                var feature = featureModel.ToFeature();

                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }

        private IActionResult FeatureCollection(FeatureCollection featureCollection) => Json(featureCollection);
    }
}
