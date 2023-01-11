using Microsoft.AspNetCore.Mvc;

namespace cfr_backend.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
