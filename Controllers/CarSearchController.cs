using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class CarSearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //--------------------CAR SEARCH--------------------------------------

        // GET: CarSearch
        public ActionResult getmodel(int? id)
        {
            List<CarModel> models = db.CarModel.Where(a => a.Mid == id).ToList<CarModel>();
            SelectList modelslist = new SelectList(models, "ModelId", "ModelName");
            ViewBag.models = modelslist;
            return PartialView();

        }
        public ActionResult getcity(int? id)
        {
            List<City> cities = db.City.Where(a => a.Gid == id).ToList<City>();
            SelectList citylist = new SelectList(cities, "CityId", "CityName");
            ViewBag.cities = citylist;
            return PartialView();

        }
        public ActionResult CarIndex()
        {
           
            List<Manufacturer> Manufacturers = db.Manufacturer.ToList<Manufacturer>();
            SelectList Manufacs = new SelectList(Manufacturers, "Mid", "Name");
            ViewBag.Manufacs = Manufacs;


            List<CarModel> Models = db.CarModel.ToList<CarModel>();
            SelectList modelss = new SelectList(Models, "ModelId", "ModelName");
            ViewBag.modelss = modelss;


            List<CarAdv> CarAdverts = db.CarAdv.ToList<CarAdv>();
            ViewBag.CarAdverts = CarAdverts;


            List<Governrate> Gevornerats = db.Governrate.ToList<Governrate>();
            SelectList govs = new SelectList(Gevornerats, "Gid", "Gname");
            ViewBag.govs = govs;

            List<City> Cities = db.City.ToList<City>();
            SelectList cities = new SelectList(Cities, "CityId", "CityName");
            ViewBag.cities = cities;

            List<CarAdv> TopCars = db.CarAdv.Where(a=>a.state ==status.accepted).OrderByDescending(x => x.AdDate).Take(9).ToList<CarAdv>();
            ViewBag.TopCars = TopCars;


            return View();
        }
        
        //--------------------CAR Datails--------------------------------------

        public ActionResult carDetails(int? id)
        {

            CarAdv advDetails = db.CarAdv.FirstOrDefault(a => a.Aid == id);
            List<CarPhoto> carPho = db.CarPhoto.Where(a => a.Aid == id).ToList<CarPhoto>();
            var i = db.CarAdv.Where(a => a.Aid == id).Single().ApplicationUser_Id;
            List<ApplicationUser> user = db.Users.Where(a => a.Id == i).ToList<ApplicationUser>();
            List<CarAdv> userAdvs = db.CarAdv.Where(a => a.ApplicationUser_Id == i).ToList<CarAdv>();
            ViewBag.userAdvs = userAdvs;
            ViewBag.user = user;
            ViewBag.carPho = carPho;
            return View(advDetails);
        }

        public ActionResult HomeSearch(int?mid)
        {
             CarIndex();
          //  carDetails(1);
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name");
            ViewBag.ModelId = new SelectList(db.CarModel, "ModelId", "ModelName");
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname");
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName");
            var caradvs = from s in db.CarAdv
                          where s.state == status.accepted
                          select s;
            caradvs = caradvs.Where(a => a.Mid == mid);
            return View(caradvs);


        }
        //[HttpGet]
        public ActionResult Search(int? mid, int? modelId, int? gid, int? ccfrom,int? ccto ,int? cityid,int? kmfrom,int? kmto,int? pricefrom, int? priceto,string transimition,int? yearfrom,int? yearto,bool? airbag,bool? sunroof,bool? alarm,bool? airdevice,bool? centerlock,bool? radio,bool? electricwindow,bool? abs,bool? power)
        {
            ViewBag.Mid = new SelectList(db.Manufacturer, "Mid", "Name");
            ViewBag.ModelId = new SelectList(db.CarModel, "ModelId", "ModelName");
            ViewBag.Gid = new SelectList(db.Governrate, "Gid", "Gname");
            ViewBag.CityId = new SelectList(db.City, "CityId", "CityName");           
            var caradvs = from s in db.CarAdv where s.state == status.accepted
                       select s;
            if (mid != null)
            {
                caradvs = caradvs.Where(a => a.Mid == mid);
            }
            if (modelId != null)
            {
                caradvs = caradvs.Where(a => a.ModelId == modelId);
            }
            if (ccfrom != null)
            {
                caradvs = caradvs.Where(a => a.Cc >= ccfrom);
            }
            if (ccto != null)
            {
                caradvs = caradvs.Where(a => a.Cc <= ccto);
            }
            if (ccfrom != null && ccto != null)
            {
                caradvs = caradvs.Where(a => a.Cc >= ccfrom &&  a.Cc <= ccto);
            }

            if (gid != null)
            {
                caradvs = caradvs.Where(a => a.Gid == gid);
            }
            if (cityid != null)
            {
                caradvs = caradvs.Where(a => a.CityId == cityid);
            }
            if (pricefrom != null)
            {

                caradvs = caradvs.Where(a => a.Price >= pricefrom);

            }
            if (priceto != null)
            {

                caradvs = caradvs.Where(a => a.Price <= priceto);

            }
            if (pricefrom !=null && priceto!=null)
            {

                caradvs = caradvs.Where(a => a.Price >= pricefrom && a.Price <= priceto);

            }
            if (yearfrom != null)
            {

                caradvs = caradvs.Where(a => a.Year >= yearfrom);

            }
            if (yearto != null)
            {

                caradvs = caradvs.Where(a => a.Year <= yearto);

            }
            if (yearfrom != null && yearto != null)
            {

                caradvs = caradvs.Where(a => a.Year >= yearfrom && a.Year <= yearto);

            }
            if (kmfrom != null )
            {

                caradvs = caradvs.Where(a => a.Km >= kmfrom );

            }
            if ( kmto != null)
            {

                caradvs = caradvs.Where(a => a.Km <= kmto);

            }
            if (kmfrom!=null && kmto!=null)
            {

                caradvs = caradvs.Where(a => a.Km >= kmfrom && a.Km <= kmto);

            }
            if(!string.IsNullOrEmpty(transimition))
            {
                caradvs = caradvs.Where(a => a.Transimition ==transimition);

            }

            ////////////////////// search details
            if (airbag !=null)
            {
                caradvs = caradvs.Where(a => a.AirBag == airbag);
            }
            if (sunroof != null)
            {
                caradvs = caradvs.Where(a => a.SunRoof == sunroof);
            }
            if (alarm != null)
            {
                caradvs = caradvs.Where(a => a.Alarm == alarm);
            }
            if (airdevice != null)
            {
                caradvs = caradvs.Where(a => a.AirDevice == airdevice);
            }
            if (centerlock != null)
            {
                caradvs = caradvs.Where(a => a.CenterLock == centerlock);
            }
            if (radio != null)
            {
                caradvs = caradvs.Where(a => a.Radio == radio);
            }
            if (electricwindow != null)
            {
                caradvs = caradvs.Where(a => a.ElectricWindow == electricwindow);
            }
            if (abs != null)
            {
                caradvs = caradvs.Where(a => a.ABS == abs);
            }
            if (power != null)
            {
                caradvs = caradvs.Where(a => a.Power == power);
            }



            //////////////////////////////////////////////////
            if (mid != null && modelId != null && gid != null &&  ccfrom!=null && ccto != null && cityid != null && kmfrom!=null && kmto!=null && pricefrom!=null &&priceto!=null&& !string.IsNullOrEmpty(transimition)&& yearfrom != null && yearto != null && airbag != null && sunroof != null && alarm != null && airdevice != null && centerlock != null && radio != null && electricwindow != null && abs != null && power != null)
            { 

                caradvs = caradvs.Where(a => a.ModelId == modelId && a.Mid == mid &&a.Cc>=ccfrom &&a.Cc<=ccto &&a.Gid == gid && a.CityId == cityid && a.Km >= kmfrom && a.Km <= kmto && a.Price >= pricefrom && a.Price <= priceto &&a.Transimition==transimition&& a.Year >= yearfrom && a.Year <= yearto && a.AirBag == airbag && a.SunRoof == sunroof && a.Alarm == alarm && a.AirDevice == airdevice && a.CenterLock == centerlock && a.Radio == radio && a.ElectricWindow == electricwindow && a.ABS == abs && a.Power == power ); 

            }
            if (mid == null && modelId == null && gid == null && cityid == null && kmfrom==null && ccfrom==null &&ccto==null && kmto==null &&pricefrom==null && priceto==null && string.IsNullOrEmpty(transimition)&&yearfrom == null && yearto == null && airbag == null && sunroof == null && alarm == null && airdevice == null && centerlock == null && radio == null && electricwindow == null && abs == null && power == null)
            {
                return PartialView("selectitem");


            }


            if (caradvs.Count() == 0)
            {
                return PartialView("Searchnotfound");
            }

            return PartialView(caradvs);

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

