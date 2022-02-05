using System.ComponentModel.DataAnnotations;

namespace GalerieRenovation.Models;

public record Service(string Seo,
    string? Title,
    string? Subtitle,
    string? Description1,
    string? Description2,
    string? Image,
    IEnumerable<string>? Descriptions,
    IEnumerable<string>? ShortDescriptions);

public record Carousel(string UniqueId,
    IEnumerable<CarouselItem> Items);

public record CarouselItem(string? Title,
    string? Image,
    string? Description,
    string? Route,
    string BackgroundColor = "primary",
    string TextColor = "light");

public record Informations(string Url);

public record Achievement(string Title,
    string? Description,
    string? Location,
    IEnumerable<string>? Images);

public record Product(string Title,
    string? Subtitle,
    string? Description,
    double? Price,
    string? Unit,
    IEnumerable<string>? Images);


public abstract class ContactContextBase
{
    public string? Names { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool SubscribeToNewsletter { get; set; }
}

public class ContactContext : ContactContextBase
{
    public string? Message { get; set; }

}

public class BusinessDevContext : ContactContextBase
{
    public string Address { get; set; }
}