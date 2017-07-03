using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Fotbollstips.Logic
{
    public class BlobStorageLogic
    {
        public BlobStorageLogic()
        {

        }
        public void SavePDF(PdfDocument document)
        {
            string accountName = "storagemartin";
            string accountKey = "NADhxvTU40qlYify/eTZR+li4xkIaIUsbx8Kgz+SKUEoXmiVomKVIVvCTQjCSd+xyVNLy5x44e8nu44kl6FO7w==";
            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer sampleContainer = client.GetContainerReference("testimages");
                sampleContainer.CreateIfNotExists();


                string randomNumber = RandomNumber();
                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(randomNumber + ".pdf");

                //var document = CreateDocument();
                using (MemoryStream myStream = new MemoryStream())
                {
                    document.Save(myStream, false);

                    blob.UploadFromStream(myStream);
                }


                //var stream = ConvertMemDocToMemoryStream2(document);

                //blob.UploadFromStream(stream);

                //using (Stream file = System.IO.File.OpenRead(@"C:\bimdemo\ball.jpg"))
                //{
                //    blob.UploadFromStream(file);
                //}



            }
            catch (Exception ex)
            {
                var stophere = 3;
            }



        }
        private static string RandomNumber()
        {
            Random rand = new Random();
            return rand.Next(0, 10000).ToString();
        }

        //private Document CreateDocument()
        //{
        //    Document document = new Document();
        //    var section = document.AddSection();
        //    var paragraph = section.AddParagraph("Report Demo");

        //    // Add other document elements here

        //    return document;
        //}
        //protected static MemoryStream ConvertMemDocToMemoryStream(Document document)
        //{
        //    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
        //    pdfRenderer.Document = document;
        //    pdfRenderer.RenderDocument();
        //    MemoryStream stream = new MemoryStream();
        //    pdfRenderer.PdfDocument.Save(stream, false);
        //    return stream;
        //}
        //protected static MemoryStream ConvertMemDocToMemoryStream2(PdfDocument document)
        //{
        //    try
        //    {
        //        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
        //        pdfRenderer.PdfDocument = document;

        //        pdfRenderer.RenderDocument();
        //        MemoryStream stream = new MemoryStream();
        //        pdfRenderer.PdfDocument.Save(stream, false);
        //        return stream;
        //    }
        //    catch (Exception e)
        //    {
        //        var hejsan = e;
        //    }
        //    return new MemoryStream();
        //}
    }
}
