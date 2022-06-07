using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SendGrid;
using SendGrid.Helpers.Mail;

namespace FunctionApp1
{
    public static class BlobTriggerFunction
    {
        [FunctionName("BlobTriggerFunction")]
        public static void Run([BlobTrigger("license/{name}", Connection = "")]string myBlob,[SendGrid(ApiKey = "SendGridAPIKey")] out SendGridMessage messageSend, string name, ILogger log)
        {

            var licencecontent = System.Text.Encoding.UTF8.GetBytes(myBlob);
            string base64 = Convert.ToBase64String(licencecontent);
            messageSend = new SendGridMessage();
            messageSend.SetFrom(new EmailAddress("sgutlapalli@outlook.com"));
            messageSend.AddTo("sivasankarrocks@gmail.com");
            messageSend.AddAttachment(name, base64,type:"text/plain");
            messageSend.SetSubject("license file");
            messageSend.HtmlContent = "your order details ";
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
