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
                using (var db = new MartinDatabaseEntities())
                {
                    TipsError error = new TipsError()
                    {
                        Exception = e.ToString(),
                        InnerException = e.InnerException != null ? e.InnerException.ToString() : "NULL",
                        EntryDate = DateTime.UtcNow
                    };

                    db.TipsErrors.Add(error);
                    db.SaveChanges();
                }
                return null;
            }
        }

        private static List<TipsData> GetTipsRawData()
        {
            using (var db = new MartinDatabaseEntities())
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
                    //TODO Ändra till 51
                    item.Poäng = 7;
                    continue;
                }
                int points = 0;

                #region compare games

                if (item.Sverige_Kamerun == correct.Sverige_Kamerun)
                    points++;

                if (item.Ryssland_Brasilien == correct.Ryssland_Brasilien)
                    points++;

                if (item.Kamerun_Brasilien == correct.Kamerun_Brasilien)
                    points++;

                if (item.Sverige_Ryssland == correct.Sverige_Ryssland)
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
                item.Sverige_Kamerun = item.Sverige_Kamerun.Insert(1, "-");
                item.Ryssland_Brasilien = item.Ryssland_Brasilien.Insert(1, "-");
                item.Kamerun_Brasilien = item.Kamerun_Brasilien.Insert(1, "-");
                item.Sverige_Ryssland = item.Sverige_Ryssland.Insert(1, "-");
            }

            return data;
        }

        public static bool SaveCorrectAnswers(TipsData model)
        {
            using (var db = new MartinDatabaseEntities())
            {
                var result = (from hits in db.TipsDatas
                              where hits.Namn == DataLogic.RATT_SVAR
                              select hits).FirstOrDefault();

                result.Finallag1 = model.Finallag1;
                result.Finallag2 = model.Finallag2;
                result.Vinnare = model.Vinnare;
                result.Sverige_Kamerun = model.Sverige_Kamerun;
                result.Ryssland_Brasilien = model.Ryssland_Brasilien;
                result.Kamerun_Brasilien = model.Kamerun_Brasilien;
                result.Sverige_Ryssland = model.Sverige_Ryssland;

                db.SaveChanges();
            }

            return true;
        }

        public static bool SaveUpdatedPaymentStatus(List<int> idsWhoHasPayed)
        {
            using (var db = new MartinDatabaseEntities())
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
        public static List<TipsError> GetErrors()
        {
            using (var db = new MartinDatabaseEntities())
            {
                var result = (from hits in db.TipsErrors
                              select hits).ToList();

                return result;
            }
        }

        public static List<TipsComment> GetComments()
        {
            using (var db = new MartinDatabaseEntities())
            {
                var result = (from hits in db.TipsComments
                              select hits).ToList();

                return result;
            }
        }

        public static bool SaveComment(TipsComment model)
        {
            using (var db = new MartinDatabaseEntities())
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

        public static SavedTipsDataResult SaveNewTipsData(TipsData tipsData)
        {
            try
            {

                using (var db = new MartinDatabaseEntities())
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
            catch(Exception ex)
            {
                return new SavedTipsDataResult()
                {
                    IdOfTipsdata = 0,
                    SuccessedSave = false
                };
            }
        }

        public static bool SaveNewTipsDataImagePath(TipsPathToPDF path)
        {
            using (var db = new MartinDatabaseEntities())
            {
                db.TipsPathToPDFs.Add(path);
                db.SaveChanges();
            }

            return true;
        }
    }
}