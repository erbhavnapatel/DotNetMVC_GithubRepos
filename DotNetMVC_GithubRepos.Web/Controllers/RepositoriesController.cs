using DotNetMVC_GithubRepos.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetMVC_GithubRepos.Web.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly AppDbContext _context;

        public RepositoriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            var pageSize = 5;
            var repositories = _context.Repositories.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                repositories = repositories.Where(r => r.Name.Contains(searchString));
            }

            var paginatedRepositories = await repositories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(await repositories.CountAsync() / (double)pageSize);

            ViewData["SearchString"] = searchString;

            return View(paginatedRepositories);
        }
    }
}
