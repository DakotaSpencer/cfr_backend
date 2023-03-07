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
        private static string IMG_BASE_PATH = "http://image.tmdb.org/t/p/";
        private static RestClient client = new RestClient(URL);

        public ApiManager() { }
        // width : w500 search results
        // width : original
        // backdrop : original

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
            movie.backdrop_path = IMG_BASE_PATH + "original" + movie.backdrop_path;
            movie.poster_path = IMG_BASE_PATH + "w500" + movie.poster_path;
            for(var i = 0; i < movie.production_companies.Length; i++) {
                if(movie.production_companies[i].logo_path != null) {
                    movie.production_companies[i].logo_path = IMG_BASE_PATH + "w154" + movie.production_companies[i].logo_path;
                }
            }
            return movie;
        }

        public List<ReviewData> GetReviewsForMovie(int movieId)
        {
            return userManager.GetReviewsForMovie(movieId);
        }

        public List<ReviewData> GetReviewsForUser(string userId)
        {
            return userManager.GetReviewsForUser(userId);
        }

        public List<SearchResultMovie> SearchMovies(string query)
        {
            List<SearchResultMovie> returnList = new List<SearchResultMovie>();
            MovieHolder results = new MovieHolder();
            var req = new RestRequest("https://api.themoviedb.org/3/search/movie?api_key=" + API_KEY + "&language=en-US&query=" + query + "&include_adult=false");
            var res = client.Execute(req);
            string json = "";

            if(res.StatusCode == HttpStatusCode.OK)
            {
                results = JsonSerializer.Deserialize<MovieHolder>(res.Content);
            }

            foreach(var movie in results.results) 
            {
                movie.poster_path = IMG_BASE_PATH + "w154" + movie.poster_path;
                returnList.Add(movie);
            }

            return returnList;
        }

        // public List<SearchResultMovie> SearchMoviesByActor(string query)
        // {
        //     List<SearchResultMovie> returnList = new List<SearchResultMovie>();
        //     ActorHolder results = new ActorHolder();
        //     Person person = new Person();
        //     var req = new RestRequest("https://api.themoviedb.org/3/search/person?api_key=" + API_KEY +"&query=" + query);
        //     var res = client.Execute(req);

        //     if(res.StatusCode == HttpStatusCode.OK)
        //     {
        //         results = JsonSerializer.Deserialize<ActorHolder>(res.Content);
        //     }
        //     results.results.ForEach(r => {
        //         Console.WriteLine(r.Id);
        //         Console.WriteLine(r.Name);
        //     });
        //     int actorId = results.results[0].Id;

        //     req = new RestRequest("https://api.themoviedb.org/3/person/" + actorId + "/movie_credits?api_key=" + API_KEY + "&language=en-US&query=" + query + "&include_adult=false");
        //     res = client.Execute(req);
        //     string json = "";
        //     MovieHolder movieResults = new MovieHolder();

        //     if(res.StatusCode == HttpStatusCode.OK)
        //     {
        //         movieResults = JsonSerializer.Deserialize<MovieHolder>(res.Content);
        //     }

        //     Console.WriteLine(results);

        //     foreach(var movie in movieResults.results) 
        //     {
        //         movie.poster_path = IMG_BASE_PATH + "w154" + movie.poster_path;
        //         returnList.Add(movie);
        //     }

        //     return returnList;
        // }

        public List<SearchResultMovie> GetSimilarMovies(int id)
        {
            List<SearchResultMovie> returnList = new List<SearchResultMovie>();
            MovieHolder results = new MovieHolder();
            var req = new RestRequest("https://api.themoviedb.org/3/movie/" + id + "/similar?api_key=" + API_KEY + "&language=en-US");
            var res = client.Execute(req);
            string json = "";

            if(res.StatusCode == HttpStatusCode.OK)
            {
                results = JsonSerializer.Deserialize<MovieHolder>(res.Content);
            }

            foreach(var movie in results.results) 
            {
                movie.poster_path = IMG_BASE_PATH + "w154" + movie.poster_path;
                returnList.Add(movie);
            }

            return returnList;
        }

        public string AuthenticateUser(LoginRequest loginRequest)
        {
            return userManager.Login(loginRequest);
        }

        public bool AuthorizeUser(string userId)
        {
            return userManager.UserExists(userId);
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
