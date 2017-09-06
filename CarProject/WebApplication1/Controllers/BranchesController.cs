using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BranchesController : Controller
    {
        ApplicationDbContext conn = new ApplicationDbContext();
       // Branch branch = new Branch();

        // GET: Branches
        public ActionResult Index()
        {
            List<Manufacturer> manufactuer = conn.Manufacturer.ToList<Manufacturer>();
            SelectList sel = new SelectList(manufactuer, "Mid", "Name");
            ViewBag.sel = sel;
            List<Governrate> government = conn.Governrate.Take(9).ToList<Governrate>();

            SelectList sel1 = new SelectList(government, "GId", "Gname");
            ViewBag.sel1 = sel1;
            List<Branch> br = conn.Branch.Take(9).ToList<Branch>();
            ViewBag.br = br;

           // branch.Gid= 0;

            // Loading drop down lists.
           // this.ViewBag.CountryList = this.GetCountryList();

            // Info.
            // return this.View(branch);

            return this.View();

        }
        public ActionResult Search(int? mid, int? gid)

        {
            ViewBag.Mid = new SelectList(conn.Manufacturer, "Mid", "Name");
            ViewBag.Gid = new SelectList(conn.Governrate, "Gid", "Gname");
            var branch = from s in conn.Branch
                      
                       select s;

            if (mid != null)
            {
                branch = branch.Where(a => a.Mid == mid);
            }
            if (gid != null)
            {
                branch = branch.Where(a => a.Gid == gid);
            }
            if (mid != null && gid != null)
            {

                branch = branch.Where(a=>a.Mid == mid && a.Gid == gid );

            }
            if (mid == null && gid == null)
            {
                return PartialView("selectitem");


            }
            if (branch.Count() == 0)
            {
                return PartialView("Searchnotfound");
            }

            return PartialView(branch);

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


    #region looding_data_from_txt_file
    //private IEnumerable<SelectListItem> GetCountryList()
    //{
    //    // Initialization.
    //    SelectList lstobj = null;

    //    try
    //    {
    //        // Loading.
    //        var list = this.LoadData()
    //                          .Select(p =>
    //                                    new SelectListItem
    //                                    {
    //                                        Value = p.Gid.ToString(),
    //                                        Text = p.Gname
    //                                    });

    //        // Setting.
    //        lstobj = new SelectList(list, "Value", "Text");
    //    }
    //    catch (Exception ex)
    //    {
    //        // Info
    //        throw ex;
    //    }

    //    // info.
    //    return lstobj;
    //}


    //private List<Governrate> LoadData()
    //{
    //    // Initialization.
    //    List<Governrate> lst = new List<Governrate>();

    //    try
    //    {
    //        // Initialization.
    //        string line = string.Empty;
    //        string srcFilePath = "content/files/country_list.txt";
    //        var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
    //        var fullPath = Path.Combine(rootPath, srcFilePath);
    //        string filePath = new Uri(fullPath).LocalPath;
    //        StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

    //        // Read file.
    //        while ((line = sr.ReadLine()) != null)
    //        {
    //            // Initialization.
    //            Governrate infoObj = new Governrate();
    //            string[] info = line.Split(',');

    //            // Setting.
    //            infoObj.Gid = Convert.ToInt32(info[0].ToString());
    //            infoObj.Gname = info[1].ToString();

    //            // Adding.
    //            lst.Add(infoObj);
    //        }

    //        // Closing.
    //        sr.Dispose();
    //        sr.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        // info.
    //        Console.Write(ex);
    //    }

    //    // info.
    //    return lst;
    //}
    #endregion
}
