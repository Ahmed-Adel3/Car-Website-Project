using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TechJobsSearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: TechJobsSearch
        public ActionResult getcity(int id)
        {
            List<City> cities = db.City.Where(a => a.Gid == id).ToList<City>();
            SelectList citylist = new SelectList(cities, "CityId", "CityName");
            ViewBag.cities = citylist;
            return PartialView();

        }
        public ActionResult TechAdvIndex()
        {
            List<Governrate> Gevornerats = db.Governrate.ToList<Governrate>();
            SelectList govs = new SelectList(Gevornerats, "Gid", "Gname");
            ViewBag.govs = govs;

            List<City> Cities = db.City.ToList<City>();
            SelectList cities = new SelectList(Cities, "CityId", "CityName");
            ViewBag.cities = cities;

            List<TechJob> Jobs = db.TechJob.ToList<TechJob>();
            SelectList jobs = new SelectList(Jobs, "Jid", "Jname");
            ViewBag.jobs = jobs;

            List<JobAdv> TopJobs = db.JobAdv.Where(a=>a.state==status.accepted).OrderByDescending(x => x.AdDate).Take(9).ToList<JobAdv>();
            ViewBag.TopJobs = TopJobs;
            return View();
        }
        //----------------------------------------------Details--------------------
        public ActionResult jobDetails(int? id)
        {

            JobAdv jobadvDetails = db.JobAdv.FirstOrDefault(a => a.JAid == id);
            var i = db.JobAdv.Where(a => a.JAid == id).Single().ApplicationUser_Id;
            ApplicationUser jobuser = db.Users.Where(a => a.Id == i).Single();
            List<JobAdv> userJobAdvs = db.JobAdv.Where(a => a.ApplicationUser_Id == i).ToList<JobAdv>();
            ViewBag.userJobAdvs = userJobAdvs;
            ViewBag.jobuser = jobuser;
            return PartialView(jobadvDetails);
        }
        //----------------------------------------------Search--------------------
        public ActionResult jobSearch(int? jid, int? gid,int? cityid)
        {
            ViewBag.Jid = new SelectList(db.TechJob, "Jid", "Jname");
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname");
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName");

            var jobadvs = from s in db.JobAdv where s.state == status.accepted
                          select s;

            if (jid != null)
            {
                jobadvs = jobadvs.Where(a => a.Jid == jid);
            }
            if (cityid != null)
            {
                jobadvs = jobadvs.Where(a => a.CityId == cityid);
            }

            if (gid != null)
            {
                jobadvs = jobadvs.Where(a => a.Gid == gid);
            }

            if (jid != null && gid != null&& cityid != null)
            {             
                    jobadvs = jobadvs.Where(a => a.Gid == gid && a.Jid == jid && a.CityId==cityid);

            }
            if (jid == null && gid == null && cityid == null)
            {
                return PartialView("selectitem");

            }
            

             if (jobadvs.Count() == 0 )
             {
                return PartialView("Searchnotfound");
            }
     
            return PartialView(jobadvs);

        }          

        public ActionResult Searchnotfound()
        {
            return PartialView();
        }
        public ActionResult selectitem()
        {
            return PartialView();
        }
    }
}