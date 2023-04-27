using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace TopologyProject
{
    public class FeatureJsonController : Controller
    {
        private const string _featureJsonCacheName = "featureJson";

        private FeaturesDbContext _featuresDb;
        private IMemoryCache _memoryCache;
        private string _featuresJsonPath;

        public FeatureJsonController(FeaturesDbContext featuresDb, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _featuresDb = featuresDb;

            _featuresJsonPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "features.json");
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportFeatures()
        {
            var formFiles = HttpContext.Request.Form.Files;

            Console.WriteLine($"Files: {formFiles.Count()}");

            if (formFiles.Any())
            {
                var jsonFile = formFiles.First();

                if (IsJsonFile(jsonFile))
                {
                    var formFileStream = jsonFile.OpenReadStream();

                    string json = GetStringFromStream(formFileStream);

                    var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);

                    System.IO.File.WriteAllText(_featuresJsonPath, json);

                    return Content("Success");
                }

                return BadRequest();
            }

            else
                return BadRequest();
        }

        private string GetStringFromStream(Stream stream)
        {
            string result = null!;

            using (stream)
            using (StreamReader reader = new(stream))
                result = reader.ReadToEnd();

            return result;
        }

        private bool IsJsonFile(IFormFile file) => file.ContentType == "application/json";

        public async Task<IActionResult> Export()
        {
            byte[] featureJsonBytes = null!;

            if (_memoryCache.TryGetValue(_featureJsonCacheName, out featureJsonBytes) == false)
            {
                WriteJsonInCache();

                featureJsonBytes = _memoryCache.Get<byte[]>(_featureJsonCacheName);
            }

            return File(featureJsonBytes, "application/json", "featuresJson.json");
        }

        private async void WriteJsonInCache()
        {
            Task<byte[]> featuresJson = System.IO.File.ReadAllBytesAsync(_featuresJsonPath);

            _memoryCache.Set(_featureJsonCacheName, await featuresJson, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }
    }
}
