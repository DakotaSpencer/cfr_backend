using Microsoft.AspNetCore.Mvc;
using CFRDal;
using CFRDal.Models;

namespace cfr_backend.Controllers
{
    public class UserController : Controller
    {
        private readonly IManager _dal;
        public UserController(IManager dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUser([FromBody] User user)
        {
            var userId = _dal.CreateUser(user);
            return Json(userId);
        }

        [HttpDelete]
        public JsonResult DeleteUser(string id)
        {
            var success = _dal.DeleteUser(id);
            return Json(success);
        }

        [HttpPost]
        public JsonResult CreateReview([FromBody] Review review)
        {
            var reviewId = _dal.CreateReview(review);
            return Json(reviewId);
        }

        [HttpPost]
        public JsonResult Login([FromBody] LoginRequest loginRequest)
        {
            var userId = _dal.AuthenticateUser(loginRequest);
            return Json(userId);
        }

        [HttpPost]
        public JsonResult UpdateUser([FromBody] User user)
        {
            var userId = _dal.UpdateUser(user);
            return Json(userId);
        }

        [HttpGet]
        public JsonResult GetReviewsForMovie(int movieId)
        {
            List<Review> reviews = _dal.GetReviewsForMovie(movieId);
            return Json(reviews);
        }
    }
}
