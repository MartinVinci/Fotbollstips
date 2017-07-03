using Fotbollstips.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class ResultController : Controller
    {
        public ActionResult Index()
        {
            TipsData emTipsData = DataLogic.GetRattSvarEntity();

            Random rand = new Random();
            ViewBag.Number = rand.Next(999).ToString();

            return View(emTipsData);
        }

        [HttpPost]
        public ActionResult Index(TipsData model)
        {
            var success = DataLogic.SaveCorrectAnswers(model);

            return Index();
        }
    }
}
