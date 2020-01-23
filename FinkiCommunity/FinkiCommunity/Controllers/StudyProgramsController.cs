using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FinkiCommunity.Models;

namespace FinkiCommunity.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class StudyProgramsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: StudyPrograms
        public ActionResult Index()
        {
            return View(db.StudyPrograms.ToList());
        }

        //// GET: StudyPrograms/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudyProgram studyProgram = db.StudyPrograms.Find(id);
        //    if (studyProgram == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(studyProgram);
        //}

        
        // GET: StudyPrograms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudyPrograms/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name")] StudyProgram studyProgram)
        {
            if (ModelState.IsValid)
            {
                db.StudyPrograms.Add(studyProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studyProgram);
        }

        
        // GET: StudyPrograms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyProgram studyProgram = db.StudyPrograms.Find(id);
            if (studyProgram == null)
            {
                return HttpNotFound();
            }
            return View(studyProgram);
        }

        // POST: StudyPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name")] StudyProgram studyProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studyProgram);
        }

        // GET: StudyPrograms/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudyProgram studyProgram = db.StudyPrograms.Find(id);
        //    if (studyProgram == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(studyProgram);
        //}

        // POST: StudyPrograms/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    StudyProgram studyProgram = db.StudyPrograms.Find(id);
        //    db.StudyPrograms.Remove(studyProgram);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
