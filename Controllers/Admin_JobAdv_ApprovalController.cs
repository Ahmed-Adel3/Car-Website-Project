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
    public class Admin_JobAdv_ApprovalController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin_Home
        public ActionResult Index()
        {

            var JobAdv = db.JobAdv.Where(c => c.state == status.waiting);
            return View(JobAdv.ToList());
        }

        ///    [HttpPost]
        //   [ValidateAntiForgeryToken]
        public ActionResult Publish(int id)
        {
            JobAdv JobAdv = db.JobAdv.FirstOrDefault(a => a.JAid == id);
            JobAdv.state = status.accepted;
            db.Entry(JobAdv).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
    }
}