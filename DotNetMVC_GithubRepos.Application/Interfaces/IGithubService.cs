using DotNetMVC_GithubRepos.Domain.Entities;

namespace DotNetMVC_GithubRepos.Application.Interfaces
{
    public interface IGithubService
    {
        Task<IEnumerable<GitRepository>> FetchRepositoriesAsync(string searchQuery);
    }
}
