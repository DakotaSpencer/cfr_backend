using CFRDal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;

namespace CFRDal
{
    public class ApiManager : IManager
    {
        private static UserManager userManager = new UserManager();
        private static string URL = "https://api.themoviedb.org/3";
        private static string API_KEY = "96fdc416520d2dd5b75c1c82c854e506";
        private static RestClient client = new RestClient(URL);

        public ApiManager() { }

        public Movie GetMovie(int id)
        {
            var req = new RestRequest("movie/" + id + "?api_key=" + API_KEY);
            var res = client.Execute(req);
            string json = "";

            if(res.StatusCode == HttpStatusCode.OK)
            {
                json = res.Content;
            }

            Movie movie = JsonSerializer.Deserialize<Movie>(json);

            return movie;
        }

        public List<Review> GetReviewsForMovie(int movieId)
        {
            return userManager.GetReviewsForMovie(movieId);
        }

        public List<Review> GetReviewsForUser(string userId)
        {
            return userManager.GetReviewsForUser(userId);
        }

        public List<Movie> SearchMovies(string query)
        {
            List<Movie> results = new List<Movie>();
            var req = new RestRequest("https://api.themoviedb.org/3/search/movie?api_key=" + API_KEY + "&language=en-US&query=" + query + "&include_adult=false");
            var res = client.Execute(req);
            string json = "";

            if(res.StatusCode == HttpStatusCode.OK)
            {
                json = res.Content;
            }

            

            return results;
        }

        public string AuthenticateUser(LoginRequest loginRequest)
        {
            return userManager.Login(loginRequest);
        }

        public string CreateUser(User user)
        {
            return userManager.CreateUser(user);
        }

        public bool DeleteUser(string id)
        {
            return userManager.DeleteUser(id);
        }

        public bool DeleteReview(string id)
        {
            return userManager.DeleteReview(id);
        }

        public string CreateReview(Review review)
        {
            return userManager.CreateReview(review);
        }

        public string UpdateUser(User user)
        {
            return userManager.UpdateUser(user);
        }

        public string UpdateReview(Review review)
        {
            return userManager.UpdateReview(review);
        }

        public bool CreateUpvote(Upvote upvote)
        {
            return userManager.CreateUpvote(upvote);
        }

        public bool CreateDownvote(Downvote downvote)
        {
            return userManager.CreateDownvote(downvote);
        }

        public bool RemoveUpvote(Upvote upvote)
        {
            return userManager.RemoveUpvote(upvote);
        }

        public bool RemoveDownvote(Downvote downvote)
        {
            return userManager.RemoveDownvote(downvote);
        }

        public bool RemoveUpvote(Downvote downvote)
        {
            return userManager.RemoveUpvote(downvote);
        }

        public bool RemoveDownvote(Upvote upvote)
        {
            return userManager.RemoveDownvote(upvote);
        }
    }
}
