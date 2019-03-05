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
    public class Admin_AccessoriesAdvsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin_AccessoriesAdvs
        public ActionResult Index()
        {
            var accessoriesAdv = db.AccessoriesAdv.Include(a => a.Manufacturer);
            return View(accessoriesAdv.ToList());
        }

        // GET: Admin_AccessoriesAdvs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessoriesAdv accessoriesAdv = db.AccessoriesAdv.Find(id);
            if (accessoriesAdv == null)
            {
                return HttpNotFound();
            }
            return View(accessoriesAdv);
        }

        // GET: Admin_AccessoriesAdvs/Create
        public ActionResult Create()
        {
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name");
            return View();
        }

        // POST: Admin_AccessoriesAdvs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccId,AccName,Aphoto,Price,AdDate,Mid")] AccessoriesAdv accessoriesAdv)
        {
            if (ModelState.IsValid)
            {
                db.AccessoriesAdv.Add(accessoriesAdv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name", accessoriesAdv.Mid);
            return View(accessoriesAdv);
        }

        // GET: Admin_AccessoriesAdvs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessoriesAdv accessoriesAdv = db.AccessoriesAdv.Find(id);
            if (accessoriesAdv == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name", accessoriesAdv.Mid);
            return View(accessoriesAdv);
        }

        // POST: Admin_AccessoriesAdvs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccId,AccName,Aphoto,Price,AdDate,Mid")] AccessoriesAdv accessoriesAdv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessoriesAdv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name", accessoriesAdv.Mid);
            return View(accessoriesAdv);
        }

        // GET: Admin_AccessoriesAdvs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessoriesAdv accessoriesAdv = db.AccessoriesAdv.Find(id);
            if (accessoriesAdv == null)
            {
                return HttpNotFound();
            }
            return View(accessoriesAdv);
        }

        // POST: Admin_AccessoriesAdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessoriesAdv accessoriesAdv = db.AccessoriesAdv.Find(id);
            db.AccessoriesAdv.Remove(accessoriesAdv);
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
