using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        
        public string SavePDF(PdfDocument document, string name)
        {
            string accountName = "storagemartin";
            string accountKey = ConfigurationManager.AppSettings["BlobPassword"];
            //string accountKey = "NADhxvTU40qlYify/eTZR+li4xkIaIUsbx8Kgz+SKUEoXmiVomKVIVvCTQjCSd+xyVNLy5x44e8nu44kl6FO7w==";
            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer sampleContainer = client.GetContainerReference("rows3");
                sampleContainer.CreateIfNotExists();

                string fileName = string.Format("{0}_{1}.pdf", name, Guid.NewGuid());
                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(fileName);

                //var document = CreateDocument();
                using (MemoryStream myStream = new MemoryStream())
                {
                    document.Save(myStream, false);

                    blob.UploadFromStream(myStream);
                }

                return blob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
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
                return "Något gick fel";
            }
        }

        private string NameWithoutSpace(string name)
        {
            return name.Replace(' ', '_');
        }
    }
}
