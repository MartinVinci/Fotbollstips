using Fotbollstips;
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
        public string Ryssland_Saudiarabien { get; set; }
        public string Egypten_Uruguay { get; set; }
        public string Marocko_Iran { get; set; }
        public string Portugal_Spanien { get; set; }
        public string Frankrike_Australien { get; set; }
        public string Argentina_Island { get; set; }
        public string Peru_Danmark { get; set; }
        public string Kroatien_Nigeria { get; set; }
        public string CostaRica_Serbien { get; set; }
        public string Tyskland_Mexiko { get; set; }
        public string Brasilien_Schweiz { get; set; }
        public string Sverige_Sydkorea { get; set; }
        public string Belgien_Panama { get; set; }
        public string Tunisien_England { get; set; }
        public string Colombia_Japan { get; set; }
        public string Polen_Senegal { get; set; }
        public string Ryssland_Egypten { get; set; }
        public string Portugal_Marocko { get; set; }
        public string Uruguay_Saudiarabien { get; set; }
        public string Iran_Spanien { get; set; }
        public string Danmark_Australien { get; set; }
        public string Frankrike_Peru { get; set; }
        public string Argentina_Kroatien { get; set; }
        public string Brasilien_CostaRica { get; set; }
        public string Nigeria_Island { get; set; }
        public string Serbien_Schweiz { get; set; }
        public string Belgien_Tunisien { get; set; }
        public string Sydkorea_Mexiko { get; set; }
        public string Tyskland_Sverige { get; set; }
        public string England_Panama { get; set; }
        public string Japan_Senegal { get; set; }
        public string Polen_Colombia { get; set; }
        public string Saudiarabien_Egypten { get; set; }
        public string Uruguay_Ryssland { get; set; }
        public string Iran_Portugal { get; set; }
        public string Spanien_Marocko { get; set; }
        public string Danmark_Frankrike { get; set; }
        public string Australien_Peru { get; set; }
        public string Nigeria_Argentina { get; set; }
        public string Island_Kroatien { get; set; }
        public string Sydkorea_Tyskland { get; set; }
        public string Mexiko_Sverige { get; set; }
        public string Serbien_Brasilien { get; set; }
        public string Schweiz_CostaRica { get; set; }
        public string Japan_Polen { get; set; }
        public string Senegal_Colombia { get; set; }
        public string England_Belgien { get; set; }
        public string Panama_Tunisien { get; set; }

        public TipsDataDisplay(TipsData td)
        {
            Namn = td.Namn;
            Poäng = td.Poäng;
            Finallag1 = td.Finallag1;
            Finallag2 = td.Finallag2;
            Vinnare = td.Vinnare;
            Ryssland_Saudiarabien = td.Ryssland_Saudiarabien;
            Egypten_Uruguay = td.Egypten_Uruguay;
            Marocko_Iran = td.Marocko_Iran;
            Portugal_Spanien = td.Portugal_Spanien;
            Frankrike_Australien = td.Frankrike_Australien;
            Argentina_Island = td.Argentina_Island;
            Peru_Danmark = td.Peru_Danmark;
            Kroatien_Nigeria = td.Kroatien_Nigeria;
            CostaRica_Serbien = td.CostaRica_Serbien;
            Tyskland_Mexiko = td.Tyskland_Mexico;
            Brasilien_Schweiz = td.Brasilien_Schweiz;
            Sverige_Sydkorea = td.Sverige_Sydkorea;
            Belgien_Panama = td.Belgien_Panama;
            Tunisien_England = td.Tunisien_England;
            Colombia_Japan = td.Colombia_Japan;
            Polen_Senegal = td.Polen_Senegal;
            Ryssland_Egypten = td.Ryssland_Egypten;
            Portugal_Marocko = td.Portugal_Marocko;
            Uruguay_Saudiarabien = td.Uruguay_Saudiarabien;
            Iran_Spanien = td.Iran_Spanien;
            Danmark_Australien = td.Danmark_Australien;
            Frankrike_Peru = td.Frankrike_Peru;
            Argentina_Kroatien = td.Argentina_Kroatien;
            Brasilien_CostaRica = td.Brasilien_CostaRica;
            Nigeria_Island = td.Nigeria_Island;
            Serbien_Schweiz = td.Serbien_Schweiz;
            Belgien_Tunisien = td.Belgien_Tunisien;
            Sydkorea_Mexiko = td.Sydkorea_Mexiko;
            Tyskland_Sverige = td.Tyskland_Sverige;
            England_Panama = td.England_Panama;
            Japan_Senegal = td.Japan_Senegal;
            Polen_Colombia = td.Polen_Colombia;
            Saudiarabien_Egypten = td.Saudiarabien_Egypten;
            Uruguay_Ryssland = td.Uruguay_Ryssland;
            Iran_Portugal = td.Iran_Portugal;
            Spanien_Marocko = td.Spanien_Marocko;
            Danmark_Frankrike = td.Danmark_Frankrike;
            Australien_Peru = td.Australien_Peru;
            Nigeria_Argentina = td.Nigeria_Argentina;
            Island_Kroatien = td.Island_Kroatien;
            Sydkorea_Tyskland = td.Sydkorea_Tyskland;
            Mexiko_Sverige = td.Mexiko_Sverige;
            Serbien_Brasilien = td.Serbien_Brasilien;
            Schweiz_CostaRica = td.Schweiz_CostaRica;
            Japan_Polen = td.Japan_Polen;
            Senegal_Colombia = td.Senegal_Colombia;
            England_Belgien = td.England_Belgien;
            Panama_Tunisien = td.Panama_Tunisien;
        }
    }
}