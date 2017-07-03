using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new MartinDatabaseEntities())
            {
                var result = (from hits in db.TipsDatas
                              select hits).ToList();

                foreach (var item in result)
                {
                    ViewBag.Message = "Your application description page.";

                    ViewBag.Hej = item.Email;
                }

            }



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}