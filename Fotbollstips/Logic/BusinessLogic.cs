using Fotbollstips.Objects;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotbollstips.Logic
{
    public class BusinessLogic
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(BusinessLogic));
        public static List<TipsData> GetDataForPresentation()
        {
            return DataLogic.GetDataForPresentation();
        }

        public static List<TipsData> RemoveSensitiveData(List<TipsData> tipsData)
        {
            var returnList = new List<TipsData>();

            foreach (var tips in tipsData)
            {
                var newItem = new TipsData();
                newItem.Namn = tips.Namn;
                newItem.HasPayed = tips.HasPayed;

                returnList.Add(newItem);
            }

            return returnList;
        }

        public static ParticipateResult ParticipateInTips(FormCollection col)
        {
            try
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

                    Argentina_Island = GetGameResult(col, "argisl"),
                    Argentina_Kroatien = GetGameResult(col, "argkro"),
                    Australien_Peru = GetGameResult(col, "ausper"),
                    Belgien_Panama = GetGameResult(col, "belpan"),
                    Belgien_Tunisien = GetGameResult(col, "beltun"),
                    Brasilien_CostaRica = GetGameResult(col, "bracos"),
                    Brasilien_Schweiz = GetGameResult(col, "brasch"),
                    Colombia_Japan = GetGameResult(col, "coljap"),
                    CostaRica_Serbien = GetGameResult(col, "cosser"),
                    Danmark_Australien = GetGameResult(col, "danaus"),
                    Danmark_Frankrike = GetGameResult(col, "danfra"),
                    Egypten_Uruguay = GetGameResult(col, "egyuru"),
                    England_Belgien = GetGameResult(col, "engbel"),
                    England_Panama = GetGameResult(col, "engpan"),
                    Frankrike_Australien = GetGameResult(col, "fraaus"),
                    Frankrike_Peru = GetGameResult(col, "fraper"),
                    Iran_Portugal = GetGameResult(col, "irapor"),
                    Iran_Spanien = GetGameResult(col, "iraspa"),
                    Island_Kroatien = GetGameResult(col, "islkro"),
                    Japan_Polen = GetGameResult(col, "jappol"),
                    Japan_Senegal = GetGameResult(col, "japsen"),
                    Kroatien_Nigeria = GetGameResult(col, "kronig"),
                    Marocko_Iran = GetGameResult(col, "marira"),
                    Mexiko_Sverige = GetGameResult(col, "mexsve"),
                    Nigeria_Argentina = GetGameResult(col, "nigarg"),
                    Nigeria_Island = GetGameResult(col, "nigisl"),
                    Panama_Tunisien = GetGameResult(col, "pantun"),
                    Peru_Danmark = GetGameResult(col, "perdan"),
                    Polen_Colombia = GetGameResult(col, "polcol"),
                    Polen_Senegal = GetGameResult(col, "polsen"),
                    Portugal_Marocko = GetGameResult(col, "pormar"),
                    Portugal_Spanien = GetGameResult(col, "porspa"),
                    Ryssland_Egypten = GetGameResult(col, "rysegy"),
                    Ryssland_Saudiarabien = GetGameResult(col, "ryssau"),
                    Saudiarabien_Egypten = GetGameResult(col, "sauegy"),
                    Schweiz_CostaRica = GetGameResult(col, "schcos"),
                    Senegal_Colombia = GetGameResult(col, "sencol"),
                    Serbien_Brasilien = GetGameResult(col, "serbra"),
                    Serbien_Schweiz = GetGameResult(col, "sersch"),
                    Spanien_Marocko = GetGameResult(col, "spamar"),
                    Sverige_Sydkorea = GetGameResult(col, "svesyd"),
                    Sydkorea_Mexiko = GetGameResult(col, "sydmex"),
                    Sydkorea_Tyskland = GetGameResult(col, "sydtys"),
                    Tunisien_England = GetGameResult(col, "tuneng"),
                    Tyskland_Mexico = GetGameResult(col, "tysmex"),
                    Tyskland_Sverige = GetGameResult(col, "tyssve"),
                    Uruguay_Ryssland = GetGameResult(col, "ururys"),
                    Uruguay_Saudiarabien = GetGameResult(col, "urusau")
                };

                // Save to database
                var saveResultOfTipsData = DataLogic.SaveNewTipsData(newTipsData);

                bool emailSent = false;

                if (saveResultOfTipsData.SuccessedSave)
                {
                    // Create PDF
                    var pdfWorder = new PdfLogic();
                    PdfDocument pdfDocument = pdfWorder.SaveTipsDatas(newTipsData);

                    #region During development
                    //// During development - Store locally on computer

                    //string filePath = GetFileNameAndPath("", false);
                    //pdfDocument.Save(filePath);

                    //Process.Start(filePath);

                    //return true;
                    #endregion
                    if (pdfDocument != null)
                    {
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

                        //// Send email
                        //string getEmail = col["getemail"];

                        ////MNTODO Ta bort if vid deploy
                        //if (getEmail.ToLower() == "ja")
                        //{
                        var mailWorker = new MailLogic();
                        emailSent = mailWorker.SendMail(newTipsData.Email, pathToPdf.PathToPDF, col["myname"]);
                        //}
                    }
                }
                return new ParticipateResult(saveResultOfTipsData.SuccessedSave, emailSent, newTipsData.Email);
            }
            catch (Exception e)
            {
                string inner = e.InnerException == null ? "NULL" : e.InnerException.ToString();

                log.Error(string.Format("Error in ParticipateInTips method, Inner: {0}.", inner), e);

                return new ParticipateResult(false, false, "");
            }
        }

        public static string GetRandomValue(string name)
        {
            return DataLogic.GetRandomValue(name);
        }
        public static bool UpdateRandomValue(string name, string value)
        {
            return DataLogic.UpdateRandomValue(name, value);
        }
        private static string GetFileNameAndPath(string type, bool tomorrow)
        {
            DateTime date = DateTime.Now;

            string time = string.Format(date.ToString());
            time = time.Replace(':', '-');
            time = time.Replace(' ', '_');
            string pdfFilename = string.Format("{0}.pdf", time);

            string filePath = "";

            filePath = @"C:\Tips\" + "Tips_" + pdfFilename;

            return filePath;
        }

        private static string GetGameResult(FormCollection col, string game)
        {
            string home = game + "1";
            string away = game + "2";

            return col[home] + col[away];
        }

        public static string GetFileFromFileStorage()
        {
            var blobWorker = new BlobStorageLogic();
            return blobWorker.GetFileFromFileStorage();
        }

    }
}