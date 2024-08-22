using DotNetMVC_GithubRepos.Application.Interfaces;
using DotNetMVC_GithubRepos.Domain.Entities;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DotNetMVC_GithubRepos.Application.Services
{
    public class GitHubService : IGithubService
    {
        private readonly RestClient _client;

        public GitHubService(RestClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<GitRepository>> FetchRepositoriesAsync(string searchQuery)
        {
            var request = new RestRequest("search/repositories", Method.Get);
            request.AddQueryParameter("q", searchQuery);

            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception("Failed to fetch data from GitHub");
            }

            var data = JObject.Parse(response.Content);
            var repositories = new List<GitRepository>();

            foreach (var item in data["items"])
            {
                repositories.Add(new GitRepository
                {
                    Id = item["id"].ToString(),
                    Name = item["name"].ToString(),
                    FullName = item["full_name"].ToString(),
                    Description = item["description"]?.ToString(),
                    CreatedAt = DateTime.Parse(item["created_at"].ToString()),
                    ForksCount = int.Parse(item["forks_count"].ToString())
                });
            }

            return repositories;
        }
    }
}
