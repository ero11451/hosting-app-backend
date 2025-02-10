using Octokit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GitHubUserService
{
    public async Task<User> GetUserProfileAsync(string accessToken)
    {
        var github = new GitHubClient(new ProductHeaderValue("WebHostingPlatform"))
        {
            Credentials = new Credentials(accessToken)
        };

        return await github.User.Current();
    }

    public async Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync(string accessToken)
    {
        var github = new GitHubClient(new ProductHeaderValue("WebHostingPlatform"))
        {
            Credentials = new Credentials(accessToken)
        };

        return await github.Repository.GetAllForCurrent();
    }
}
