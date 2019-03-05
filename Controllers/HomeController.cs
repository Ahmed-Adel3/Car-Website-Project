using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        #region old
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        #endregion

        ApplicationDbContext Cadv = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<CarAdv> Home_Cars = Cadv.CarAdv.OrderByDescending(x => x.Aid).Where(a=>a.state == status.accepted).Take(6).ToList<CarAdv>();
            return View(Home_Cars);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Action()
        {
            return View();
        }


        public ActionResult _404Page()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Action(Contactus contact)
        {

            if (ModelState.IsValid == true)
            {
                Cadv.Contactus.Add(contact);
                Cadv.SaveChanges();
                return View();
            }
            else
            {
                return View();
            }
        }

    }

}