using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiCommunity.Models
{
    public class HomePageModel
    {
        // public string CourseCode { get; set; }
        public List<HomeGroupModel> HomeGroups { get; set; }
        public List<HomePostModel> HomePosts { get; set; }
    }
}