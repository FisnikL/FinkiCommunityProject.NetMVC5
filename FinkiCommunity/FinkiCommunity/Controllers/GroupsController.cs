using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FinkiCommunity.Models;

namespace FinkiCommunity.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Groups/Details/{CourseCode}
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Where(g => g.CourseCode == id).FirstOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [Authorize(Roles = RoleName.Admin)]
        // GET: Groups/Create
        public ActionResult Create()
        {
            var model = new CreateGroupModelGet();

            model.StudyYear.AddRange(Enum.GetNames(typeof(Enums.StudyYear)));
            model.Semester.AddRange(Enum.GetNames(typeof(Enums.Semester)));
            model.CourseType.AddRange(Enum.GetNames(typeof(Enums.CourseType)));
            model.StudyPrograms = db.StudyPrograms.Select(sP => sP.Name).ToList();

            return View(model);
        }

        [Authorize(Roles = RoleName.Admin)]
        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGroupModelPost model)
        {
            List<StudyProgram> studyPrograms = new List<StudyProgram>();

            StudyProgram studyProgramDb;
            foreach(string studyProgram in model.StudyPrograms)
            {
                studyProgramDb = db.StudyPrograms.Where(sP => sP.Name.Equals(studyProgram)).First();
                if(studyProgramDb != null)
                {
                    studyPrograms.Add(studyProgramDb);
                }
            }
            Group gr = db.Groups.Where(g => g.CourseCode == model.CourseCode).FirstOrDefault();
            if (ModelState.IsValid && gr == null)
            {
                Group group = new Group()
                {
                    CourseCode = model.CourseCode,
                    CourseName = model.CourseName,
                    CourseDescription = model.CourseDescription,
                    StudyYear = (Enums.StudyYear) Enum.Parse(typeof(Enums.StudyYear), model.StudyYear),
                    Semester = (Enums.Semester)Enum.Parse(typeof(Enums.Semester), model.Semester),
                    CourseType = (Enums.CourseType)Enum.Parse(typeof(Enums.CourseType), model.CourseType),
                    StudyPrograms = studyPrograms,
                    NumberOfPosts = 0,
                    NumberOfReplies = 0,
                    CoursePictureUrl = DefaultImagePath.DEFAULT_GROUP_IMAGE
                };

                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The CourseCode is already taken!");

            var m = new CreateGroupModelGet();

            m.StudyYear.AddRange(Enum.GetNames(typeof(Enums.StudyYear)));
            m.Semester.AddRange(Enum.GetNames(typeof(Enums.Semester)));
            m.CourseType.AddRange(Enum.GetNames(typeof(Enums.CourseType)));
            m.StudyPrograms = db.StudyPrograms.Select(sP => sP.Name).ToList();

            return View(m);
        }

        // GET: Groups/Edit/{CourseCode}
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Where(g => g.CourseCode == id).FirstOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }

            var model = new EditGroupModel()
            {
                CourseCode = group.CourseCode,
                CourseName = group.CourseName,
                CourseDescription = group.CourseDescription
            };

            return View(model);
        }


        // POST: Groups/Edit/{CourseCode}
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditGroupModel editGroupModel)
        {
            if (ModelState.IsValid)
            {
                Group group = db.Groups.Include(g => g.StudyPrograms).Where(g => g.CourseCode == editGroupModel.CourseCode).First();
                
                group.CourseName = editGroupModel.CourseName;
                group.CourseDescription = editGroupModel.CourseDescription;
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editGroupModel);
        }

        // GET: Groups/Posts/{CourseName}
        public ActionResult Posts(string id)
        {
            // In fact group
            var model = db.Groups.Include(g => g.Posts).Where(g => g.CourseCode == id).First();
            model.Posts = model.Posts.OrderByDescending(post => post.Created).ToList();
            return View(model);
        }

        // POST Groups/
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Moderator)]
        [HttpPost]
        public ActionResult UpdateGroupPicture(UpdateGroupPictureModel updateGroupPictureModel)
        {
            Group group = db.Groups.Where(g => g.CourseCode == updateGroupPictureModel.CourseCode).First();

            string fileName = Path.GetFileNameWithoutExtension(updateGroupPictureModel.GroupPicture.FileName);
            string extension = Path.GetExtension(updateGroupPictureModel.GroupPicture.FileName);

            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string groupPicturePath = "~/Content/group-images/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/Content/group-images/"), fileName);

            updateGroupPictureModel.GroupPicture.SaveAs(fileName);

            group.CoursePictureUrl = groupPicturePath;

            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = group.CourseCode });

            // TO DO: REMOVE THE OLD IMAGE SO IT WON'T TAKE MEMORY
        }

        public ActionResult SearchGroupsPosts(string CourseCode, string groupPostTerm)
        {
            Group group = db.Groups
                .Where(g => g.CourseCode == CourseCode)
                .Include(g => g.Posts)
                .FirstOrDefault();

            // MAYBE WE NEED TO SORT DESC!
            group.Posts = group.Posts.Where(p => p.Title.Contains(groupPostTerm) || p.Content.Contains(groupPostTerm)).OrderByDescending(g => g.Created).ToList();

            if (group == null)
            {
                return HttpNotFound();
            }

            return View("Posts", group);
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
