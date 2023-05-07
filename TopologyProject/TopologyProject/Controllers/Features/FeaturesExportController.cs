using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using IoFile = System.IO.File;

namespace TopologyProject.Controllers.Feature
{
    [Route("Features")]
    public sealed class FeaturesExportController : FeaturesJsonController
    {
        public FeaturesExportController(IMemoryCache memoryCache) : base(memoryCache) { }

        /// <summary>
        /// Export json topology data
        /// </summary>
        /// <returns>
        /// Json file
        /// </returns>
        /// <response code="200">File received</response>
        /// <response code="400">Invalid json data format</response>
        [HttpGet]
        [Route("Export")]
        public IActionResult Export()
        {
            byte[]? featureJsonBytes = GetOrCreateBytesInCache();

            if (featureJsonBytes is null)
                return BadRequest();

            return File(featureJsonBytes, "application/json"/*, "featuresJson.json"*/);
        }

        private byte[]? GetOrCreateBytesInCache()
        {
            if (MemoryCache.TryGetValue(jsonBytesKey, out byte[]? result) == false)
            {
                result = IoFile.ReadAllBytes(featuresJsonPath);

                MemoryCache.Set(jsonBytesKey, result);
            }

            return result;
        }
    }
}
