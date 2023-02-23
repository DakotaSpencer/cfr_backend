namespace cfr_backend.Models
{
    public class UserPostModel
    {
        public string UserEmail { get;set; }
        public string UserCity { get; set; }
        public string UserZIP { get; set; }
        public string UserState { get; set; }
        public string UserPhone { get; set; }
        public string UserStreet { get; set;}
        public string? UserPfpUrl { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string? UserBio { get; set; }
    }
}