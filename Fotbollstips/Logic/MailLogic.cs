﻿using System;
using System.Collections.Generic;
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
        public bool SendMail(string emailAddress, string blobUrl)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vmtips2018@gmail.com");
                mail.To.Add(emailAddress);
                mail.Subject = "Din VM-Tipsrad";
                //mail.Body = "This is for testing SMTP mail from GMAIL";
                mail.IsBodyHtml = true;
                mail.Body = GetEmailBody(blobUrl);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vmtips2018", "S4onybut!1");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        
        private static string GetEmailBody(string blobUrl)
        {
            string body = "";

            body += "<h1>Tack för att du deltar i VM-tipset!</h1>";
            body += "<p>På nedanstående länk kan du ladda ner din rad. Glöm inte att betala 50 kr genom ";
            body += "Swish till telefonnummer xxxx-xxxxxx.</p>";
            body += string.Format("<p>{0}</p>",blobUrl);
            body += "<br/>";
            body += "<p>Med vänlig hälsning</p><p>Tipsadministratören Martin</p>";
            body += string.Format(@"https://vmtips.azurewebsites.net");

            return body;
        }
    }
}