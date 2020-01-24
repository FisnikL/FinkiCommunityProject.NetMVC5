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
        public ActionResult Index()
        {
            List<HomeGroupModel> homePageGroups = db.Groups.Select(group => new HomeGroupModel
            {
                CourseCode = group.CourseCode,
                CourseName = group.CourseName,
                CourseDescription = group.CourseDescription
            }).ToList();

            List<HomePostModel> homePagePosts = db.Posts.Include(p => p.UserOwner).OrderByDescending(p => p.Created)
                .Select(post => new HomePostModel()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    Created = post.Created
                }).ToList();


            HomePageModel model = new HomePageModel()
            {
                HomeGroups = homePageGroups,
                HomePosts = homePagePosts
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}