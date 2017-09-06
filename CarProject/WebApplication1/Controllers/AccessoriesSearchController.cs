using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccessoriesSearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccessoriesSearch
        public ActionResult getcity(int id)
        {
            List<City> cities = db.City.Where(a => a.Gid == id).ToList<City>();
            SelectList citylist = new SelectList(cities, "CityId", "CityName");
            ViewBag.cities = citylist;
            return PartialView();

        }
        public ActionResult AccessoriesIndex()
        {
            List<Manufacturer> Manufacturers = db.Manufacturer.ToList<Manufacturer>();
            SelectList Manufacs = new SelectList(Manufacturers, "Mid", "Name");
            ViewBag.Manufacs = Manufacs;


            List<Accessory> Access = db.Accessory.ToList<Accessory>();
            SelectList access = new SelectList(Access, "AccTypeId", "AccTypeName");
            ViewBag.access = access;

            List<Governrate> Gevornerats = db.Governrate.ToList<Governrate>();
            SelectList govs = new SelectList(Gevornerats, "Gid", "Gname");
            ViewBag.govs = govs;

            List<City> Cities = db.City.ToList<City>();
            SelectList cities = new SelectList(Cities, "CityId", "CityName");
            ViewBag.cities = cities;

            List<AccessoriesAdv> TopAccess = db.AccessoriesAdv.Where(a=>a.state==status.accepted).OrderByDescending(x => x.AdDate).Take(9).ToList<AccessoriesAdv>();
            ViewBag.TopAccess = TopAccess;


            return View();


        }
        //----------------------------------------------Details--------------------
        public ActionResult accDetails(int? id)
        {

            AccessoriesAdv accadvDetails = db.AccessoriesAdv.FirstOrDefault(a => a.AccId == id);
            var i = db.AccessoriesAdv.Where(a => a.AccId== id).Single().ApplicationUser_Id;
            ApplicationUser accuser = db.Users.Where(a => a.Id == i).Single();
            List<AccessoriesAdv> userAccAdvs = db.AccessoriesAdv.Where(a => a.ApplicationUser_Id == i).ToList<AccessoriesAdv>();
            ViewBag.userAccAdvs = userAccAdvs;
            ViewBag.accuser = accuser;
            return PartialView(accadvDetails);
        }
        //----------------------------------------------Search--------------------

        public ActionResult accSearch(int? acctypeid, int? mid ,int? gid, int? cityid,int? pricefrom,int? priceto,string condition)
        {

            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name");
            ViewBag.AccTypeId = new SelectList(db.Accessory, "AccTypeId", "AccTypeName");
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname");
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName");

            var advs =from s in db.AccessoriesAdv where s.state==status.accepted
                    select s;
            

            if (acctypeid != null)
            {
                advs = advs.Where(a => a.AccTypeId == acctypeid);
            }
            if (mid != null)
            {
                advs = advs.Where(a => a.Mid == mid);
            }
            if (gid != null)
            {
                advs = advs.Where(a => a.Gid== gid);
            }
            if (cityid != null)
            {
                advs = advs.Where(a => a.CityId == cityid);
            }

            if (priceto != null)
            {
                advs = advs.Where(a => a.Price <= priceto);
            }
            if (pricefrom != null)
            {
                advs = advs.Where(a => a.Price >= pricefrom);
            }


            if (pricefrom!=null && priceto!=null)
            {

                advs = advs.Where(a => a.Price >= pricefrom && a.Price <= priceto);

            }
            if(!string.IsNullOrEmpty(condition))
            {
                advs = advs.Where(a => a.Status==condition);

            }
           
            if (mid != null && acctypeid != null && gid != null && cityid != null && pricefrom!=null && priceto!=null && !string.IsNullOrEmpty(condition))
            {

                advs = advs.Where(a => a.AccTypeId == acctypeid && a.Mid == mid && a.Gid == gid && a.CityId == cityid && a.Price >= pricefrom && a.Price <= priceto &&a.Status==condition);

            }
            if (mid == null && acctypeid == null && gid == null && cityid == null && pricefrom==null && priceto==null &&  string.IsNullOrEmpty(condition))
            {
                return PartialView("selectitem");


            }
            if (advs.Count() == 0)
            {
                return PartialView("Searchnotfound");
            }

            return PartialView(advs);

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




