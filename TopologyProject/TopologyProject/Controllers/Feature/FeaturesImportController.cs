using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using IoFile = System.IO.File;

namespace TopologyProject
{
    [Route("Features")]
    public class FeaturesImportController : FeaturesJsonController
    {
        private string _defaultFeaturesJsonPath;

        public FeaturesImportController(IMemoryCache memoryCache) : base(memoryCache)
        {
            _defaultFeaturesJsonPath = Path.Combine(Environment.CurrentDirectory, "Server Only Data", "defaultFeatures.json");
        }

        [HttpGet]
        [Route("SetDefault")]
        public IActionResult SetDefault() => ImportFromLocalFile(_defaultFeaturesJsonPath);

        public IActionResult ImportFromLocalFile(string path)
        {
            var fileContent = IoFile.ReadAllText(path);

            return Import(fileContent);
        }

        private IActionResult Import(string json)
        {
            if (TryDeserializeJson(json, out FeatureCollection? featureCollection) == false)
                return BadRequest();

            WriteFeaturesInJson(json);
            ResetCacheValue();

            return Ok();
        }

        private bool TryDeserializeJson<T>(string json, out T? deserializedObject)
        {
            deserializedObject = default;

            try
            {
                deserializedObject = JsonSerializer.Deserialize<T>(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteFeaturesInJson(string json) => IoFile.WriteAllText(featuresJsonPath, json);

        private void ResetCacheValue() => MemoryCache.Remove(jsonBytesKey);

        [HttpGet]
        [Route("Import")]
        public IActionResult SendImportForm() => View("ImportForm");

        [HttpPost]
        [Route("Import")]
        public IActionResult ImportFromFormFile()
        {
            if (TryGetFileFromRequest(HttpContext, out IFormFile? file) == false)
                return BadRequest();

            string fileContent = GetStringFromFormFile(file);

            return Import(fileContent);
        }

        private bool TryGetFileFromRequest(HttpContext context, out IFormFile? file)
        {
            file = null;

            var formFiles = context.Request.Form.Files;

            if (formFiles.Any())
                file = formFiles.First();

            return file is IFormFile;
        }

        private string GetStringFromFormFile(IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            using (StreamReader reader = new(stream))
                return reader.ReadToEnd();
        }
    }
}
