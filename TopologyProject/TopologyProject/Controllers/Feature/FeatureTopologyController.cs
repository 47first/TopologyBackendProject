using Microsoft.AspNetCore.Mvc;

namespace TopologyProject
{
    [Route("Features")]
    public class FeatureTopologyController : Controller
    {
        private FeaturesDbContext _featuresDb;
        public FeatureTopologyController(FeaturesDbContext db)
        {
            _featuresDb = db;
        }

        [Route("All")]
        public IActionResult GetAllFeatures()
        {
            var featureCollection = GetFeatureCollection();

            return FeatureCollection(featureCollection);
        }

        private FeatureCollection GetFeatureCollection()
        {
            var featureCollection = new FeatureCollection();

            foreach (var featureModel in _featuresDb.Features)
                featureCollection.Features.Add(featureModel.ToFeature());

            return featureCollection;
        }

        private IActionResult FeatureCollection(FeatureCollection featureCollection) => Json(featureCollection);
    }
}
