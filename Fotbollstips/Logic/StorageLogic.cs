using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.Azure;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Fotbollstips.Logic
{
    public class StorageLogic
    {
        public StorageLogic()
        {

        }

        public string SavePDF(PdfDocument document, string name)
        {
            //string accountKey = "NADhxvTU40qlYify/eTZR+li4xkIaIUsbx8Kgz+SKUEoXmiVomKVIVvCTQjCSd+xyVNLy5x44e8nu44kl6FO7w==";
            try
            {
                CloudStorageAccount account = GetStorageAccount();

                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer sampleContainer = client.GetContainerReference("tips");
                sampleContainer.CreateIfNotExists();

                name = NameWithoutSpace(name);
                string fileName = string.Format("{0}_{1}.pdf", name, Guid.NewGuid());
                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(fileName);

                using (MemoryStream myStream = new MemoryStream())
                {
                    document.Save(myStream, false);

                    blob.UploadFromStream(myStream);
                }

                return blob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
            }
            catch (Exception e)
            {
                Log4NetLogic.Log(Log4NetLogic.LogLevel.ERROR, "An error in:", "SavePDF", e);

                return "Något gick fel när PDF skulle sparas.";
            }
        }

        private CloudStorageAccount GetStorageAccount()
        {
            string accountName = "storagemartin";
            string accountKey = ConfigurationManager.AppSettings["BlobPassword"];

            StorageCredentials creds = new StorageCredentials(accountName, accountKey);

            return new CloudStorageAccount(creds, useHttps: true);
        }

        private string NameWithoutSpace(string name)
        {
            return name.Replace(' ', '_');
        }

        public string GetFileFromFileStorage()
        {
            try
            {
                string returnString = "No data retreived.";
                string accountName = "storagemartin";
                string accountKey = ConfigurationManager.AppSettings["BlobPassword"];

                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);

                // Create a CloudFileClient object for credentialed access to Azure Files.
                CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

                // Get a reference to the file share we created previously.
                CloudFileShare share = fileClient.GetShareReference("tipslogs");

                // Ensure that the share exists.
                if (share.Exists())
                {
                    // Get a reference to the root directory for the share.
                    CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                    // Get a reference to the directory we created previously.
                    CloudFileDirectory sampleDir = rootDir.GetDirectoryReference("thetipslogs");

                    // Ensure that the directory exists.
                    if (sampleDir.Exists())
                    {
                        // Get a reference to the file we created previously.
                        CloudFile file = sampleDir.GetFileReference("tipslogError.txt");

                        // Ensure that the file exists.
                        if (file.Exists())
                        {
                            // Write the contents of the file to the console window.
                            returnString = file.DownloadTextAsync().Result;

                            returnString = MakeHtmlFriendlyText(returnString);
                            return returnString;
                        }
                    }
                }

                return returnString;
            }
            catch (Exception e)
            {
                string inner = e.InnerException != null ? e.InnerException.ToString() : "NULL";
                return string.Format("Error in method GetFileFromFileStorage. Exception: {0}. Inner: {1}", e.Message.ToString(), inner);
            }
        }

        public void SendSms(string message)
        {
            CloudStorageAccount account = GetStorageAccount();

            CloudQueueClient queueClient = account.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("fotbollstips");

            //queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            queue.AddMessage(queueMessage);
        }

        private string MakeHtmlFriendlyText(string returnString)
        {
            returnString = returnString.Replace("\r\n", "<br /><br />");

            return returnString;
        }

    }
}
