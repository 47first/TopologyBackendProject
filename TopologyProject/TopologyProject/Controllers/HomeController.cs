using Microsoft.AspNetCore.Mvc;

namespace TopologyProject.Controllers
{
    public class HomeController : Controller
    {
        private FeaturesDbContext _db;
        public HomeController(FeaturesDbContext db)
        {
            _db = db;
        }

        public IActionResult GetJson()
        {
            _db.Features.Add(new() { Id = "123", Json = "{ \"name\" = \"Bebra\" }" });

            _db.SaveChanges();

            return Content(" ");
        }
    }
}