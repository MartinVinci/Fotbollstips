using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotbollstips.Logic
{
    public class PdfLogic
    {
        public PdfLogic()
        {

        }

        public PdfDocument SaveTipsDatas(TipsData tipsData)
        {
            PdfDocument pdf = new PdfDocument();

            PdfPage pdfPage = pdf.AddPage();

            XFont fontBig = new XFont("Times New Roman", 20, XFontStyle.Regular);
            XFont fontMedium = new XFont("Times New Roman", 16, XFontStyle.Regular);
            XFont fontSmall = new XFont("Times New Roman", 12, XFontStyle.Regular);

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);

            // Set beginning coordinates
            int xCoordGame = 60;
            int xCoordResult = 230;
            //int yCoordGeneral = 35;
            int yCoord = 35;

            // Write initial information
            graph.DrawString("Namn:", fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.Namn, fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Telefonnummer:", fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.PhoneNumber, fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Mailadress:", fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.Email, fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();
            yCoord += SpaceAfterText();

            // Write games
            yCoord = DrawTheString("Ryssland - Saudiarabien", tipsData.Ryssland_Saudiarabien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Egypten - Uruguay", tipsData.Egypten_Uruguay, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Marocko - Iran", tipsData.Marocko_Iran, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Portugal - Spanien", tipsData.Portugal_Spanien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Frankrike - Australien", tipsData.Frankrike_Australien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Argentina - Island", tipsData.Argentina_Island, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Peru - Danmark", tipsData.Peru_Danmark, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Kroatien - Nigeria", tipsData.Kroatien_Nigeria, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Costa Rica - Serbien", tipsData.CostaRica_Serbien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Tyskland - Mexiko", tipsData.Tyskland_Mexico, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Brasilien - Schweiz", tipsData.Brasilien_Schweiz, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Sverige - Sydkorea", tipsData.Sverige_Sydkorea, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Belgien - Panama", tipsData.Belgien_Panama, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Tunisien - England", tipsData.Tunisien_England, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Colombia - Japan", tipsData.Colombia_Japan, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Polen - Senegal", tipsData.Polen_Senegal, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Ryssland - Egypten", tipsData.Ryssland_Egypten, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Portugal - Marocko", tipsData.Portugal_Marocko, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Uruguay - Saudiarabien", tipsData.Uruguay_Saudiarabien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Iran - Spanien", tipsData.Iran_Spanien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Danmark - Australien", tipsData.Danmark_Australien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Frankrike - Peru", tipsData.Frankrike_Peru, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Argentina - Kroatien", tipsData.Argentina_Kroatien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Brasilien - Costa Rica", tipsData.Brasilien_CostaRica, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Nigeria - Island", tipsData.Nigeria_Island, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Serbien - Schweiz", tipsData.Serbien_Schweiz, yCoord, xCoordGame, xCoordResult, graph, fontMedium);

            // Right column
            yCoord = 135;
            xCoordGame = 340;
            xCoordResult = 510;

            yCoord = DrawTheString("Belgien - Tunisien", tipsData.Belgien_Tunisien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Sydkorea - Mexiko", tipsData.Sydkorea_Mexiko, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Tyskland - Sverige", tipsData.Tyskland_Sverige, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("England - Panama", tipsData.England_Panama, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Japan - Senegal", tipsData.Japan_Senegal, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Polen - Colombia", tipsData.Polen_Colombia, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Saudiarabien - Egypten", tipsData.Saudiarabien_Egypten, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Uruguay - Ryssland", tipsData.Uruguay_Ryssland, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Iran - Portugal", tipsData.Iran_Portugal, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Spanien - Marocko", tipsData.Spanien_Marocko, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Danmark - Frankrike", tipsData.Danmark_Frankrike, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Australien - Peru", tipsData.Australien_Peru, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Nigeria - Argentina", tipsData.Nigeria_Argentina, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Island - Kroatien", tipsData.Island_Kroatien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Sydkorea - Tyskland", tipsData.Sydkorea_Tyskland, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Mexiko - Sverige", tipsData.Mexiko_Sverige, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Serbien - Brasilien", tipsData.Serbien_Brasilien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Schweiz - Costa Rica", tipsData.Schweiz_CostaRica, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Japan - Polen", tipsData.Japan_Polen, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Senegal - Colombia", tipsData.Senegal_Colombia, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("England - Belgien", tipsData.England_Belgien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);
            yCoord = DrawTheString("Panama - Tunisien", tipsData.Panama_Tunisien, yCoord, xCoordGame, xCoordResult, graph, fontMedium);

            xCoordGame = 340;
            xCoordResult = 440;

            graph.DrawString("Finallag 1", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.Finallag1, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Finallag 2", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.Finallag2, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Vinnare", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.Vinnare, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            return pdf;
        }

        private int DrawTheString(string game, string result, int yCoord, int xCoordGame, int xCoordResult, XGraphics graph, XFont font)
        {
            graph.DrawString(game, font, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(ManipulateResult(result), font, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            return yCoord += SpaceAfterText();

        }
        private static int SpaceAfterText()
        {
            return 25;
        }
        private static string ManipulateResult(string resultText)
        {
            return resultText.Insert(1, "-");
        }

        //public static void DoStuff()
        //{
        //    // Create a new PDF document
        //    PdfDocument document = new PdfDocument();

        //    // Create an empty page
        //    PdfPage page = document.AddPage();

        //    // Get an XGraphics object for drawing
        //    XGraphics gfx = XGraphics.FromPdfPage(page);

        //    // Create a font
        //    XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

        //    // Draw the text
        //    gfx.DrawString("Med SendGrid", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);


        //    var blobWorker = new BlobStorageLogic();
        //    blobWorker.SavePDF(document, );

        //    //SendGridLogic.SendEmail();


        //    // Save the document...
        //    //string filename = "HelloWorld.pdf";
        //    //document.Save(filename);


        //    //// ...and start a viewer.
        //    //Process.Start(filename);

        //}
    }
}