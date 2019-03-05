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
    public class ContactusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contactus
        public ActionResult Index()
        {
            return View(db.Contactus.ToList());
        }

        // GET: Contactus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = db.Contactus.Find(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // GET: Contactus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = db.Contactus.Find(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contactus contactus = db.Contactus.Find(id);
            db.Contactus.Remove(contactus);
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
