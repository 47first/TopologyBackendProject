using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using IoFile = System.IO.File;

namespace TopologyProject
{
    [Route("Features")]
    public class FeaturesImportController : JsonFeaturesController
    {
        public FeaturesImportController(IMemoryCache memoryCache) : base(memoryCache) { }

        [HttpGet]
        [Route("Import")]
        public IActionResult SendImportView() => View();

        [HttpPost]
        public IActionResult Import()
        {
            if (TryGetFileFromRequest(HttpContext, out IFormFile? file) == false)
                return BadRequest();

            string fileContent = GetStringFromFile(file);

            if(TryDeserializeJson(fileContent, out FeatureCollection featureCollection) == false)
                return BadRequest();

            IoFile.WriteAllText(featuresJsonPath, fileContent);

            WriteJsonToCache(fileContent);

            return Ok();
        }

        private bool TryGetFileFromRequest(HttpContext context, out IFormFile? file)
        {
            file = null;

            var formFiles = context.Request.Form.Files;

            if (formFiles.Any())
                file = formFiles.First();

            return file is IFormFile;
        }

        private string GetStringFromFile(IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            using (StreamReader reader = new(stream))
                return reader.ReadToEnd();
        }

        private bool TryDeserializeJson<T>(string json, out T deserializedObject)
        {
            try
            {
                deserializedObject = JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                deserializedObject = default;
            }

            return deserializedObject == default;
        }
    }
}
