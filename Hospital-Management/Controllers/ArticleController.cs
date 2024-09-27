using Hospital_Management.Repository;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class ArticleController(IArticleServices articleServices) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var articles = await articleServices.GetArticles(page, pageSize);

            return View(articles);
        }

        public async Task<IActionResult> Search(string searchString, int page = 1, int pageSize = 10, string? searchProperty = null)
        {
            var articles = await articleServices.Search(page, pageSize, searchString, searchProperty);

            ViewBag.SearchString = searchString;

            return View("Index", articles);
        }

        [HttpGet("Article/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var article = await articleServices.GetById(id);

            return View(article);
        }
    }
}
