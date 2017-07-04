using Fotbollstips.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Index()
        {
            List<TipsError> errors = DataLogic.GetErrors();

            return View(errors.ToList());
        }
    }
}
