using Microsoft.AspNetCore.Mvc;

namespace DotNetMVC_GithubRepos.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult RedirectAction()
        {
            return RedirectToAction("Index", "Repositories");
        }
    }
}
