using FinkiCommunity.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.IO;
using System;

namespace FinkiCommunity.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = RoleName.Admin)]
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/username
        public ActionResult Details(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users
                .Include(u => u.Roles)
                .Where(u => u.UserName == id)
                .FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new UserDetailModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = db.Roles.Find(user.Roles.ToList()[0].RoleId).Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Gender = user.Gender,
                ProfilePictureUrl = GetProfilePictureForUser(user),
                IsHisHerProfile = User.Identity.GetUserName() == id,
                Rating = user.Rating
            };

            return View(model);
        }

        private string GetProfilePictureForUser(ApplicationUser user)
        {
            if(user.ProfilePictureUrl == null)
            {
                if(user.Gender == "M")
                {
                    return "~/Content/profile-images/MALE_AVATAR.png";
                }
                else
                {
                    return "~/Content/profile-images/FEMALE_AVATAR.png";
                }
            }
            else
            {
                return user.ProfilePictureUrl;
            }
        }


        // GET: Users/Edit/username
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var loggedInUserName = User.Identity.GetUserName();
            if(loggedInUserName != id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            ApplicationUser user = db.Users.Where(u => u.UserName == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new UserEditModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate
            };

            return View(model);
        }


        // GET: Users/Edit/username
        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserEditModel userEditModel)
        {
            // SECURITY LEAK!!!!!
            // WE WILL SOLVE IT WITH Bind CONSTRAINTS
            string userName = userEditModel.UserName;
            var loggedInUserName = User.Identity.GetUserName();
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();

            if (user == null)
            {
                // TO DO: BAD REQUEST
                return HttpNotFound();
            }

            if (loggedInUserName != user.UserName)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // ApplicationUser user = db.Users.Where(u => u.UserName == id).FirstOrDefault();
            user.Email = userEditModel.Email;
            user.FirstName = userEditModel.FirstName;
            user.LastName = userEditModel.LastName;
            user.Birthdate = userEditModel.Birthdate;

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Detail", "Users", new { id = user.UserName});
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateProfilePicture(UpdateProfilePictureModel updateProfilePictureModel)
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            string fileName = Path.GetFileNameWithoutExtension(updateProfilePictureModel.ProfilePicture.FileName);
            string extension = Path.GetExtension(updateProfilePictureModel.ProfilePicture.FileName);

            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string profilePicturePath = "~/Content/profile-images/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/Content/profile-images/"), fileName);

            updateProfilePictureModel.ProfilePicture.SaveAs(fileName);

            user.ProfilePictureUrl = profilePicturePath;

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = user.UserName});

            // TO DO: REMOVE THE OLD IMAGE SO IT WON'T TAKE MEMORY
        }
    }
}