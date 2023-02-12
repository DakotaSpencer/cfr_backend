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
        public List<Movie> SearchMovies(string query);
        public List<Review> GetReviewsForMovie(int movieId);
        public string AuthenticateUser(LoginRequest loginRequest);
        public string CreateUser(User user);
        public bool DeleteUser(string id);
        public string CreateReview(Review review);
        public string UpdateUser(User user);
    }
}
