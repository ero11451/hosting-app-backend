using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/github")]
[ApiController]
public class GitHubController : ControllerBase
{
    private readonly GitHubUserService _gitHubService;

    public GitHubController()
    {
        _gitHubService = new GitHubUserService();
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserProfile([FromQuery] string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
            return BadRequest("❌ Missing access token.");

        var user = await _gitHubService.GetUserProfileAsync(accessToken);
        return Ok(user);
    }

    [HttpGet("repos")]
    public async Task<IActionResult> GetUserRepositories([FromQuery] string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
            return BadRequest("❌ Missing access token.");

        var repos = await _gitHubService.GetUserRepositoriesAsync(accessToken);
        return Ok(repos);
    }
}
