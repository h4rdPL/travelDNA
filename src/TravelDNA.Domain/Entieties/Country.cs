public class Country
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string OfficialName { get; set; } = null!;

    public string Iso2Code { get; set; } = null!;

    public string Iso3Code { get; set; } = null!;

    public string? Capital { get; set; }

    public string? Continent { get; set; }

    public string? FlagEmoji { get; set; }

    public ICollection<AdministrativeArea> AdministrativeAreas
        { get; set; } = [];
}

