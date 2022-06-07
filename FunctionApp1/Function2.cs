using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static void Run([QueueTrigger("orders")] Order orders,  [Blob("license/{rand-guid}.lic")] TextWriter writer, ILogger log)
        {
            writer.WriteLine($"OrderId : {orders.OrderId}");
            writer.WriteLine($"OrderName : {orders.OrderNmae}");
            writer.WriteLine($"Email : sivasankarrocks@gmail.com");


            log.LogInformation($"C# Queue trigger function processed: {orders}");
        }
    }
}
