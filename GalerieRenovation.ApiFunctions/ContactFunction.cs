using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GalerieRenovation.Models;
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
        if (request.Body is null)
        {
            ArgumentNullException.ThrowIfNull(request.Body);
        }
        string body = await new StreamReader(request.Body).ReadToEndAsync();
        ContactModel contactModel = JsonConvert.DeserializeObject<ContactModel>(body);
        MailMessage mailMessage = new()
        {
            Body = BuildEmail(contactModel),
            IsBodyHtml = true,
            Priority = MailPriority.High,
            Subject = "Contact www.galerierenovation.com"
        };
        string? grAddress = Environment.GetEnvironmentVariable("OFFICE365_USERNAME");
        if (string.IsNullOrEmpty(grAddress))
        {
            ArgumentNullException.ThrowIfNull(grAddress);
        }
        mailMessage.From = new(grAddress);
        mailMessage.To.Add(new(grAddress));
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

    private static string BuildEmail(ContactModel contactModel)
    {
        StringBuilder builder = new ();
        builder.Append("<table>");
        builder.Append("<tr>");

        builder.Append("<td>");
        builder.Append("Expéditeur :");
        builder.Append("</td>");
        builder.Append("<td>");
        builder.Append(contactModel.Names);
        builder.Append("</td>");

        builder.Append("<td>");
        builder.Append("Courriel :");
        builder.Append("</td>");
        builder.Append("<td>");
        builder.Append(contactModel.Email);
        builder.Append("</td>");

        builder.Append("<td>");
        builder.Append("Téléphone :");
        builder.Append("</td>");
        builder.Append("<td>");
        builder.Append(contactModel.Phone);
        builder.Append("</td>");

        builder.Append("</tr>");
        builder.Append("</table>");
        return builder.ToString();
    }

    private static SmtpClient GetSmtpClient()
    {
        string? username = Environment.GetEnvironmentVariable("OFFICE365_USERNAME");
        string? password = Environment.GetEnvironmentVariable("OFFICE365_PASSWORD");
        string? host = Environment.GetEnvironmentVariable("OFFICE365_HOST");

        if (string.IsNullOrWhiteSpace(username))
        {
            ArgumentNullException.ThrowIfNull(username);
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            ArgumentNullException.ThrowIfNull(password);
        }
        if (string.IsNullOrWhiteSpace(host))
        {
            ArgumentNullException.ThrowIfNull(host);
        }

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