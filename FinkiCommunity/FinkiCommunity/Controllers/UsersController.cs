using FinkiCommunity.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FinkiCommunity.Models;
using System.Data.Entity;
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

        // GET: Users/Detail/username
        public ActionResult Detail(string id)
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
                IsHisHerProfile = User.Identity.GetUserName() == id
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

        [Authorize]
        // GET: Users/Edit/username
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

            return View(user);
        }
    }
}