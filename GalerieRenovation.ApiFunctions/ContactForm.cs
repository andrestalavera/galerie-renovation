using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace GalerieRenovation.ApiFunctions;
public static class ContactForm
{
    [FunctionName(nameof(ContactForm))]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("HTTP trigger processed a request for ContactForm Function.");

        MailMessage mailMessage = await GetMessageAsync(req);
        SmtpClient smtpClient = GetSmtpClient();

        try
        {
            log.LogInformation("Attempt to send email message");
            smtpClient.Send(mailMessage);
            log.LogInformation("Message sent");
            return new OkResult();
        }
        catch (Exception ex)
        {
            log.LogError(ex.ToString());
            return new BadRequestResult();
        }

    }

    private static async Task<MailMessage> GetMessageAsync(HttpRequest request)
    {
        string body = await new StreamReader(request.Body).ReadToEndAsync();
        Message message = JsonConvert.DeserializeObject<Message>(body);
        MailMessage mail = new()
        {
            Body = $"Message de `{message.From}`: {body}",
            IsBodyHtml = false,
            Priority = MailPriority.High
        };
        mail.To.Add(new("contact@galerierenovation.com"));
        mail.ReplyToList.Add(message.From);
        return mail;
    }

    private static SmtpClient GetSmtpClient()
    {
        string username = Environment.GetEnvironmentVariable("OFFICE365_USERNAME");
        string password = Environment.GetEnvironmentVariable("OFFICE365_PASSWORD");
        string host = Environment.GetEnvironmentVariable("OFFICE365_HOST");

        NetworkCredential credential = new(username, password);

        return new()
        {
            Credentials = credential,
            Host = host,
            EnableSsl = true,
            Port = 587
        };
    }
}

internal class Message
{
    public string From { get; set; }
    public string Body { get; set; }
}