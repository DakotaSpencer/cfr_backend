using CFRDal;
using CFRDal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Web.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace cfr_backend.Controllers
{
    public class ApiController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IManager _dal;
        public ApiController(IManager dal)
        {
            _dal = dal;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("movie/{id:int}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult GetMovie(int id)
        {
            var movie = _dal.GetMovie(id);
            return Json(movie);
        }

        [Route("movie/search/{query}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult SearchMovies(string query)
        {
            var movies = _dal.SearchMovies(query);
            return Json(movies);
        }
    }
}
