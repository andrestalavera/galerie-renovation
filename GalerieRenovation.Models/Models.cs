using System.ComponentModel.DataAnnotations;

namespace GalerieRenovation.Models;

public record CardItem(string Seo,
    string? Header,
    string? Title,
    string? Text,
    string? Target,
    string? ImageUrl,
    string? Footer,
    IEnumerable<string>? Items);

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
    string? Description,
    double? Price,
    string? Unit,
    IEnumerable<string>? Images);


public abstract class ContactContextBase
{
    [Required]
    public string? Names { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? Phone { get; set; }

    public bool Newsletter { get; set; } = true;
}

public class ContactModel : ContactContextBase
{
    [Required]
    public string? Message { get; set; }

}

public class BusinessDevContext : ContactContextBase
{
    public string? Address { get; set; }
}