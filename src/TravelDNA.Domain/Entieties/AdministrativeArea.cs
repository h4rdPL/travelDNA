public class AdministrativeArea
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? LocalName { get; set; }
    public string? GeoBoundaryId { get; set; }
    public int AdminLevel { get; set; }
    public ICollection<AdministrativeArea> Children
        { get; set; } = [];
}