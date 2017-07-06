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
            ViewBag.ParticipateResultSuccess = null;
            ViewBag.ParticipateResultFail = null;

            return View();
        }

        [HttpPost]
        public ActionResult Participate(FormCollection col)
        {
            //string name = col["myname"];
            //string phoneNumber = col["myphonenumber"];
            //string email = col["myemail"];

            //string finalTeam1 = col["finalteam1"];
            //string finalTeam2 = col["finalteam2"];
            //string winner = col["winner"];

            //string svekam = GetGameResult(col, "svekam");
            //string rysbra = GetGameResult(col, "rysbra");
            //string kambra = GetGameResult(col, "kambra");
            //string sverys = GetGameResult(col, "sverys");

            TipsData newTipsData = new TipsData()
            {
                EntryDate = DateTime.UtcNow,
                Poäng = 0,
                HasPayed = false,

                Namn = col["myname"],
                PhoneNumber = col["myphonenumber"],
                Email = col["myemail"],

                Finallag1 = col["finalteam1"],
                Finallag2 = col["finalteam2"],
                Vinnare = col["winner"],

                Sverige_Kamerun = GetGameResult(col, "svekam"),
                Ryssland_Brasilien = GetGameResult(col, "rysbra"),
                Kamerun_Brasilien = GetGameResult(col, "kambra"),
                Sverige_Ryssland = GetGameResult(col, "sverys")
            };

            var success = DataLogic.SaveNewTipsData(newTipsData);

            if (success)
            {
                ModelState.Clear();
                ViewBag.ParticipateResultSuccess = "Tack för din rad!";
                ViewBag.ParticipateResultFail = null;
            }
            else
            {
                ViewBag.ParticipateResultSuccess = null;
                ViewBag.ParticipateResultFail = "Något gick fel! Dessvärre har inte din rad sparats, du måste göra om det :-(";
            }

            return View();
        }

        private static string GetGameResult(FormCollection col, string game)
        {
            string home = game + "1";
            string away = game + "2";

            return col[home] + col[away];
        }

        public ActionResult Comment()
        {
            ViewBag.ThankYouForYourComment = null;

            return View();
        }

        [HttpPost]
        public ActionResult Comment(TipsComment model)
        {
            var success = DataLogic.SaveComment(model);

            ModelState.Clear();

            ViewBag.ThankYouForYourComment = "Tack för din kommentar!";

            return View();
        }
    }
}