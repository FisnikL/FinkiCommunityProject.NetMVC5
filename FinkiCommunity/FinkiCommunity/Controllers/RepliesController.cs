using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinkiCommunity.Models;
using Microsoft.AspNet.Identity;

namespace FinkiCommunity.Controllers
{
    public class RepliesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Replies
        public ActionResult Index()
        {
            return View(db.Replies.ToList());
        }

        // GET: Replies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        [Authorize]
        // GET: Replies/Create
        public ActionResult Create(int id)
        {
            Post post = db.Posts
                .Include(p => p.UserOwner)
                .Where(p => p.Id == id)
                .First();

            ViewBag.Post = post;
            ViewBag.PostId = post.Id;

            return View();
        }

        // POST: Replies/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Content")] NewReplyModel newReplyModel)
        {
            if (ModelState.IsValid)
            {
                Post post = db.Posts.Find(newReplyModel.PostId);

                Reply reply = new Reply()
                {
                    Content = newReplyModel.Content,
                    Created = DateTime.Now,
                    NumberOfLikes = 0,
                    UserOwner = db.Users.Find(User.Identity.GetUserId()),
                    ToPost = post
                };

                post.NumberOfReplies++;

                db.Replies.Add(reply);
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { id = newReplyModel.PostId });
            }

            //return View(reply);
            return View();
        }

        // GET: Replies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,Created,NumberOfLikes")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reply);
        }

        // GET: Replies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reply reply = db.Replies.Find(id);
            db.Replies.Remove(reply);
            db.SaveChanges();
            return RedirectToAction("Index");
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
