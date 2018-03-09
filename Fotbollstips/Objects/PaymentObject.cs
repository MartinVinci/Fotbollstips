using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotbollstips.Objects
{
    public class PaymentObject
    {
        public List<TipsData> Tipsdata { get; set; }
        public string LatestDate { get; set; }
        public string SendSms { get; set; }

        public PaymentObject(List<TipsData> tipsdata, string latestdate, string sendSms)
        {
            Tipsdata = tipsdata;
            LatestDate = latestdate;
            SendSms = sendSms;
        }
        public PaymentObject()
        {

        }
    }
}