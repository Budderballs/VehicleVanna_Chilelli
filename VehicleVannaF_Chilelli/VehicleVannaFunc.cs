using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
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
            await vehicleAsync.AddAsync(plzWork);
            log.LogInformation("Log stuff here");
            string responseMessage = string.IsNullOrEmpty(plzWork.buyerFirstName) || string.IsNullOrEmpty(plzWork.buyerLastName) || string.IsNullOrEmpty(plzWork.buyerEmail)
                || string.IsNullOrEmpty(plzWork.Make) || string.IsNullOrEmpty(plzWork.Model) || string.IsNullOrEmpty(plzWork.Year)
                ? "Please make sure to fill out all of the fields and try again"
                : $"Buyer {plzWork.buyerFirstName} {plzWork.buyerLastName} {"(" + plzWork.buyerEmail +")"} purchased a {plzWork.Year} {plzWork.Make + " "}" +
                $"{plzWork.Model} {plzWork.vehicleType} with list price of {plzWork.listPrice.ToString("C")}. With discount applied, " +
                $"purchase price is {(plzWork.listPrice - (plzWork.listPrice * .085m)).ToString("C")}";

            return new OkObjectResult(responseMessage);
        }
    }
}

