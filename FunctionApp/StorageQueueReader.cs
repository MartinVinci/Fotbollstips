using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace FunctionApp
{
    public static class StorageQueueReader
    {
        [FunctionName("StorageQueueReader")]
        public static void Run([QueueTrigger("fotbollstips", Connection = "StorageConnectionString")]string myQueueItem, TraceWriter log)
        {
            bool success = SendSmsWithTwilio(myQueueItem);
        }

        private static bool SendSmsWithTwilio(string myQueueItem)
        {
            try
            {
                // Find your Account Sid and Auth Token at twilio.com/console
                const string accountSid = "ACe2743f8aa25b5dde4193e977d5f2c1d1";
                const string authToken = "Masked";
                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber("+46709632067");
                var message = MessageResource.Create(
                    to,
                    from: new PhoneNumber("+46765193966"),
                    body: myQueueItem);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
