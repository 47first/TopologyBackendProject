using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace TopologyProject
{
    public class FeaturesJsonController : Controller
    {
        private IMemoryCache _memoryCache;

        public FeaturesJsonController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Export()
        {
            byte[] featureJsonBytes = null!;

            if (_memoryCache.TryGetValue(_featureJsonCacheName, out featureJsonBytes) == false)
            {
                WriteJsonFromFileInCache(_featureJsonCacheName);

                featureJsonBytes = _memoryCache.Get<byte[]>(_featureJsonCacheName);
            }

            return File(featureJsonBytes, "application/json", "featuresJson.json");
        }

        private async void WriteJsonFromFileInCache(string cacheName)
        {
            Task<byte[]> featuresJson = System.IO.File.ReadAllBytesAsync(_featuresJsonPath);

            _memoryCache.Set(cacheName, await featuresJson, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }
    }
}
