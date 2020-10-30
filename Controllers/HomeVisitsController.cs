using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Ass.Models;
using FIT5032_Week08A.Utils;
using Microsoft.AspNet.Identity;

namespace FIT5032_Ass.Controllers
{
    public class HomeVisitsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: HomeVisits
        public ActionResult Index()
        {
            var homeVisitSet = db.HomeVisitSet.Include(h => h.Student).Include(h => h.Teacher);
            return View(homeVisitSet.ToList());
        }

        // GET: HomeVisits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeVisit homeVisit = db.HomeVisitSet.Find(id);
            if (homeVisit == null)
            {
                return HttpNotFound();
            }
            return View(homeVisit);
        }

        // GET: HomeVisits/Create
        public ActionResult Create(String date)
        {
            if (null == date)
                return RedirectToAction("Index");
            HomeVisit e = new HomeVisit();
            DateTime convertedDate = DateTime.Parse(date);
            e.Date = convertedDate;
            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName");
            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "FirstName");
            return View(e);
        }

        // POST: HomeVisits/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeacherEmail,Date,Subject,StudentId,TeacherId")] HomeVisit homeVisit)
        {
            var date = homeVisit.Date.ToString();
            EmailAsync(date,homeVisit.TeacherEmail);
            if (homeVisit.Date.CompareTo(DateTime.Now) < 0) 
            {
                return Content("<script>alert('Start time cannot before now, please select again'); window.location.href='../HomeVisits/Index'</script>");
            }           
                      
            if (ModelState.IsValid)
            {
                db.HomeVisitSet.Add(homeVisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName", homeVisit.StudentId);
            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "FirstName", homeVisit.TeacherId);
            return View(homeVisit);
        }

        // GET: HomeVisits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeVisit homeVisit = db.HomeVisitSet.Find(id);
            if (homeVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName", homeVisit.StudentId);
            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "FirstName", homeVisit.TeacherId);
            return View(homeVisit);
        }

        // POST: HomeVisits/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeacherEmail,Date,Subject,StudentId,TeacherId")] HomeVisit homeVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.StudentSet, "Id", "FirstName", homeVisit.StudentId);
            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "FirstName", homeVisit.TeacherId);
            return View(homeVisit);
        }

        // GET: HomeVisits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeVisit homeVisit = db.HomeVisitSet.Find(id);
            if (homeVisit == null)
            {
                return HttpNotFound();
            }
            return View(homeVisit);
        }

        // POST: HomeVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeVisit homeVisit = db.HomeVisitSet.Find(id);
            db.HomeVisitSet.Remove(homeVisit);
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

        public async System.Threading.Tasks.Task<ActionResult> EmailAsync(String Date, String Email)
        {
            String toEmail = Email;
            String body = "Your booking on " + Date + " is confirmed!";
            EmailSender es = new EmailSender();
            await es.SendAsync(toEmail, body);
            return null;

        }
    }
}
