using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiCommunity.Models
{
    public class PostDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Creted { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfReplies { get; set; }
        public ApplicationUser UserOwner { get; set; }
        public Group Group { get; set; }
        public IEnumerable<Reply> Replies { get; set; }
    }
}