using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Fotbollstips.Logic
{
    public class MailLogic
    {
        public MailLogic()
        {

        }
        public bool SendMail(string emailAddress, string blobUrl, string name)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vmtips2018@gmail.com");
                mail.To.Add(emailAddress);
                mail.Subject = "Din VM-Tipsrad";
                mail.IsBodyHtml = true;
                mail.Body = GetEmailBody(blobUrl, name);

                SmtpServer.Port = 587;
                string password = ConfigurationManager.AppSettings["MailPassword"];

                SmtpServer.Credentials = new System.Net.NetworkCredential("vmtips2018", password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                string logText = string.Format("Email sent to: {0}, name: {1}", emailAddress, name);

                Log4NetLogic.Log(Log4NetLogic.LogLevel.INFO, logText, "SendMail");
            }
            catch (Exception e)
            {
                string logText = string.Format("Email is: {0}. Name: {1}.", emailAddress, name);
                Log4NetLogic.Log(Log4NetLogic.LogLevel.ERROR, logText, "SendMail", e);

                return false;
            }
            return true;
        }

        private static string GetEmailBody(string blobUrl, string name)
        {
            string body = "";

            body += "<h1>Tack för att du deltar i VM-tipset!</h1>";
            body += "<p>På nedanstående länk kan du ladda ner din rad. Glöm inte att betala 50 kr genom ";
            body += "Swish till telefonnummer 0709-632067. Ange ";
            body += string.Format("<strong>{0}</strong>", name);
            body += " som meddelandetext.</p>";
            body += string.Format("<a href=\"{0}\">{1}</a>", blobUrl, blobUrl);
            body += "<br/>";
            body += "<p>Med vänlig hälsning</p><p>Tipsadministratören Martin Nordkvist</p>";
            body += string.Format(@"https://vmtips.azurewebsites.net");

            return body;
        }
    }
}