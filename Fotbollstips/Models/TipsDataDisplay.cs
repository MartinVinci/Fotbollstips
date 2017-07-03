using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotbollstips.Models
{
    public class TipsDataDisplay
    {
        public string Namn { get; set; }
        public int? Poäng { get; set; }
        public string Finallag1 { get; set; }
        public string Finallag2 { get; set; }
        public string Vinnare { get; set; }
        public string Sverige_Kamerun { get; set; }
        public string Ryssland_Brasilien { get; set; }
        public string Kamerun_Brasilien { get; set; }
        public string Sverige_Ryssland { get; set; }

        public TipsDataDisplay(TipsData td)
        {
            Namn = td.Namn;
            Poäng = td.Poäng;
            Finallag1 = td.Finallag1;
            Finallag2 = td.Finallag2;
            Vinnare = td.Vinnare;
            Sverige_Kamerun = td.Sverige_Kamerun;
            Ryssland_Brasilien = td.Ryssland_Brasilien;
            Kamerun_Brasilien = td.Kamerun_Brasilien;
            Sverige_Ryssland = td.Sverige_Kamerun;
        }
    }
}