using CFRDal.Models;
using BCrypt.Net;

namespace CFRDal
{
    public class UserManager
    {
        public string CreateUser(User user)
        {
            using (var dbContext = new ApiDbContext())
            {
                if(dbContext.Users.Any(u => u.UserEmail == user.UserEmail))
                {
                    return "User with email " + user.UserEmail + " already exists.";
                }

                user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword, BCrypt.Net.BCrypt.GenerateSalt(10));
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return user.UserId;
            }
        }

        public string UpdateReview(Review review)
        {
            using (var dbContext = new ApiDbContext())
            {
                try
                {
                    dbContext.Reviews.Update(review);
                    dbContext.SaveChanges();
                    return review.ReviewId;
                } catch (Exception e) {
                    Console.WriteLine("Exception updating review: " + e);
                    return "Could not update review";
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
                if(context.Reviews.Where(r => r.ReviewUserId == review.ReviewUserId && r.ReviewMovieId == review.ReviewMovieId).ToList().Count > 0)
                {
                    return "Could not create review. User has already reviewed this movie.";
                }

                if(review.ReviewRating < 0 || review.ReviewRating > 5)
                {
                    return "Bad data passed in. Rating must be between 0 and 5.";
                }
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

        public bool DeleteReview(string id)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    var review = context.Reviews.Where(review => review.ReviewId == id).FirstOrDefault();
                    context.Reviews.Remove(review);
                    context.SaveChanges();
                    return true;
                } catch (Exception e) {
                    Console.WriteLine("Exception deleting review: " + e);
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

        public List<ReviewData> GetReviewsForMovie(int id)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    List<ReviewData> reviews = context.ReviewData.Where(review => review.ReviewMovieId == id).ToList();
                    return reviews;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not get reviews: " + e);
                    return new List<ReviewData>();
                }
            }
        }
        
        public List<ReviewData> GetReviewsForUser(string id)
        {
            using (var context = new ApiDbContext())
            {
                try
                {
                    List<ReviewData> reviews = context.ReviewData.Where(review => review.UserId == id).ToList();
                    return reviews;
                } catch (Exception e)
                {
                    Console.WriteLine("Could not get reviews: " + e);
                    return new List<ReviewData>();
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

        public bool UserExists(string userId)
        {
            using (var context = new ApiDbContext())
            {
                if(context.Users.Where(u => u.UserId == userId).ToList().Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}