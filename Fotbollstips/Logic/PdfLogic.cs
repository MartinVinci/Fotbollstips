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

        private static int SpaceAfterText()
        {
            return 20;
        }

        private static string ManipulateResult(string resultText)
        {
            return resultText.Insert(1, "-");
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
            int xCoordGame = 30;
            int xCoordResult = 200;
            int yCoord = 35;

            // Write initial information
            graph.DrawString("Namn", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.Namn, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Telefonnummer", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.PhoneNumber, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Mailadress", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(tipsData.Email, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();
            yCoord += SpaceAfterText();

            graph.DrawString("Sverige - Kamerun", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(ManipulateResult(tipsData.Sverige_Kamerun), fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Ryssland - Brasilien", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(ManipulateResult(tipsData.Ryssland_Brasilien), fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Kamerun - Brasilien", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(ManipulateResult(tipsData.Kamerun_Brasilien), fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

            graph.DrawString("Sverige - Ryssland", fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordGame, yCoord, XStringFormats.TopLeft);
            graph.DrawString(ManipulateResult(tipsData.Sverige_Ryssland), fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), xCoordResult, yCoord, XStringFormats.TopLeft);
            yCoord += SpaceAfterText();

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