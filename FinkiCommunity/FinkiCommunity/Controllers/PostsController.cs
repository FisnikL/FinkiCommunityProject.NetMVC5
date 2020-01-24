using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FinkiCommunity.Models;
using Microsoft.AspNet.Identity;

namespace FinkiCommunity.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Posts.Include(p => p.UserOwner).ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            Post post = db.Posts
                .Include(p => p.UserOwner)
                .Include(p => p.Group)
                .Include(p => p.Replies)
                .Include("Replies.UserOwner")
                .Where(p => p.Id == id).First();

            if (post == null)
            {
                return HttpNotFound();
            }


            return View(post);
        }

        [Authorize]
        // GET: Posts/Create/{CourseName}
        public ActionResult Create(string id)
        {
            ViewBag.CourseCode = id;
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewPostModel newPostModel)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post()
                {
                    Title = newPostModel.Title,
                    Content = newPostModel.Content,
                    Created = DateTime.Now,
                    NumberOfLikes = 0,
                    NumberOfReplies = 0,
                    UserOwner = db.Users.Find(User.Identity.GetUserId()),
                    Group = db.Groups.Where(g => g.CourseCode == newPostModel.CourseCode).First()
                };

                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Posts", "Groups", new { id = newPostModel.CourseCode });
            }

            return View();
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var model = new EditPostModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };

            return View(model);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")] EditPostModel editPostModel)
        {
            if (ModelState.IsValid)
            {
                Post post = db.Posts.Find(editPostModel.Id);
                post.Title = editPostModel.Title;
                post.Content = editPostModel.Content;

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = editPostModel.Id });
            }
            return View(editPostModel);
        }

        //[Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        //GET: Posts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Include(p => p.Replies).Where(p => p.Id == id).First();
            // db.Posts.Remove(post);
            db.Replies.RemoveRange(post.Replies.ToList());
            // db.SaveChanges();
            db.Posts.Remove(post);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
