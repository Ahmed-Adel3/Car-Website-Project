using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Users = "ADMIN")]
    public class Admin_CarAdv_ApprovalController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var carAdv = db.CarAdv.Where(c=>c.state==status.waiting);
            return View(carAdv.ToList());
        }

    ///    [HttpPost]
     //   [ValidateAntiForgeryToken]
        public ActionResult Publish( int id)
        {
                CarAdv carAdv= db.CarAdv.FirstOrDefault(a => a.Aid == id);
                carAdv.state = status.accepted;
                db.Entry(carAdv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Setting");
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

    }
}