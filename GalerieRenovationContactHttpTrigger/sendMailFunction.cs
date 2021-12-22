using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GalerieRenovation.Models;
using System.Net.Mail;

namespace GalerieRenovationContact
{
    public static class SendMailFunction
    {
        [FunctionName("SendMailFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            //log.LogInformation("C# HTTP trigger function processed a request.");

            // string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ContactContext cc = JsonConvert.DeserializeObject<ContactContext>(requestBody);
            MailMessage mailMessage = new();
            mailMessage.Subject = "Site internet";
            mailMessage.Body = cc.Names + cc.Message;
            mailMessage.Priority = MailPriority.High;
            //c'est pour les Keyvolt
           // var recipient = config["mail"];
            //var password = config["password"];

            //name = name ?? data?.name;

            /*  string responseMessage = string.IsNullOrEmpty(name)
                  ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                  : $"Hello, {name}. This HTTP triggered function executed successfully.";*/
            SmtpClient smtpClient = new();
            try
            {
                smtpClient.SendMailAsync(mailMessage);
                return new OkObjectResult(mailMessage);
            }

            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
