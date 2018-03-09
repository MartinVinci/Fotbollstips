using Fotbollstips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotbollstips.Logic
{
    public class DataLogic
    {
        public static string RATT_SVAR = "Rätt svar";

        public static List<TipsData> GetDataForPresentation()
        {
            try
            {
                // Get raw data
                List<TipsData> rawData = GetTipsRawData();

                // Calculate points
                List<TipsData> calculatedPoints = CalculatePoints(rawData);

                // Sort list by points
                List<TipsData> sortedList = calculatedPoints.OrderByDescending(o => o.Poäng).ToList();

                // Manipulate text string 
                List<TipsData> returnList = ManipulateTextStrings(sortedList);

                return returnList;
            }
            catch (Exception e)
            {
                Log4NetLogic.Log(Log4NetLogic.LogLevel.ERROR, "An error in", "GetDataForPresentation", e);

                return null;
            }
        }

        public static string GetRandomValue(string name)
        {
            try
            {
                using (var db = new FotbollsTipsModel())
                {
                    var result = (from hits in db.TipsRandomValues
                                  where hits.Name == name
                                  select hits).FirstOrDefault();

                    return result.Value;
                }
            }
            catch (Exception e)
            {
                Log4NetLogic.Log(Log4NetLogic.LogLevel.ERROR, "An error in", "GetRandomValue", e);

                throw;
            }

        }
        public static bool UpdateRandomValue(string name, string value)
        {
            try
            {
                using (var db = new FotbollsTipsModel())
                {
                    var result = (from hits in db.TipsRandomValues
                                  where hits.Name == name
                                  select hits).FirstOrDefault();

                    result.Value = value;
                    result.Modified = DateTime.Now;

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                Log4NetLogic.Log(Log4NetLogic.LogLevel.ERROR, "An error in", "UpdateRandomValue", e);

                return false;
            }

        }

        private static List<TipsData> GetTipsRawData()
        {
            using (var db = new FotbollsTipsModel())
            {
                var result = (from hits in db.TipsDatas
                              select hits).ToList();

                return result;
            }
        }

        public static TipsData GetRattSvarEntity()
        {
            var result = GetTipsRawData();

            var output = (from hits in result
                          where hits.Namn == RATT_SVAR
                          select hits).FirstOrDefault();

            return output;
        }


        private static List<TipsData> CalculatePoints(List<TipsData> data)
        {
            var correct = (from hits in data
                           where hits.Namn == RATT_SVAR
                           select hits).FirstOrDefault();

            foreach (var item in data)
            {
                if (item.Namn == RATT_SVAR)
                {
                    item.Poäng = 51;
                    continue;
                }
                int points = 0;

                #region compare games

                if (item.Argentina_Island == correct.Argentina_Island)
                    points++;

                if (item.Argentina_Kroatien == correct.Argentina_Kroatien)
                    points++;

                if (item.Australien_Peru == correct.Australien_Peru)
                    points++;

                if (item.Belgien_Panama == correct.Belgien_Panama)
                    points++;

                if (item.Belgien_Tunisien == correct.Belgien_Tunisien)
                    points++;

                if (item.Brasilien_CostaRica == correct.Brasilien_CostaRica)
                    points++;

                if (item.Brasilien_Schweiz == correct.Brasilien_Schweiz)
                    points++;

                if (item.Colombia_Japan == correct.Colombia_Japan)
                    points++;

                if (item.CostaRica_Serbien == correct.CostaRica_Serbien)
                    points++;

                if (item.Danmark_Australien == correct.Danmark_Australien)
                    points++;

                if (item.Danmark_Frankrike == correct.Danmark_Frankrike)
                    points++;

                if (item.Egypten_Uruguay == correct.Egypten_Uruguay)
                    points++;

                if (item.England_Belgien == correct.England_Belgien)
                    points++;

                if (item.England_Panama == correct.England_Panama)
                    points++;

                if (item.Frankrike_Australien == correct.Frankrike_Australien)
                    points++;

                if (item.Frankrike_Peru == correct.Frankrike_Peru)
                    points++;

                if (item.Iran_Portugal == correct.Iran_Portugal)
                    points++;

                if (item.Iran_Spanien == correct.Iran_Spanien)
                    points++;

                if (item.Island_Kroatien == correct.Island_Kroatien)
                    points++;

                if (item.Japan_Polen == correct.Japan_Polen)
                    points++;

                if (item.Japan_Senegal == correct.Japan_Senegal)
                    points++;

                if (item.Kroatien_Nigeria == correct.Kroatien_Nigeria)
                    points++;

                if (item.Marocko_Iran == correct.Marocko_Iran)
                    points++;

                if (item.Mexiko_Sverige == correct.Mexiko_Sverige)
                    points++;

                if (item.Nigeria_Argentina == correct.Nigeria_Argentina)
                    points++;

                if (item.Nigeria_Island == correct.Nigeria_Island)
                    points++;

                if (item.Panama_Tunisien == correct.Panama_Tunisien)
                    points++;

                if (item.Peru_Danmark == correct.Peru_Danmark)
                    points++;

                if (item.Polen_Colombia == correct.Polen_Colombia)
                    points++;

                if (item.Polen_Senegal == correct.Polen_Senegal)
                    points++;

                if (item.Portugal_Marocko == correct.Portugal_Marocko)
                    points++;

                if (item.Portugal_Spanien == correct.Portugal_Spanien)
                    points++;

                if (item.Ryssland_Egypten == correct.Ryssland_Egypten)
                    points++;

                if (item.Ryssland_Saudiarabien == correct.Ryssland_Saudiarabien)
                    points++;

                if (item.Saudiarabien_Egypten == correct.Saudiarabien_Egypten)
                    points++;

                if (item.Schweiz_CostaRica == correct.Schweiz_CostaRica)
                    points++;

                if (item.Senegal_Colombia == correct.Senegal_Colombia)
                    points++;

                if (item.Serbien_Brasilien == correct.Serbien_Brasilien)
                    points++;

                if (item.Serbien_Schweiz == correct.Serbien_Schweiz)
                    points++;

                if (item.Spanien_Marocko == correct.Spanien_Marocko)
                    points++;

                if (item.Sverige_Sydkorea == correct.Sverige_Sydkorea)
                    points++;

                if (item.Sydkorea_Mexiko == correct.Sydkorea_Mexiko)
                    points++;

                if (item.Sydkorea_Tyskland == correct.Sydkorea_Tyskland)
                    points++;

                if (item.Tunisien_England == correct.Tunisien_England)
                    points++;

                if (item.Tyskland_Mexico == correct.Tyskland_Mexico)
                    points++;

                if (item.Tyskland_Sverige == correct.Tyskland_Sverige)
                    points++;

                if (item.Uruguay_Ryssland == correct.Uruguay_Ryssland)
                    points++;

                if (item.Uruguay_Saudiarabien == correct.Uruguay_Saudiarabien)
                    points++;





                if (CompareFinal(item.Finallag1, correct.Finallag1))
                    points++;

                if (CompareFinal(item.Finallag2, correct.Finallag2))
                    points++;

                if (CompareFinal(item.Vinnare, correct.Vinnare))
                    points++;

                #endregion

                item.Poäng = points;
            }

            return data;
        }

        private static bool CompareFinal(string finalTeam, string correctAnswer)
        {
            var correctTeams = correctAnswer.Split('-').ToList();

            foreach (var item in correctTeams.ToList())
            {
                if (item.ToString() == finalTeam)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<TipsData> ManipulateTextStrings(List<TipsData> data)
        {
            foreach (var item in data)
            {
                item.Argentina_Island = item.Argentina_Island.Insert(1, "-");
                item.Argentina_Kroatien = item.Argentina_Kroatien.Insert(1, "-");
                item.Australien_Peru = item.Australien_Peru.Insert(1, "-");
                item.Belgien_Panama = item.Belgien_Panama.Insert(1, "-");
                item.Belgien_Tunisien = item.Belgien_Tunisien.Insert(1, "-");
                item.Brasilien_CostaRica = item.Brasilien_CostaRica.Insert(1, "-");
                item.Brasilien_Schweiz = item.Brasilien_Schweiz.Insert(1, "-");
                item.Colombia_Japan = item.Colombia_Japan.Insert(1, "-");
                item.CostaRica_Serbien = item.CostaRica_Serbien.Insert(1, "-");
                item.Danmark_Australien = item.Danmark_Australien.Insert(1, "-");
                item.Danmark_Frankrike = item.Danmark_Frankrike.Insert(1, "-");
                item.Egypten_Uruguay = item.Egypten_Uruguay.Insert(1, "-");
                item.England_Belgien = item.England_Belgien.Insert(1, "-");
                item.England_Panama = item.England_Panama.Insert(1, "-");
                item.Frankrike_Australien = item.Frankrike_Australien.Insert(1, "-");
                item.Frankrike_Peru = item.Frankrike_Peru.Insert(1, "-");
                item.Iran_Portugal = item.Iran_Portugal.Insert(1, "-");
                item.Iran_Spanien = item.Iran_Spanien.Insert(1, "-");
                item.Island_Kroatien = item.Island_Kroatien.Insert(1, "-");
                item.Japan_Polen = item.Japan_Polen.Insert(1, "-");
                item.Japan_Senegal = item.Japan_Senegal.Insert(1, "-");
                item.Kroatien_Nigeria = item.Kroatien_Nigeria.Insert(1, "-");
                item.Marocko_Iran = item.Marocko_Iran.Insert(1, "-");
                item.Mexiko_Sverige = item.Mexiko_Sverige.Insert(1, "-");
                item.Nigeria_Argentina = item.Nigeria_Argentina.Insert(1, "-");
                item.Nigeria_Island = item.Nigeria_Island.Insert(1, "-");
                item.Panama_Tunisien = item.Panama_Tunisien.Insert(1, "-");
                item.Peru_Danmark = item.Peru_Danmark.Insert(1, "-");
                item.Polen_Colombia = item.Polen_Colombia.Insert(1, "-");
                item.Polen_Senegal = item.Polen_Senegal.Insert(1, "-");
                item.Portugal_Marocko = item.Portugal_Marocko.Insert(1, "-");
                item.Portugal_Spanien = item.Portugal_Spanien.Insert(1, "-");
                item.Ryssland_Egypten = item.Ryssland_Egypten.Insert(1, "-");
                item.Ryssland_Saudiarabien = item.Ryssland_Saudiarabien.Insert(1, "-");
                item.Saudiarabien_Egypten = item.Saudiarabien_Egypten.Insert(1, "-");
                item.Schweiz_CostaRica = item.Schweiz_CostaRica.Insert(1, "-");
                item.Senegal_Colombia = item.Senegal_Colombia.Insert(1, "-");
                item.Serbien_Brasilien = item.Serbien_Brasilien.Insert(1, "-");
                item.Serbien_Schweiz = item.Serbien_Schweiz.Insert(1, "-");
                item.Spanien_Marocko = item.Spanien_Marocko.Insert(1, "-");
                item.Sverige_Sydkorea = item.Sverige_Sydkorea.Insert(1, "-");
                item.Sydkorea_Mexiko = item.Sydkorea_Mexiko.Insert(1, "-");
                item.Sydkorea_Tyskland = item.Sydkorea_Tyskland.Insert(1, "-");
                item.Tunisien_England = item.Tunisien_England.Insert(1, "-");
                item.Tyskland_Mexico = item.Tyskland_Mexico.Insert(1, "-");
                item.Tyskland_Sverige = item.Tyskland_Sverige.Insert(1, "-");
                item.Uruguay_Ryssland = item.Uruguay_Ryssland.Insert(1, "-");
                item.Uruguay_Saudiarabien = item.Uruguay_Saudiarabien.Insert(1, "-");
            }

            return data;
        }

        public static bool SaveCorrectAnswers(TipsData model)
        {
            using (var db = new FotbollsTipsModel())
            {
                var result = (from hits in db.TipsDatas
                              where hits.Namn == DataLogic.RATT_SVAR
                              select hits).FirstOrDefault();

                result.Finallag1 = model.Finallag1;
                result.Finallag2 = model.Finallag2;
                result.Vinnare = model.Vinnare;
                result.Argentina_Island = model.Argentina_Island;
                result.Argentina_Kroatien = model.Argentina_Kroatien;
                result.Australien_Peru = model.Australien_Peru;
                result.Belgien_Panama = model.Belgien_Panama;
                result.Belgien_Tunisien = model.Belgien_Tunisien;
                result.Brasilien_CostaRica = model.Brasilien_CostaRica;
                result.Brasilien_Schweiz = model.Brasilien_Schweiz;
                result.Colombia_Japan = model.Colombia_Japan;
                result.CostaRica_Serbien = model.CostaRica_Serbien;
                result.Danmark_Australien = model.Danmark_Australien;
                result.Danmark_Frankrike = model.Danmark_Frankrike;
                result.Egypten_Uruguay = model.Egypten_Uruguay;
                result.England_Belgien = model.England_Belgien;
                result.England_Panama = model.England_Panama;
                result.Frankrike_Australien = model.Frankrike_Australien;
                result.Frankrike_Peru = model.Frankrike_Peru;
                result.Iran_Portugal = model.Iran_Portugal;
                result.Iran_Spanien = model.Iran_Spanien;
                result.Island_Kroatien = model.Island_Kroatien;
                result.Japan_Polen = model.Japan_Polen;
                result.Japan_Senegal = model.Japan_Senegal;
                result.Kroatien_Nigeria = model.Kroatien_Nigeria;
                result.Marocko_Iran = model.Marocko_Iran;
                result.Mexiko_Sverige = model.Mexiko_Sverige;
                result.Nigeria_Argentina = model.Nigeria_Argentina;
                result.Nigeria_Island = model.Nigeria_Island;
                result.Panama_Tunisien = model.Panama_Tunisien;
                result.Peru_Danmark = model.Peru_Danmark;
                result.Polen_Colombia = model.Polen_Colombia;
                result.Polen_Senegal = model.Polen_Senegal;
                result.Portugal_Marocko = model.Portugal_Marocko;
                result.Portugal_Spanien = model.Portugal_Spanien;
                result.Ryssland_Egypten = model.Ryssland_Egypten;
                result.Ryssland_Saudiarabien = model.Ryssland_Saudiarabien;
                result.Saudiarabien_Egypten = model.Saudiarabien_Egypten;
                result.Schweiz_CostaRica = model.Schweiz_CostaRica;
                result.Senegal_Colombia = model.Senegal_Colombia;
                result.Serbien_Brasilien = model.Serbien_Brasilien;
                result.Serbien_Schweiz = model.Serbien_Schweiz;
                result.Spanien_Marocko = model.Spanien_Marocko;
                result.Sverige_Sydkorea = model.Sverige_Sydkorea;
                result.Sydkorea_Mexiko = model.Sydkorea_Mexiko;
                result.Sydkorea_Tyskland = model.Sydkorea_Tyskland;
                result.Tunisien_England = model.Tunisien_England;
                result.Tyskland_Mexico = model.Tyskland_Mexico;
                result.Tyskland_Sverige = model.Tyskland_Sverige;
                result.Uruguay_Ryssland = model.Uruguay_Ryssland;
                result.Uruguay_Saudiarabien = model.Uruguay_Saudiarabien;

                db.SaveChanges();
            }

            return true;
        }

        public static bool SaveUpdatedPaymentStatus(List<int> idsWhoHasPayed)
        {
            try
            {
                using (var db = new FotbollsTipsModel())
                {
                    foreach (var item in idsWhoHasPayed)
                    {
                        var tipsData = (from hits in db.TipsDatas
                                        where hits.Id == item
                                        select hits).FirstOrDefault();

                        tipsData.HasPayed = true;
                    }

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                Log4NetLogic.Log(Log4NetLogic.LogLevel.ERROR, "An error in", "SaveUpdatedPaymentStatus", e);

                return false;
            }
        }

        public static List<TipsComment> GetComments()
        {
            using (var db = new FotbollsTipsModel())
            {
                var result = (from hits in db.TipsComments
                              select hits).ToList();

                return result;
            }
        }

        public static bool SaveComment(TipsComment model)
        {
            using (var db = new FotbollsTipsModel())
            {
                var comment = new TipsComment()
                {
                    Name = model.Name,
                    Comment = model.Comment,
                    EntryDate = DateTime.UtcNow
                };

                db.TipsComments.Add(comment);
                db.SaveChanges();
            }

            return true;
        }
        private static void SaveTipsDataToLogFile(TipsData t)
        {
            //var logText = string.Format("{0}, {1}",
            //    tipsData.Namn,
            //    tipsData.Argentina_Island);

            var logText = string.Format("Namn: {0}, Telefon: {1}, mail: {2}. ", t.Namn, t.PhoneNumber, t.Email);
            logText += string.Format("Ryssland_Saudiarabien: {0},", t.Ryssland_Saudiarabien);
            logText += string.Format("Egypten_Uruguay: {0}, ", t.Egypten_Uruguay);
            logText += string.Format("Marocko_Iran: {0}, ", t.Marocko_Iran);
            logText += string.Format("Portugal_Spanien: {0}, ", t.Portugal_Spanien);
            logText += string.Format("Frankrike_Australien: {0}, ", t.Frankrike_Australien);
            logText += string.Format("Argentina_Island: {0}, ", t.Argentina_Island);
            logText += string.Format("Peru_Danmark: {0}, ", t.Peru_Danmark);
            logText += string.Format("Kroatien_Nigeria: {0}, ", t.Kroatien_Nigeria);
            logText += string.Format("CostaRica_Serbien: {0}, ", t.CostaRica_Serbien);
            logText += string.Format("Tyskland_Mexico: {0}, ", t.Tyskland_Mexico);
            logText += string.Format("Brasilien_Schweiz: {0}, ", t.Brasilien_Schweiz);
            logText += string.Format("Sverige_Sydkorea: {0}, ", t.Sverige_Sydkorea);
            logText += string.Format("Belgien_Panama: {0}, ", t.Belgien_Panama);
            logText += string.Format("Tunisien_England: {0}, ", t.Tunisien_England);
            logText += string.Format("Colombia_Japan: {0}, ", t.Colombia_Japan);
            logText += string.Format("Polen_Senegal: {0}, ", t.Polen_Senegal);
            logText += string.Format("Ryssland_Egypten: {0}, ", t.Ryssland_Egypten);
            logText += string.Format("Portugal_Marocko: {0}, ", t.Portugal_Marocko);
            logText += string.Format("Uruguay_Saudiarabien: {0}, ", t.Uruguay_Saudiarabien);
            logText += string.Format("Iran_Spanien: {0}, ", t.Iran_Spanien);
            logText += string.Format("Danmark_Australien: {0}, ", t.Danmark_Australien);
            logText += string.Format("Frankrike_Peru: {0}, ", t.Frankrike_Peru);
            logText += string.Format("Argentina_Kroatien: {0}, ", t.Argentina_Kroatien);
            logText += string.Format("Brasilien_CostaRica: {0}, ", t.Brasilien_CostaRica);
            logText += string.Format("Nigeria_Island: {0}, ", t.Nigeria_Island);
            logText += string.Format("Serbien_Schweiz: {0}, ", t.Serbien_Schweiz);
            logText += string.Format("Belgien_Tunisien: {0}, ", t.Belgien_Tunisien);
            logText += string.Format("Sydkorea_Mexiko: {0}, ", t.Sydkorea_Mexiko);
            logText += string.Format("Tyskland_Sverige: {0}, ", t.Tyskland_Sverige);
            logText += string.Format("England_Panama: {0}, ", t.England_Panama);
            logText += string.Format("Japan_Senegal: {0}, ", t.Japan_Senegal);
            logText += string.Format("Polen_Colombia: {0}, ", t.Polen_Colombia);
            logText += string.Format("Saudiarabien_Egypten: {0}, ", t.Saudiarabien_Egypten);
            logText += string.Format("Uruguay_Ryssland: {0}, ", t.Uruguay_Ryssland);
            logText += string.Format("Iran_Portugal: {0}, ", t.Iran_Portugal);
            logText += string.Format("Spanien_Marocko: {0}, ", t.Spanien_Marocko);
            logText += string.Format("Danmark_Frankrike: {0}, ", t.Danmark_Frankrike);
            logText += string.Format("Australien_Peru: {0}, ", t.Australien_Peru);
            logText += string.Format("Nigeria_Argentina: {0}, ", t.Nigeria_Argentina);
            logText += string.Format("Island_Kroatien: {0}, ", t.Island_Kroatien);
            logText += string.Format("Sydkorea_Tyskland: {0}, ", t.Sydkorea_Tyskland);
            logText += string.Format("Mexiko_Sverige: {0}, ", t.Mexiko_Sverige);
            logText += string.Format("Serbien_Brasilien: {0}, ", t.Serbien_Brasilien);
            logText += string.Format("Schweiz_CostaRica: {0}, ", t.Schweiz_CostaRica);
            logText += string.Format("Japan_Polen: {0}, ", t.Japan_Polen);
            logText += string.Format("Senegal_Colombia: {0}, ", t.Senegal_Colombia);
            logText += string.Format("England_Belgien: {0}, ", t.England_Belgien);
            logText += string.Format("Panama_Tunisien: {0}, ", t.Panama_Tunisien);

            Log4NetLogic.Log(Log4NetLogic.LogLevel.INFO, logText, "SaveTipsDataToLogFile");
        }
        public static SavedTipsDataResult SaveNewTipsData(TipsData tipsData)
        {
            try
            {
                SaveTipsDataToLogFile(tipsData);

            }
            catch (Exception)
            {
                // Do nothing, since log to textfile does not work
            }

            try
            {
                using (var db = new FotbollsTipsModel())
                {
                    db.TipsDatas.Add(tipsData);
                    db.SaveChanges();


                    return new SavedTipsDataResult()
                    {
                        IdOfTipsdata = tipsData.Id,
                        SuccessedSave = true
                    };
                }
            }
            catch (Exception e)
            {
                Log4NetLogic.Log(Log4NetLogic.LogLevel.ERROR, "An error in", "SaveNewTipsData", e);

                return new SavedTipsDataResult()
                {
                    IdOfTipsdata = 0,
                    SuccessedSave = false
                };
            }
        }

        public static bool SaveNewTipsDataImagePath(TipsPathToPDF path)
        {
            using (var db = new FotbollsTipsModel())
            {
                db.TipsPathToPDFs.Add(path);
                db.SaveChanges();
            }

            return true;
        }
    }
}