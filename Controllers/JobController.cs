using BrainsToDo.Helpers;
using BrainsToDo.Services.jobFetcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class JobController : ControllerBase
{
    private readonly NavJobClient _navJobClient;

    public JobController(NavJobClient navJobClient)
    {
        _navJobClient = navJobClient;
    }

    // GET /Job
    [HttpGet]
    public async Task<ActionResult<JobItemsEnvelope>> GetJobs()
    {
        const string navUrl = "https://pam-stilling-feed.nav.no/api/v1/feed";

        // Forward the caller’s Authorization header (if present)
        string? bearerHeader = Request.Headers["Authorization"].FirstOrDefault();
        string? bearerToken  = bearerHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) == true
            ? bearerHeader["Bearer ".Length..].Trim()
            : bearerHeader;

        var envelope = await _navJobClient.GetAsync<JobItemsEnvelope>(
            navUrl,
            bearerToken);

        if (envelope is null)
            return StatusCode(StatusCodes.Status502BadGateway,
                              "Unable to fetch NAV job feed.");

        return Ok(envelope);
    }
}