using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext conn = new ApplicationDbContext();

        public ActionResult getcity(int id)
        {
            List<City> cities = conn.City.Where(a => a.Gid == id).ToList<City>();
            SelectList citylist = new SelectList(cities, "CityId", "CityName");
            ViewBag.cities = citylist;
            return PartialView();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Setting()

        {
            string id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            ApplicationUser user = conn.Users.FirstOrDefault(a => a.Id == id);
            List<CarAdv> caradv = conn.CarAdv.Where(a => a.ApplicationUser_Id == id).ToList<CarAdv>();
            ViewBag.caradv = caradv;
            List<AccessoriesAdv> accadv = conn.AccessoriesAdv.Where(a => a.ApplicationUser_Id == id).ToList<AccessoriesAdv>();
            ViewBag.accadv = accadv;
            List<JobAdv> jobadv = conn.JobAdv.Where(a => a.ApplicationUser_Id == id).ToList<JobAdv>();
            ViewBag.jobadv = jobadv;

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(conn.City, "CityId", "CityName", user.CityId);
            ViewBag.Gid = new SelectList(conn.Governrate, "Gid", "Gname", user.Gid);
            return View(user);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Setting(ApplicationUser user, HttpPostedFileBase img)
        {
            if (img != null)
            {
                img.SaveAs(Server.MapPath("/images/Profile/") + img.FileName);
                string photoName = "/images/Profile/" + img.FileName;
                user.UPhoto = photoName;
            }

            ViewBag.CityId = new SelectList(conn.City, "CityId", "CityName", user.CityId);
            ViewBag.Gid = new SelectList(conn.Governrate, "Gid", "Gname", user.Gid);
            string id = user.Id;
            if (ModelState.IsValid)
            {
                user.UserName = User.Identity.Name;
                conn.Entry(user).State = System.Data.Entity.EntityState.Modified;
                conn.SaveChanges();
                user = conn.Users.Include("Governrate").Include("City").FirstOrDefault(a => a.Id == id);
            }
            return View(user);
        }
        public ActionResult SoldCar(int? id)
        {
            CarAdv carAdv = conn.CarAdv.FirstOrDefault(a => a.Aid == id);
            carAdv.state = status.sold;
            conn.Entry(carAdv).State = EntityState.Modified;
            conn.SaveChanges();

            return RedirectToAction("Setting");
        }

        public ActionResult RepublishCar(int? id)
        {
            CarAdv carAdv = conn.CarAdv.FirstOrDefault(a => a.Aid == id);
            carAdv.state = status.accepted;
            conn.Entry(carAdv).State = EntityState.Modified;
            conn.SaveChanges();

            return RedirectToAction("Setting");
        }

        public ActionResult SoldAcc(int? id)
        {
            AccessoriesAdv AccAdv = conn.AccessoriesAdv.FirstOrDefault(a => a.AccId == id);
            AccAdv.state = status.sold;
            conn.Entry(AccAdv).State = EntityState.Modified;
            conn.SaveChanges();

            return RedirectToAction("Setting");
        }

        public ActionResult RepublishAcc(int? id)
        {
            AccessoriesAdv AccAdv = conn.AccessoriesAdv.FirstOrDefault(a => a.AccId == id);
            AccAdv.state = status.accepted;
            conn.Entry(AccAdv).State = EntityState.Modified;
            conn.SaveChanges();

            return RedirectToAction("Setting");
        }

        public ActionResult SoldJob(int? id)
        {
            JobAdv jobAdv = conn.JobAdv.FirstOrDefault(a => a.JAid == id);
            jobAdv.state = status.sold;
            conn.Entry(jobAdv).State = EntityState.Modified;
            conn.SaveChanges();

            return RedirectToAction("Setting");
        }

        public ActionResult RepublishJob(int? id)
        {
            JobAdv jobAdv = conn.JobAdv.FirstOrDefault(a => a.JAid == id);
            jobAdv.state = status.accepted;
            conn.Entry(jobAdv).State = EntityState.Modified;
            conn.SaveChanges();

            return RedirectToAction("Setting");
        }
    }

    }




