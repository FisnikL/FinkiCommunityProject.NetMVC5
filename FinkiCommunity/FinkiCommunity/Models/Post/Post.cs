using System;
using System.Collections.Generic;

namespace FinkiCommunity.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfReplies { get; set; }
        public IEnumerable<ApplicationUser> UsersLiked { get; set; }
        public ApplicationUser UserOwner { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public Group Group { get; set; }
    }
}