using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace TopologyProject
{
    public abstract class FeaturesJsonController : Controller
    {
        protected const string jsonBytesKey = "featureCollectionBytes";

        protected string featuresJsonPath;

        protected IMemoryCache MemoryCache { get; private set; }

        public FeaturesJsonController(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;

            featuresJsonPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "features.json");
        }
    }
}
