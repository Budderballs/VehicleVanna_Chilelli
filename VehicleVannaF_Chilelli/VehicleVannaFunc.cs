using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using VehicleVannaUI_Chilelli;

namespace VehicleVannaF_Chilelli
{
    public static class VehicleVannaFunc
    {
        [FunctionName("VehicleVannaF")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("VehicleQueue")] IAsyncCollector<VehicleEnum> vehicleAsync,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var plzWork = JsonConvert.DeserializeObject<VehicleEnum>(requestBody);
            log.LogInformation("Log stuff here");
            string responseMessage = $"Buyer {plzWork.buyerFirstName} {plzWork.buyerLastName} purchased a {plzWork.Year} {plzWork.Make}" +
                $"{plzWork.Model}  {plzWork.vehicleType}  with list price of {plzWork.listPrice.ToString("C")}. With discount applied, " +
                $"purchase price is {(plzWork.listPrice - (plzWork.listPrice * .085m)).ToString("C")}";

            return new OkObjectResult(responseMessage);
        }
    }
}

