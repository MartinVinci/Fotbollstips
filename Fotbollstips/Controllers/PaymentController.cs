using Fotbollstips.Logic;
using Fotbollstips.Objects;
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

            string latestUpdate = BusinessLogic.GetRandomValue("PayedLatestUpdate");
            string sendSms = BusinessLogic.GetRandomValue("SendSms");

            PaymentObject paymentObject = new PaymentObject(tipsDataWithPaymentFalse, latestUpdate, sendSms);

            //return View(tipsDataWithPaymentFalse.ToList());
            return View(paymentObject);
        }

        [HttpPost]
        public ActionResult Index(PaymentObject model)
        {
            List<int> newPayments = new List<int>();

            BusinessLogic.UpdateRandomValue("PayedLatestUpdate", model.LatestDate);
            BusinessLogic.UpdateRandomValue("SendSms", model.SendSms);

            foreach (TipsData data in model.Tipsdata)
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
