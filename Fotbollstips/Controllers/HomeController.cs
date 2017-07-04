using Fotbollstips.Logic;
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
            ViewBag.Message = "Your application description page.";

            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            List<TipsData> tipsData = DataLogic.GetDataForPresentation();

            //tipsData = new List<TipsData>();

            return View(tipsData.ToList());
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

        public ActionResult Participate()
        {
            //TODO Så att man kan lämna in rad
            return View();
        }

        public ActionResult Comment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Comment(TipsComment model)
        {
            var success = DataLogic.SaveComment(model);

            return View();
        }
    }
}