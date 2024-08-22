using DotNetMVC_GithubRepos.Application.Interfaces;
using DotNetMVC_GithubRepos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetMVC_GithubRepos.Application.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IGithubService _gitHubService;
        private readonly AppDbContext _context;

        public RepositoryService(IGithubService gitHubService, AppDbContext context)
        {
            _gitHubService = gitHubService;
            _context = context;
        }

        public async Task FetchAndSaveDataAsync()
        {
            var repositories = await _gitHubService.FetchRepositoriesAsync("python"); // Adjust query as needed

            _context.Repositories.RemoveRange(await _context.Repositories.ToListAsync());
            await _context.Repositories.AddRangeAsync(repositories);
            await _context.SaveChangesAsync();
        }
    }
}
