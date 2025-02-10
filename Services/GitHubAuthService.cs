using System.Net.Http.Headers;
using Newtonsoft.Json;

public class OAuthTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}

public class GitHubAuthService
{
    private readonly HttpClient _httpClient;

    public GitHubAuthService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> ExchangeCodeForTokenAsync(string code)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://github.com/login/oauth/access_token");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", "Ov23lim8oc2IGYl3TvE5" },
            { "client_secret", "bbfe64cefb64db6e589e50b6a7ab50647d5af629" },
            { "code", code }
        });

        var response = await _httpClient.SendAsync(request);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var tokenResponse = JsonConvert.DeserializeObject<OAuthTokenResponse>(jsonResponse);

        return tokenResponse?.AccessToken;
    }
}
