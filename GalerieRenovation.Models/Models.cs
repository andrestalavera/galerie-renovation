namespace GalerieRenovation.Models;
public record Models(string Seo,
    string Title,
    string Subtitle,
    string Description1,
    string Description2,
    IEnumerable<string> Descriptions,
    IEnumerable<string> ShortDescriptions);
public record Realization(string UniqueId, string Description, string ImageUrl);
public record Informations(string Url);