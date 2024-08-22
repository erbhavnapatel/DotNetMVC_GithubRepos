using DotNetMVC_GithubRepos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetMVC_GithubRepos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<GitRepository> Repositories { get; set; }
    }
}
