using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Users = "ADMIN")]
    public class Admin_JobAdvsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin_JobAdvs
        public ActionResult Index()
        {
            var jobAdv = db.JobAdv.Include(j => j.ApplicationUser).Include(j => j.City).Include(j => j.Governrate).Include(j => j.TechJob);
            return View(jobAdv.ToList());
        }

        // GET: Admin_JobAdvs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobAdv jobAdv = db.JobAdv.Find(id);
            if (jobAdv == null)
            {
                return HttpNotFound();
            }
            return View(jobAdv);
        }

        // GET: Admin_JobAdvs/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName");
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname");
            ViewBag.Jid = new SelectList(db.TechJob, "Jid", "Jname");
            return View();
        }

        // POST: Admin_JobAdvs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JAid,WorkShopName,WorkShopPhone,CityId,Gid,Description,AdDate,ApplicationUser_Id,Jid")] JobAdv jobAdv)
        {
            if (ModelState.IsValid)
            {
                db.JobAdv.Add(jobAdv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "FirstName", jobAdv.ApplicationUser_Id);
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName", jobAdv.CityId);
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname", jobAdv.Gid);
            ViewBag.Jid = new SelectList(db.TechJob, "Jid", "Jname", jobAdv.Jid);
            return View(jobAdv);
        }

        // GET: Admin_JobAdvs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobAdv jobAdv = db.JobAdv.Find(id);
            if (jobAdv == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "FirstName", jobAdv.ApplicationUser_Id);
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName", jobAdv.CityId);
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname", jobAdv.Gid);
            ViewBag.Jid = new SelectList(db.TechJob, "Jid", "Jname", jobAdv.Jid);
            return View(jobAdv);
        }

        // POST: Admin_JobAdvs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JAid,WorkShopName,WorkShopPhone,CityId,Gid,Description,AdDate,ApplicationUser_Id,Jid")] JobAdv jobAdv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobAdv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "FirstName", jobAdv.ApplicationUser_Id);
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName", jobAdv.CityId);
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname", jobAdv.Gid);
            ViewBag.Jid = new SelectList(db.TechJob, "Jid", "Jname", jobAdv.Jid);
            return View(jobAdv);
        }

        // GET: Admin_JobAdvs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobAdv jobAdv = db.JobAdv.Find(id);
            if (jobAdv == null)
            {
                return HttpNotFound();
            }
            return View(jobAdv);
        }

        // POST: Admin_JobAdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobAdv jobAdv = db.JobAdv.Find(id);
            db.JobAdv.Remove(jobAdv);
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
