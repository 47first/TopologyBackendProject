using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using TopologyProject.Topology;
using System.Text.Json;
using IoFile = System.IO.File;

namespace TopologyProject
{
    [Produces("application/json")]
    [Route("Features")]
    public class FeaturesImportController : FeaturesJsonController
    {
        private string _defaultFeaturesJsonPath;
        private FeaturesDbContext _featuresDb;

        public FeaturesImportController(IMemoryCache memoryCache, FeaturesDbContext featuresDb) : base(memoryCache)
        {
            _defaultFeaturesJsonPath = Path.Combine(Environment.CurrentDirectory, "Server Data", "defaultFeatures.json");
            _featuresDb = featuresDb;
        }

        /// <summary>
        /// Set default topology data
        /// </summary>
        /// <remarks>
        /// Use this method to reset topology data
        /// </remarks>
        /// <returns>
        /// Status code
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid data in default json file</response>
        [HttpGet]
        [Route("SetDefault")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetDefault() => ImportFromLocalFile(_defaultFeaturesJsonPath);

        private IActionResult ImportFromLocalFile(string path)
        {
            var fileContent = IoFile.ReadAllText(path);

            return Import(fileContent);
        }

        private IActionResult Import(string json)
        {
            if (TryDeserializeJson(json, out FeatureCollection? featureCollection) == false ||
                featureCollection.Features == null)
                return BadRequest();

            ResetCacheValue();

            var features = featureCollection.Features;

            GroupFeatures(features);

            WriteFeaturesInDb(features);
            WriteFeaturesInFile(JsonSerializer.Serialize(featureCollection));

            return Ok();
        }

        private void GroupFeatures(IEnumerable<Feature> features)
        {
            var distributor = new GroupDistributor();

            distributor.DistributeByGroups(features);
        }

        private void WriteFeaturesInDb(IEnumerable<Feature> features)
        {
            _featuresDb.Clear();

            foreach (var feature in features)
            {
                var featureModel = feature.ToFeatureModel();

                _featuresDb.Features.Add(featureModel);
            }

            _featuresDb.SaveChanges();
        }

        private bool TryDeserializeJson<T>(string json, out T? deserializedObject)
        {
            deserializedObject = default;

            try
            {
                deserializedObject = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteFeaturesInFile(string json) => IoFile.WriteAllText(featuresJsonPath, json);

        private void ResetCacheValue() => MemoryCache.Remove(jsonBytesKey);

        /// <summary>
        /// Send form with file
        /// </summary>
        /// <returns>
        /// Form
        /// </returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("Import")]
        public IActionResult SendImportForm() => View("ImportForm");

        /// <summary>
        /// Receive user file from form and import data to DB and json file
        /// </summary>
        /// <returns>
        /// Status code
        /// </returns>
        /// <response code="200">Success import</response>
        /// <response code="400">Invalid data in json file</response>
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
