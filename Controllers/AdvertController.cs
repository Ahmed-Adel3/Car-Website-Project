using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AdvertController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        #region

        // Brand drop dowen lest
        List<Manufacturer> Brand;
        SelectList BrandsList;
        // end of Drop down list

        // Accessory Type Dropdown list 
        List<Accessory> access;
        SelectList accessList;

        // Governrate Type Dropdown list 
        List<Governrate> Gove;
        SelectList GoveList;
        // end of Drop down list

        // City Type Dropdown list 
          List<City> CityData;
         SelectList CityList;
        // end of Drop down list

        // Jobs Type Dropdown list 
        List<TechJob> Job;
        SelectList JobList;
        // end of Drop down list


        // Car_ model Type Dropdown list 
        List<CarModel> car_model;
        SelectList car_modelist;
        // end of Drop down list


        #endregion

        // GET: Advert
        public ActionResult getmodel(int? id)
        {
            List<CarModel> models = db.CarModel.Where(a => a.Mid == id).ToList<CarModel>();
            SelectList modelslist = new SelectList(models, "ModelId", "ModelName");
            ViewBag.models = modelslist;
            return PartialView();

        }
        public ActionResult getcity(int id)
        {
            List<City> cities = db.City.Where(a => a.Gid == id).ToList<City>();
            SelectList citylist = new SelectList(cities, "CityId", "CityName");
            ViewBag.cities = citylist;
            return PartialView();

        }


        [HttpGet]
        public ActionResult CarAdvert()
        {

            // Brand Dropdown list
            Brand = db.Manufacturer.ToList<Manufacturer>();
            BrandsList = new SelectList(Brand, "Mid", "Name");
            ViewBag.BrandsList = BrandsList;
            //end of Dropdown list

            // Governorates Drop down list
            Gove = db.Governrate.ToList<Governrate>();
            GoveList = new SelectList(Gove, "Gid", "Gname");
            ViewBag.GoveList = GoveList;
            // end of Drop down list

            // Cityes Drop down list
             CityData = db.City.ToList<City>();
            CityList = new SelectList(CityData, "CityId", "CityName");
            ViewBag.CityList = CityList;
            // end of Drop down list


            // Car_model Drop down list

            car_model = db.CarModel.ToList<CarModel>();
            car_modelist = new SelectList(car_model, "ModelId", "ModelName");
            ViewBag.car_modelist = car_modelist;

            // end of Drop down list



            return View();
        }


        [HttpPost]
        public ActionResult CarAdvert(CarAdv cadv, HttpPostedFileBase imag1, HttpPostedFileBase imag2, HttpPostedFileBase imag3)
        {
            cadv.ApplicationUser_Id = User.Identity.GetUserId();
            CarPhoto[] carphoto = new CarPhoto[3];
            cadv.AdDate = DateTime.Now;
           
            for (int i = 0; i < 3; i++)
            {
                carphoto[i] = new CarPhoto();

               if (i == 0 && imag1!= null)
                {
                    carphoto[i].Aid = cadv.Aid;
                    imag1.SaveAs(Server.MapPath("~/images/ads/car/") + imag1.FileName);
                    carphoto[i].CPhoto = "/images/ads/car/" + imag1.FileName;
                    db.CarPhoto.Add(carphoto[i]); 
                }
            else if (i == 1 && imag2!= null)
                {

                    carphoto[i].Aid = cadv.Aid;
                    imag2.SaveAs(Server.MapPath("~/images/ads/car/") + imag2.FileName);
                    carphoto[i].CPhoto = "/images/ads/car/" + imag2.FileName;
                    db.CarPhoto.Add(carphoto[i]);

                }

              else if (i==2 && imag3!=null)
                {

                   carphoto[i].Aid = cadv.Aid;
                    imag3.SaveAs(Server.MapPath("~/images/ads/car/") + imag3.FileName);
                    carphoto[i].CPhoto = "/images/ads/car/" + imag3.FileName;
                    db.CarPhoto.Add(carphoto[i]);

                }

            }


            if (ModelState.IsValid == true)
            {
                cadv.state = status.waiting ;
                db.CarAdv.Add(cadv);
               db.SaveChanges();
                return View("Done");
            }
            else
            {
                return View(cadv);
            }
        }


       [HttpGet]
       // this id represent User id  3la ma bna y7laha
        public ActionResult AccessoriesAdvert ()
        {
           
            // Brand Dropdown list
            Brand = db.Manufacturer.ToList<Manufacturer>();
            BrandsList = new SelectList(Brand, "Mid", "Name");
            ViewBag.BrandsList = BrandsList;
            // end Brand Dropdown list

            access = db.Accessory.ToList<Accessory>();
            accessList = new SelectList(access, "AccTypeId", "AccTypeName");
            ViewBag.accessoryList = accessList;
            // end of Accessory Dropdown list

            // Governorates Drop down list
            Gove = db.Governrate.ToList<Governrate>();
            GoveList = new SelectList(Gove, "Gid", "Gname");
            ViewBag.GoveList = GoveList;
            // end of Drop down list

            // Cityes Drop down list
            CityData = db.City.ToList<City>();
            CityList = new SelectList(CityData, "CityId", "CityName");
            ViewBag.CityList = CityList;
            // end of Drop down list

            return View();
        }

        [HttpPost]
        public ActionResult AccessoriesAdvert(AccessoriesAdv accessAdvert, HttpPostedFileBase image)
        {
            accessAdvert.ApplicationUser_Id = User.Identity.GetUserId();

            image.SaveAs(Server.MapPath("~/images/ads/Accessory/") + image.FileName);
            accessAdvert.Aphoto= "/images/ads/Accessory/" + image.FileName;
            accessAdvert.AdDate = DateTime.Now;
            if (ModelState.IsValid == true)
            {
                accessAdvert.state = status.waiting;
                db.AccessoriesAdv.Add(accessAdvert);
                db.SaveChanges();
                return View("Done");

            }

            else
            {
                return View(accessAdvert);

            }
        }




        [HttpGet]
        public ActionResult technicianAdvert()
        {
            // Job Dropdown list
            Job = db.TechJob.ToList<TechJob>();
            JobList = new SelectList(Job, "Jid", "Jname");
            ViewBag.JobList = JobList;
            //end of Dropdown list

            // Governorates Drop down list
            Gove = db.Governrate.ToList<Governrate>();
            GoveList = new SelectList(Gove, "Gid", "Gname");
            ViewBag.GoveList = GoveList;
            // end of Drop down list

            // Cityes Drop down list
            CityData = db.City.ToList<City>();
            CityList = new SelectList(CityData, "CityId", "CityName");
            ViewBag.CityList = CityList;
            // end of Drop down list

            return View();
        }

        [HttpPost]
        public ActionResult technicianAdvert( JobAdv JOB)
        {
            JOB.ApplicationUser_Id = User.Identity.GetUserId();
            JOB.AdDate = DateTime.Now;
            if (ModelState.IsValid== true)
            {
                JOB.state = status.waiting;
                db.JobAdv.Add(JOB);
                db.SaveChanges();
                return View("Done");
            }
            else
            {
                return View(JOB);
            }
        }



        // dumy Page
        public ActionResult Done()
        {


            return View();
        }



    }
}