namespace DotNetMVC_GithubRepos.Application.Interfaces
{
    public interface IRepositoryService
    {
        Task FetchAndSaveDataAsync();
    }
}
