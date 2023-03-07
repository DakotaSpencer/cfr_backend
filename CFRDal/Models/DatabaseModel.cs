using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFRDal.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ReviewId { get; set; }
        public string ReviewUserId { get; set; }
        public DateTime ReviewTime { get; set; }
        public int ReviewMovieId { get; set; }
        public string ReviewBody { get; set; }
        public string ReviewTitle { get; set; }
        public int ReviewRating { get; set; }
    }

    public class ReviewData
    {
        public string UserId { get; set; }
        public string? UserPfpUrl { get; set; }
        public string UserName { get; set; }
        public string ReviewId { get; set; }
        public string ReviewTitle { get; set; }
        public int ReviewMovieId { get; set; }
        public string ReviewBody { get; set; }
        public int ReviewRating { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }
        public string UserEmail { get;set; }
        public string UserCity { get; set; }
        public string UserZIP { get; set; }
        public string UserState { get; set; }
        public string UserPhone { get; set; }
        public string UserStreet { get; set;}
        public string UserPassword { get; set; }
        public string? UserPfpUrl { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string? UserBio { get; set; }
    }

    public class Upvote
    {
        public string UpvoteUserId { get; set; }
        public string UpvoteReviewId { get; set; }
    }

    public class Downvote
    {
        public string DownvoteUserId { get; set; }
        public string DownvoteReviewId { get; set; }
    }

    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserRole 
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
    }

}
