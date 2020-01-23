using FinkiCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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

            ApplicationUser user = db.Users.Where(u => u.UserName == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
    }
}