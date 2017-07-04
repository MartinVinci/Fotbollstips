using Fotbollstips.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class CommentController : Controller
    {
        public ActionResult Index()
        {
            List<TipsComment> comments = DataLogic.GetComments();

            return View(comments.ToList());
        }
    }
}
