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

        public static void DoStuff()
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            // Draw the text
            gfx.DrawString("Med SendGrid", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);


            var blobWorker = new BlobStorageLogic();
            blobWorker.SavePDF(document);

            //SendGridLogic.SendEmail();


            // Save the document...
            //string filename = "HelloWorld.pdf";
            //document.Save(filename);


            //// ...and start a viewer.
            //Process.Start(filename);

        }
    }
}