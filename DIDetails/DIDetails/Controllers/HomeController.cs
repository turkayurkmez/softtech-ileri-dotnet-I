using DIDetails.Models;
using DIDetails.POC;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DIDetails.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISingleton singleton;
        private readonly ITransient transient;
        private readonly IScoped scoped;
        private readonly LifeTimeService service;

        public HomeController(ILogger<HomeController> logger, ISingleton singleton, ITransient transient, IScoped scoped, LifeTimeService service)
        {
            _logger = logger;
            this.singleton = singleton;
            this.transient = transient;
            this.scoped = scoped;
            this.service = service;
        }

        public IActionResult Index()
        {
            ViewBag.Singleton = singleton.Guid.ToString();
            ViewBag.Transient = transient.Guid.ToString();
            ViewBag.Scoped = scoped.Guid.ToString();
            //service'den gelenler:
            ViewBag.ServiceSingleton = service.Singleton.Guid.ToString();
            ViewBag.ServiceTransient = service.Transient.Guid.ToString();
            ViewBag.ServiceScoped = service.Scoped.Guid.ToString();
            return View();
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
