using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace GalerieRenovation.ApiFunctions;

public static class SubscribeToNewsletterFunction
{
    [FunctionName(nameof(SubscribeToNewsletterFunction))]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest request)
    {
        throw new NotImplementedException();
    }
}