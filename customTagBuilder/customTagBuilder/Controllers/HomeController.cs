using customTagBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace customTagBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int pageNo = 1)
        {
            var employees = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

            var perPage = 2;
            //var pagesCount = Math.Ceiling((double)employees.Length / perPage);

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = pageNo,
                ItemsPerPage = perPage,
                TotalCount = employees.Length
            };

            var start = (pageNo - 1) * perPage;
            var end = start + perPage;
            var pagingEmployees = employees.ToList().Take(start..end);

            var model = new IndexViewModel
            {
                Items = pagingEmployees,
                PagingInfo = pagingInfo
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
