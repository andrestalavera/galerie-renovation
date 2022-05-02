using GalerieRenovation.Models;
using System.Net.Http.Json;

namespace GalerieRenovation.Web.Services;
public interface IContactService
{
    Task Send(ContactModel contact);
}
public class ContactService : IContactService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<ContactService> logger;

    public ContactService(IHttpClientFactory httpClientFactory, ILogger<ContactService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
    }

    public async Task Send(ContactModel contact)
    {
        logger.LogInformation("Attempt to POST data");
        if (contact is null)
        {
            ArgumentNullException.ThrowIfNull(contact);
        }

        HttpClient client = httpClientFactory.CreateClient("contact");
        await client.PostAsJsonAsync("ContactFunction", contact);
    }
}
