
using Microsoft.AspNetCore.Mvc;
using TravelDNA.Api.Services;

namespace TravelDNA.Api.Controllers;

[ApiController]
[Route("api/geography")]
public class GeographyController(
    GeoBoundariesClient geoBoundariesClient) : ControllerBase
{
    [HttpGet("{iso3}/boundaries/{level}")]
    public async Task<IActionResult> GetBoundary(
        string iso3,
        string level,
        CancellationToken cancellationToken)
    {
        var allowedLevels = new[]
        {
            "ADM0", "ADM1", "ADM2", "ADM3", "ADM4", "ADM5"
        };

        iso3 = iso3.ToUpperInvariant();
        level = level.ToUpperInvariant();

        if (iso3.Length != 3 ||
            !iso3.All(char.IsAsciiLetter) ||
            !allowedLevels.Contains(level))
        {
            return BadRequest("Invalid country code or boundary level.");
        }

        var geoJson = await geoBoundariesClient.GetBoundaryAsync(
            iso3,
            level,
            cancellationToken);

        if (geoJson is null)
            return NotFound();

        return Content(
            geoJson.ToJsonString(),
            "application/geo+json");
    }
}
