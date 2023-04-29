using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace TopologyProject
{
    public abstract class JsonFeaturesController : Controller
    {
        protected const string cacheJsonKey = "";

        protected string defaultFeaturesJsonPath;
        protected string featuresJsonPath;

        protected IMemoryCache MemoryCache { get; private set; }

        public JsonFeaturesController(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;

            defaultFeaturesJsonPath = Path.Combine(Environment.CurrentDirectory, "Server Only Data", "defaultFeatures.json");
            featuresJsonPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "features.json");
        }

        protected void WriteJsonToCache(string json)
        {
            MemoryCache.Set(cacheJsonKey, json, TimeSpan.FromMinutes(5));
        }
    }
}
