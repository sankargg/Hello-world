using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("GetMessage")]
        
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = "Order/{name1}")] HttpRequest req, 
            [Queue("Orders")] IAsyncCollector<Order> orderQueue, 
              string name1,
            ILogger log)
        {
            log.LogInformation($" Order details of {name1}.");

            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Order>(requestBody);

            await orderQueue.AddAsync(data);

            string responseMessage = $" {name1} you order details : \n Order Id ; { data.OrderId} , \n Order Name : {data.OrderNmae}";

            return new OkObjectResult(responseMessage);
        }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNmae { get; set; }
    }
}
