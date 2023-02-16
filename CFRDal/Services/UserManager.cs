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
                    review.ReviewTime = DateTime.Now;
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
        
        public List<Review> GetReviewsForUser(string id)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    List<Review> reviews = context.Reviews.Where(review => review.ReviewUserId == id).ToList();
                    return reviews;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not get reviews: " + e);
                    return new List<Review>();
                }
            }
        }

        // votes
        public bool CreateUpvote(Upvote upvote)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    context.Upvotes.Add(upvote);
                    return true;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not create upvote: " + e);
                    return false;
                }
            }
        }

        public bool CreateDownvote(Downvote downvote)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    context.Downvotes.Add(downvote);
                    return true;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not create downvote: " + e);
                    return false;
                }
            }            
        }
        
        public bool RemoveUpvote(Upvote upvote)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    context.Upvotes.Remove(upvote);
                    return true;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not remove upvote: " + e);
                    return false;
                }
            }
        }
        
        public bool RemoveUpvote(Downvote downvote)
        {
            // if upvote exists then remove
            using (var context = new ApiDbContext())
            {
                Upvote upvote = new Upvote() { UpvoteReviewId = downvote.DownvoteReviewId, UpvoteUserId = downvote.DownvoteUserId };
                if(context.Upvotes.Find(upvote) == null)
                {
                    return false;
                }

                try
                {
                    
                    context.Upvotes.Remove(upvote);
                    return true;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not remove upvote: " + e);
                    return false;
                }
            }
        }
        
        public bool RemoveDownvote(Downvote downvote)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    context.Downvotes.Remove(downvote);
                    return true;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not remove downvote: " + e);
                    return false;
                }
            }            
        }
        
        public bool RemoveDownvote(Upvote upvote)
        {
            // if downvote exists then remove   
            using (var context = new ApiDbContext())
            {
                Downvote downvote = new Downvote() { DownvoteReviewId = upvote.UpvoteReviewId, DownvoteUserId = upvote.UpvoteUserId };
                if(context.Downvotes.Find(downvote) == null)
                {
                    return false;
                }

                try
                {
                    context.Downvotes.Remove(downvote);
                    return true;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not remove upvote: " + e);
                    return false;
                }
            }
        }
    }
}