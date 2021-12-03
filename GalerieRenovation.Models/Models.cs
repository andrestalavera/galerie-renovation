namespace GalerieRenovation.Models;
public record Service(string Seo,
    string Title,
    string Subtitle,
    string Description1,
    string Description2,
    IEnumerable<string> Descriptions,
    IEnumerable<string> ShortDescriptions);
public record Carousel(string UniqueId, IEnumerable<CarouselItem> Items);
public record CarouselItem(string Description, string ImageUrl);
public record Informations(string Url);