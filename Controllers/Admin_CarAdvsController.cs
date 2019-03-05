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
    public class Admin_CarAdvsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin_CarAdvs
        public ActionResult Index()
        {
            var carAdv = db.CarAdv.Include(c => c.City).Include(c => c.Governrate).Include(c => c.Manufacturer);
            return View(carAdv.ToList());
        }

        // GET: Admin_CarAdvs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarAdv carAdv = db.CarAdv.Find(id);
            if (carAdv == null)
            {
                return HttpNotFound();
            }
            return View(carAdv);
        }

        // GET: Admin_CarAdvs/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName");
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname");
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name");
            return View();
        }

        // POST: Admin_CarAdvs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Aid,Color,Year,Cc,Km,AirDevice,SunRoof,Transimition,Photos,Price,AdDate,Gid,CityId,Description,AirBag,Power,CenterLock,Alarm,Radio,ABS,ElectricWindow,Uid,Mid")] CarAdv carAdv)
        {
            if (ModelState.IsValid)
            {
                db.CarAdv.Add(carAdv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName", carAdv.CityId);
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname", carAdv.Gid);
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name", carAdv.Mid);
            return View(carAdv);
        }

        // GET: Admin_CarAdvs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarAdv carAdv = db.CarAdv.Find(id);
            if (carAdv == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName", carAdv.CityId);
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname", carAdv.Gid);
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name", carAdv.Mid);
            return View(carAdv);
        }

        // POST: Admin_CarAdvs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Aid,Color,Year,Cc,Km,AirDevice,SunRoof,Transimition,Photos,Price,AdDate,Gid,CityId,Description,AirBag,Power,CenterLock,Alarm,Radio,ABS,ElectricWindow,Uid,Mid")] CarAdv carAdv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carAdv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName", carAdv.CityId);
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname", carAdv.Gid);
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name", carAdv.Mid);
            return View(carAdv);
        }

        // GET: Admin_CarAdvs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarAdv carAdv = db.CarAdv.Find(id);
            if (carAdv == null)
            {
                return HttpNotFound();
            }
            return View(carAdv);
        }

        // POST: Admin_CarAdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarAdv carAdv = db.CarAdv.Find(id);
            db.CarAdv.Remove(carAdv);
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
