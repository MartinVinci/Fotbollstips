using Fotbollstips.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            List<TipsData> tipsData = DataLogic.GetDataForPresentation();

            List<TipsData> tipsDataWithPaymentFalse =
                (from hits in tipsData
                 where hits.HasPayed == false
                 select hits).ToList();

            return View(tipsDataWithPaymentFalse.ToList());
        }

        [HttpPost]
        public ActionResult Index(List<TipsData> model)
        {
            List<int> newPayments = new List<int>();

            foreach (TipsData data in model)
            {
                if ((bool)data.HasPayed)
                {
                    newPayments.Add(data.Id);
                }
            }

            DataLogic.SaveUpdatedPaymentStatus(newPayments);

            return Index();
        }
    }
}
