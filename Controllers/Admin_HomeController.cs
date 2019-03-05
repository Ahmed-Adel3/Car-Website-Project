using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Users = "ADMIN")]
    public class Admin_HomeController : Controller
    {

        // GET: Admin_Home
        public ActionResult Index()
        {
            return View();
        }

    }
}