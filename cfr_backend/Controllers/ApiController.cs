using Microsoft.AspNetCore.Mvc;

namespace cfr_backend.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
