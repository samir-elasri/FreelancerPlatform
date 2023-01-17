using Microsoft.AspNetCore.Mvc;

namespace FreelancerPlatform.Controllers
{
    public class Request : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
