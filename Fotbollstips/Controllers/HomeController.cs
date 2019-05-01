using Fotbollstips.Logic;
using log4net;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var tournamentStart = Convert.ToDateTime(ConfigurationManager.AppSettings["TournamentStartTime"]);

            List<TipsData> tipsData = BusinessLogic.GetDataForPresentation();

            ViewBag.LatestDate = BusinessLogic.GetRandomValue("PayedLatestUpdate");

            if (tournamentStart > DateTime.Now)
            {
                tipsData = BusinessLogic.RemoveSensitiveData(tipsData);
                tipsData = tipsData.OrderBy(o => o.Namn).ToList();

                return View("PreTournament", tipsData);
            }
            return View(tipsData.ToList());
        }

        //public ActionResult About()
        //{
        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        public ActionResult Participate()
        {
            ViewBag.ParticipateResultSuccess = null;
            ViewBag.ParticipateResultFail = null;

            var tournamentStart = Convert.ToDateTime(ConfigurationManager.AppSettings["TournamentStartTime"]);

            if (tournamentStart < DateTime.Now)
            {
                return View("ParticipateToLate");
            }

            return View();
        }


        [HttpPost]
        public ActionResult Participate(FormCollection col)
        {
            var tournamentStart = Convert.ToDateTime(ConfigurationManager.AppSettings["TournamentStartTime"]);

            if (tournamentStart < DateTime.Now)
            {
                return View("ParticipateToLate");
            }
            else
            {
                var saveResult = BusinessLogic.ParticipateInTips(col);

                if (saveResult.SavedInDatabase)
                {
                    ModelState.Clear();
                    ViewBag.ParticipateResultSuccess = "Tack för din rad!";
                    ViewBag.ParticipateResultFail = null;

                    ViewBag.ReminderSwish = "Glöm inte att betala in via swish!";
                }
                else
                {
                    ViewBag.ParticipateResultSuccess = null;
                    ViewBag.ParticipateResultFail = "Något gick fel! Dessvärre har inte din rad sparats, du måste göra om det :-(";
                }

                if (saveResult.EmailSent)
                {
                    ViewBag.ParticipateEmailSuccess = "Ett mail är skickat till din e-postadress.";
                    ViewBag.ParticipateEmailFail = null;
                }
                else
                {
                    ViewBag.ParticipateEmailSuccess = null;
                    ViewBag.ParticipateEmailFail = string.Format("Något gick fel när ett mail skulle skickas till dig. Adressen du angav: '{0}'.", saveResult.EmailAddress);
                }
            }

            return View();
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