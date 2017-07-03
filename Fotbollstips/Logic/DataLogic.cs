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

        public static List<TipsDataDisplay> GetDataForPresentation()
        {
            try
            {
                // Get raw data
                List<TipsData> rawData = GetEmTipsRawData();

                // Calculate points
                List<TipsData> calculatedPoints = CalculatePoints(rawData);

                // Sort list by points
                List<TipsData> sortedList = calculatedPoints.OrderByDescending(o => o.Poäng).ToList();

                // Manipulate text string 
                List<TipsData> manipulatedTextList = ManipulateTextStrings(sortedList);

                List<TipsDataDisplay> returnList = new List<TipsDataDisplay>();

                foreach (var item in manipulatedTextList)
                {
                    returnList.Add(new TipsDataDisplay(item));
                }

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

        private static List<TipsData> GetEmTipsRawData()
        {
            using (var db = new MartinDatabaseEntities())
            {
                var result = (from hits in db.TipsDatas
                              select hits).ToList();

                return result;
            }
        }

        private static TipsData GetRattSvarEntity()
        {
            var result = GetEmTipsRawData();

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
    }
}