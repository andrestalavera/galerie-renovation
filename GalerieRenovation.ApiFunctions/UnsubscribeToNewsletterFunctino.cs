using GalerieRenovation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace GalerieRenovation.ApiFunctions;

public static class UnsubscribeToNewsletterFunctino
{
    [FunctionName(nameof(UnsubscribeToNewsletterFunctino))]
    public static async Task<Product> Run(
        [HttpTrigger(AuthorizationLevel.Function)] HttpRequest request)
    {
        throw new NotImplementedException();
    }
}