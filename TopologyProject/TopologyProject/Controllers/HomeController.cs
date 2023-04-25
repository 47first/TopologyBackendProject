using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;

namespace TopologyProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult GetJson()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "features.json");

            Console.WriteLine(path);

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(System.IO.File.Open(path, FileMode.Open));

            Console.WriteLine($"------------------- {featureCollection.features.Where(feature => feature.type == "Feature").Count()}");

            return Json(featureCollection);
        }
    }
}