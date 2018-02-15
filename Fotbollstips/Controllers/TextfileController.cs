using Fotbollstips.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class TextfileController : Controller
    {
        // GET: Textfile
        public ActionResult Index()
        {
            ViewBag.Result = BusinessLogic.GetFileFromFileStorage();

            return View();
        }
    }
}