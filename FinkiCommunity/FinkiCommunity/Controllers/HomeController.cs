using FinkiCommunity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace FinkiCommunity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string term)
        {
            List<HomeGroupModel> homePageGroups = new List<HomeGroupModel>();
            List<HomePostModel> homePagePosts = new List<HomePostModel>();

            if (term == null)
            {
                homePageGroups = db.Groups.Select(group => new HomeGroupModel
                {
                    CourseCode = group.CourseCode,
                    CourseName = group.CourseName,
                    CourseDescription = group.CourseDescription,
                    CoursePictureUrl = group.CoursePictureUrl
                }).ToList();


                homePagePosts = db.Posts
                    .Include(p => p.UserOwner)
                    .Include(p => p.Group)
                    .OrderByDescending(p => p.Created)
                    .Select(post => new HomePostModel()
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        Created = post.Created,
                        NumberOfLikes = post.NumberOfLikes,
                        NumberOfReplies = post.NumberOfReplies,
                        UserOwner = post.UserOwner,
                        Group = post.Group
                    }).ToList();
            }
            

            HomePageModel model = new HomePageModel()
            {
                HomeGroups = homePageGroups,
                HomePosts = homePagePosts
            };

            return View(model);
        }

        public ActionResult SearchGroups(string term)
        {
            //return View();
            return RedirectToAction("Index");
        }

        public ActionResult SearchPosts(string term)
        {
            return View();
        }
    }
}