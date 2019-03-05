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
    public class Admin_AccessoryAdv_ApprovalController : Controller
    {
            ApplicationDbContext db = new ApplicationDbContext();
            // GET: Admin_Home
            public ActionResult Index()
            {

                var AccessoryAdv = db.AccessoriesAdv.Where(c => c.state == status.waiting);
                return View(AccessoryAdv.ToList());
            }

            ///    [HttpPost]
            //   [ValidateAntiForgeryToken]
            public ActionResult Publish(int id)
            {
                AccessoriesAdv AccessoryAdv = db.AccessoriesAdv.FirstOrDefault(a => a.AccId == id);
                AccessoryAdv.state = status.accepted;
                db.Entry(AccessoryAdv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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

    }
}
