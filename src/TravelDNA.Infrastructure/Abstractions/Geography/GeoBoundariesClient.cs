
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace TravelDNA.Api.Services;

public sealed class GeoBoundariesClient(HttpClient httpClient)
{
    public async Task<JsonNode?> GetBoundaryAsync(
        string iso3,
        string boundaryType,
        CancellationToken cancellationToken = default)
    {
        var metadataUrl =
            $"https://www.geoboundaries.org/api/current/gbOpen/{iso3}/{boundaryType}/";

        var metadata = await httpClient.GetFromJsonAsync<JsonNode>(
            metadataUrl,
            cancellationToken);

        if (metadata is null)
            return null;

        var geoJsonUrl =
            metadata["simplifiedGeometryGeoJSON"]?.GetValue<string>();

        geoJsonUrl ??=
            metadata["gjDownloadURL"]?.GetValue<string>();

        if (string.IsNullOrWhiteSpace(geoJsonUrl))
            throw new InvalidOperationException(
                "No GeoJSON download URL was provided.");

        return await httpClient.GetFromJsonAsync<JsonNode>(
            geoJsonUrl,
            cancellationToken);
    }
}
