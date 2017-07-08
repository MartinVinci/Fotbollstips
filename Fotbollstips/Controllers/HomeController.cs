using Fotbollstips.Logic;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class HomeController : Controller
    {
        // TODOMASTER
        // Bättre PDF med swishinformation
        // Skicka mail

        // Hoppas att TODO - TaODO inte spelar någon roll


        public ActionResult Index()
        {
            //TODO ta bort testmetod
            //Test();
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

        private static void Test()
        {
            TipsData newTipsData = new TipsData()
            {
                EntryDate = DateTime.UtcNow,
                Poäng = 0,
                HasPayed = false,

                Namn = "Martin2",
                PhoneNumber = "040-123456",
                Email = "test@test.com",

                Finallag1 = "Sverige",
                Finallag2 = "Brasilien",
                Vinnare = "Sverige",

                Sverige_Kamerun = "00",
                Ryssland_Brasilien = "12",
                Kamerun_Brasilien = "32",
                Sverige_Ryssland = "21"
            };

            // Save to database
            var saveResultOfTipsData = DataLogic.SaveNewTipsData(newTipsData);

            // Create PDF
            var pdfWorder = new PdfLogic();
            PdfDocument pdfDocument = pdfWorder.SaveTipsDatas(newTipsData);

            // Store in blob storage
            var blobWorker = new BlobStorageLogic();
            string imagePath = blobWorker.SavePDF(pdfDocument, newTipsData.Namn);

            // Save file path to PDF
            TipsPathToPDF pathToPdf = new TipsPathToPDF()
            {
                PathToPDF = imagePath,
                TipsData_SoftFK = saveResultOfTipsData.IdOfTipsdata
            };

            var imagePathSaved = DataLogic.SaveNewTipsDataImagePath(pathToPdf); ;
        }

        [HttpPost]
        public ActionResult Participate(FormCollection col)
        {
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

            // Save to database
            var saveResultOfTipsData = DataLogic.SaveNewTipsData(newTipsData);

            // Create PDF
            var pdfWorder = new PdfLogic();
            PdfDocument pdfDocument = pdfWorder.SaveTipsDatas(newTipsData);

            // Store in blob storage
            var blobWorker = new BlobStorageLogic();
            string imagePath = blobWorker.SavePDF(pdfDocument, newTipsData.Namn);

            // Save file path to PDF
            TipsPathToPDF pathToPdf = new TipsPathToPDF()
            {
                PathToPDF = imagePath,
                TipsData_SoftFK = saveResultOfTipsData.IdOfTipsdata
            };

            var imagePathSaved = DataLogic.SaveNewTipsDataImagePath(pathToPdf);
            
            // Send email
            string getEmail = col["getemail"];
            if (getEmail.ToLower() == "ja")
            {
                var sendEmail = true;
            }

            if (saveResultOfTipsData.SuccessedSave)
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