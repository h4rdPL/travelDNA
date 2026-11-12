public interface IGeoBoundariesClient
{
    Task<string?> GetBoundaryAsync(
        string iso3,
        string boundaryType,
        CancellationToken cancellationToken = default);
}