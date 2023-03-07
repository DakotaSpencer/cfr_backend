using CFRDal;
using CFRDal.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Web.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace cfr_backend.Controllers
{
    [EnableCors("AllowFrontend")]
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

        [Route("movie/{id}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult GetMovie(int id)
        {
            try {
                var movie = _dal.GetMovie(id);            
                return Json(movie);
            } catch (Exception e)
            {
                return Json("Exception getting movie: " + e);
            }
        }

        [Route("movie/search/{query}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult SearchMovies(string query)
        {
            var movies = _dal.SearchMovies(query);
            return Json(movies);
        }

        [Route("movie/{id}/similar")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult GetSimilarMovies(int id)
        {
            try {
                var movies = _dal.GetSimilarMovies(id);
                return Json(movies);
            } catch (Exception e)
            {
                return Json("Exception getting similar movies: " + e);
            }
        }

        [Route("movie/{id}/reviews")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult GetReviewsForMovie(int id)
        {
            List<ReviewData> reviews = _dal.GetReviewsForMovie(id);
            return Json(reviews);
        }
    }
}
