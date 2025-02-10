using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[Route("auth/github")]
public class GitHubAuthController : Controller
{
    private readonly IConfiguration _config;
    private readonly GitHubAuthService _authService;

    public GitHubAuthController(IConfiguration config, GitHubAuthService authService)
    {
    _authService = authService;        
        _config = config;
    }
    // GET: /auth/github/login
    [HttpGet("login")]
    public IActionResult Login()
    {
        var clientId = _config["GitHub:ClientId"] ?? "Ov23lim8oc2IGYl3TvE5";
        var redirectUri = _config["GitHub:RedirectUri"];
        var scope = "repo"; // Grants access to user's repositories

        var githubOAuthUrl = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}";
        return Redirect(githubOAuthUrl);
    }

    // GET: /auth/github/callback
    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code)
    {
       if (string.IsNullOrEmpty(code))
        {
            return BadRequest("Missing authorization code.");
        }

        var tokenResponse = await _authService.ExchangeCodeForTokenAsync(code);

        if (string.IsNullOrEmpty(tokenResponse))
        {
            return StatusCode(500, "Failed to retrieve GitHub access token.");
        }

        return Ok(new { token = tokenResponse });
    }



}
