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

        public List<Movie> SearchMovies(string query)
        {
            return new List<Movie>();
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

        public string CreateReview(Review review)
        {
            return userManager.CreateReview(review);
        }

        public string UpdateUser(User user)
        {
            return userManager.UpdateUser(user);
        }
    }
}
