using Microsoft.AspNetCore.Mvc;

namespace TopologyProject
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Home");
        }
    }
}
