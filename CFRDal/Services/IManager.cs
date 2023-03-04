using CFRDal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFRDal
{
    public interface IManager
    {
        public Movie GetMovie(int id);
        public List<SearchResultMovie> SearchMovies(string query);
        public List<SearchResultMovie> GetSimilarMovies(int id);
        public List<Review> GetReviewsForMovie(int movieId);
        public List<Review> GetReviewsForUser(string userId);
        public string AuthenticateUser(LoginRequest loginRequest);
        public string CreateUser(User user);
        public bool DeleteUser(string id);
        public bool DeleteReview(string id);
        public string CreateReview(Review review);
        public string UpdateUser(User user);
        public string UpdateReview(Review review);
        // votes
        public bool CreateUpvote(Upvote upvote);
        public bool CreateDownvote(Downvote downvote);
        public bool RemoveUpvote(Upvote upvote);
        public bool RemoveDownvote(Downvote downvote);
        public bool RemoveUpvote(Downvote downvote);
        public bool RemoveDownvote(Upvote upvote);
    }
}
