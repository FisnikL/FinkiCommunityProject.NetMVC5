using System;

namespace FinkiCommunity.Models
{
    public class UserDetailModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsHisHerProfile { get; set; }
    }
}