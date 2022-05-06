using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GalerieRenovation.Models;
using GalerieRenovation.ApiFunctions.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GalerieRenovation.ApiFunctions;
public static class ContactFunction
{
    [FunctionName(nameof(ContactFunction))]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
        ILogger log)
    {
        log.LogInformation("HTTP trigger processed a request for ContactForm Function.");

        MailMessage mailMessage = await GetMessageAsync(request);
        SmtpClient smtpClient = GetSmtpClient();

        try
        {
            log.LogInformation("Attempt to send mail message");
            smtpClient.Send(mailMessage);
            log.LogInformation("Mail message sent");
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
        ArgumentNullException.ThrowIfNull(request.Body);
        string body = await new StreamReader(request.Body).ReadToEndAsync();
        ContactModel contactModel = JsonConvert.DeserializeObject<ContactModel>(body);
        MailMessage mailMessage = new()
        {
            Body = EmailHelpers.BuildEmail(BuildEmailContent(contactModel)),
            IsBodyHtml = true,
            Priority = MailPriority.High,
            Subject = "Contact www.galerierenovation.com"
        };
        string? grAddress = Environment.GetEnvironmentVariable("OFFICE365_USERNAME");
        ArgumentNullException.ThrowIfNull(grAddress);
        mailMessage.To.Add(new(grAddress));
        mailMessage.From = new(grAddress, $"{contactModel.Names} <{contactModel.Email}>");
        if (mailMessage.ReplyToList.Any(mm => mm.Address.Equals(grAddress, StringComparison.InvariantCultureIgnoreCase)))
        {
            mailMessage.ReplyToList.Remove(mailMessage.ReplyToList.First(x => x.Address.Equals(grAddress, StringComparison.InvariantCultureIgnoreCase)));
        }
        if (string.IsNullOrWhiteSpace(contactModel.Email))
        {
            ArgumentNullException.ThrowIfNull(contactModel.Email);
        }

        mailMessage.ReplyToList.Add(contactModel.Email);
        return mailMessage;
    }

    private static string BuildEmailContent(ContactModel contactModel)
    {
        StringBuilder builder = new();
        builder.Append("<table>");

        builder.Append("<tr>");
        builder.Append("<td>Expéditeur :</td>");
        builder.Append($"<td>{contactModel.Names}</td>");
        builder.Append("</tr>");

        builder.Append("<tr>");
        builder.Append("<td>Courriel :</td>");
        builder.Append($"<td>{contactModel.Email}</td>");
        builder.Append("</tr>");

        builder.Append("<tr>");
        builder.Append("<td>Téléphone :</td>");
        builder.Append($"<td>{contactModel.Phone}</td>");
        builder.Append("</tr>");

        builder.Append("<tr>");
        builder.Append("<td>Message :</td>");
        builder.Append($"<td>{contactModel.Message}</td>");
        builder.Append("</tr>");

        builder.Append("</table>");
        return builder.ToString();
    }

    private static SmtpClient GetSmtpClient()
    {
        string? username = Environment.GetEnvironmentVariable("OFFICE365_USERNAME");
        string? password = Environment.GetEnvironmentVariable("OFFICE365_PASSWORD");
        string? host = Environment.GetEnvironmentVariable("OFFICE365_HOST");

        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(host);

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