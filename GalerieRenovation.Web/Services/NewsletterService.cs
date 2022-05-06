namespace GalerieRenovation.Web.Services; 

public interface INewsletterService
{
    Task<bool> SubscribeAsync(string email);
    Task<bool> UnsubscribeAsync(string email);
}

public class NewsletterService : INewsletterService
{
    public Task<bool> SubscribeAsync(string email)
    {
        return Task.FromResult(true);
    }

    public Task<bool> UnsubscribeAsync(string email)
    {
        return Task.FromResult(false);
    }
}