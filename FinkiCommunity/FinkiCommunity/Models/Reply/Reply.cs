using System;
using System.Collections.Generic;

namespace FinkiCommunity.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfLikes { get; set; }
        public ApplicationUser UserOwner { get; set; }
        public Post ToPost { get; set; }
    }
}