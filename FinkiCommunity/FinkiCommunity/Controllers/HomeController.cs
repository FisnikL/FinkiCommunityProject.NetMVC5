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

            List<HomeGroupModel>homePageGroups = db.Groups.Select(group => new HomeGroupModel
            {
                CourseCode = group.CourseCode,
                CourseName = group.CourseName,
                CourseDescription = group.CourseDescription,
                CoursePictureUrl = group.CoursePictureUrl
            }).ToList();


            List<HomePostModel> homePagePosts = db.Posts
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
            

            HomePageModel model = new HomePageModel()
            {
                HomeGroups = homePageGroups,
                HomePosts = homePagePosts
            };

            return View(model);
        }

        public ActionResult SearchGroups(string groupTerm)
        {
            List<HomeGroupModel> homePageGroups = db.Groups
                 .Where(g => g.CourseName.Contains(groupTerm) || g.CourseDescription.Contains(groupTerm) || g.CourseDescription.Contains(groupTerm))
                .Select(group => new HomeGroupModel
                {
                    CourseCode = group.CourseCode,
                    CourseName = group.CourseName,
                    CourseDescription = group.CourseDescription,
                    CoursePictureUrl = group.CoursePictureUrl
                }).ToList();


            List<HomePostModel> homePagePosts = db.Posts
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

            HomePageModel model = new HomePageModel()
            {
                HomeGroups = homePageGroups,
                HomePosts = homePagePosts
            };
            
            return View("Index", model);
        }

        public ActionResult SearchPosts(string postTerm)
        {
            List<HomeGroupModel> homePageGroups = db.Groups
                .Select(group => new HomeGroupModel
                {
                    CourseCode = group.CourseCode,
                    CourseName = group.CourseName,
                    CourseDescription = group.CourseDescription,
                    CoursePictureUrl = group.CoursePictureUrl
                }).ToList();


            List<HomePostModel> homePagePosts = db.Posts
                .Where(p => p.Title.Contains(postTerm) || p.Content.Contains(postTerm))
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

            HomePageModel model = new HomePageModel()
            {
                HomeGroups = homePageGroups,
                HomePosts = homePagePosts
            };

            return View("Index", model);
        }
    }
}