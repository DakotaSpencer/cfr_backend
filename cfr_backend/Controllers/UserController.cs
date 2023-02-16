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

        [Route("createuser")]
        [HttpPost]
        public JsonResult CreateUser([FromBody] User user)
        {
            var userId = _dal.CreateUser(user);
            return Json(userId);
        }

        [Route("user/{id}")]
        [HttpDelete]
        public JsonResult DeleteUser(string id)
        {
            var success = _dal.DeleteUser(id);
            return Json(success);
        }

        [Route("createreview")]
        [HttpPost]
        public JsonResult CreateReview([FromBody] Review review)
        {
            var reviewId = _dal.CreateReview(review);
            return Json(reviewId);
        }

        [Route("login")]
        [HttpPost]
        public JsonResult Login([FromBody] LoginRequest loginRequest)
        {
            var userId = _dal.AuthenticateUser(loginRequest);
            return Json(userId);
        }

        [Route("user/{id}")]
        [HttpPut]
        public JsonResult UpdateUser([FromBody] User user)
        {
            var userId = _dal.UpdateUser(user);
            return Json(userId);
        }

        [Route("review/{id}")]
        [HttpPut]
        public JsonResult UpdateReview([FromBody] Review review)
        {
            var reviewId = _dal.UpdateReview(review);
            return Json(reviewId);
        }

        [Route("movie/{id:int}/reviews")]
        [HttpGet]
        public JsonResult GetReviewsForMovie(int movieId)
        {
            List<Review> reviews = _dal.GetReviewsForMovie(movieId);
            return Json(reviews);
        }

        [Route("user/{id}/reviews")]
        [HttpGet]
        public JsonResult GetReviewsForUser(string userId)
        {
            List<Review> reviews = _dal.GetReviewsForUser(userId);
            return Json(reviews);
        }

        [Route("upvote")]
        [HttpPost]
        public JsonResult DoUpvote([FromBody] Upvote upvote)
        {
            bool success = _dal.CreateUpvote(upvote);
            _dal.RemoveDownvote(upvote);
            return Json(success);
        }

        [Route("downvote")]
        [HttpPost]
        public JsonResult DoDownvote([FromBody] Downvote downvote)
        {
            bool success = _dal.CreateDownvote(downvote);
            _dal.RemoveUpvote(downvote);
            return Json(success);
        }

        [Route("upvote")]
        [HttpDelete]
        public JsonResult RemoveUpvote([FromBody] Upvote upvote)
        {
            bool success = _dal.RemoveUpvote(upvote);
            return Json(success);
        }

        [Route("downvote")]
        [HttpDelete]
        public JsonResult RemoveDownvote([FromBody] Downvote downvote)
        {
            bool success = _dal.RemoveDownvote(downvote);
            return Json(success);
        }

        [Route("review/{id}")]
        [HttpDelete]
        public JsonResult DeleteReview(string id)
        {
            bool success = _dal.DeleteReview(id);
            return Json(success);
        }
    }
}
