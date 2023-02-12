using CFRDal.Models;
using BCrypt.Net;

namespace CFRDal
{
    public class UserManager
    {
        private static string HASH = "$2a$10$6w0p7nwaO7Q4R5uEF8Wc3.D2REixhRK7nZKimj.T48iMCdPcSWD3G";
        public string CreateUser(User user)
        {
            using (var dbContext = new ApiDbContext())
            {
                if(dbContext.Users.Any(u => u.UserEmail == user.UserEmail))
                {
                    throw new ApplicationException("User with email " + user.UserEmail + " already exists.");
                }

                try 
                {
                    user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword, BCrypt.Net.BCrypt.GenerateSalt(10));
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return user.UserId;
                } catch (Exception e) {
                    Console.WriteLine("Exception creating user: " + e);
                    return "Could not create user";
                }
            }
        }

        public string UpdateUser(User user)
        {
            using (var dbContext = new ApiDbContext())
            {
                try
                {
                    dbContext.Users.Update(user);
                    dbContext.SaveChanges();
                    return user.UserId;
                } catch (Exception e) {
                    Console.WriteLine("Exception updating user: " + e);
                    return "Could not update user";
                }
            }
        }

        public string CreateReview(Review review)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    context.Reviews.Add(review);
                    context.SaveChanges();
                    return review.ReviewId;
                } catch (Exception ex)
                {
                    return "Exception creating review: " + ex;
                }
            }
        }

        public bool DeleteUser(string id)
        {
            using (var dbContext = new ApiDbContext())
            {
                try
                {
                    var user = dbContext.Users.Where(user => user.UserId == id).FirstOrDefault();
                    dbContext.Users.Remove(user);
                    dbContext.SaveChanges();
                    return true;
                } catch (Exception e) {
                    Console.WriteLine("Exception deleting user: " + e);
                    return false;
                }
            }
        }

        public string Login(LoginRequest loginRequest)
        {
            Console.WriteLine("EMAIL: " + loginRequest.email + " ; PASSWORD: " + loginRequest.password);
            using (var context = new ApiDbContext())
            {
                User user = context.Users.Where((user) => user.UserEmail.Equals(loginRequest.email)).FirstOrDefault();
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.password, user.UserPassword))
                {

                    Console.WriteLine("VERIFY: " + BCrypt.Net.BCrypt.Verify(loginRequest.password, user.UserPassword));
                    string attempt = BCrypt.Net.BCrypt.HashPassword(loginRequest.password);
                    Console.WriteLine("VERIFY TEST: " + BCrypt.Net.BCrypt.Verify(loginRequest.password, attempt));
                    throw new ApplicationException("Email or password is incorrect.");
                }
                return user.UserId;
            }
        }

        public List<Review> GetReviewsForMovie(int id)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    List<Review> reviews = context.Reviews.Where(review => review.ReviewMovieId == id).ToList();
                    return reviews;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not get reviews: " + e);
                    return new List<Review>();
                }
            }
        }
    }
}