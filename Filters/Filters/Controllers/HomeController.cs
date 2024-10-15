using Filters.Filters;
using Filters.Models;
using Filters.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        /*
         * 
         * Authorization Filter
         *  - İlk çalışır
         * Resource Filter
         *  - AOP ihtiyaçlarını karşılayabilir (Bir func. çalışırken ya da çalıştıktan sonra...)
         * Action Filter
         *  - Çalışacak olan action'un parametreleri HttpContext'i ya da response değerlerine erişebilir.
         * Endpoint
         * - Action çalışmaya başlamadan önce ya da hemen sonra yakalanır. 
         * Exception Filter
         * - Hata yakalar
         * Result Filter
         * 
         * 
         */
        [Stopwatch]
        public IActionResult Index()
        {
            return View();
        }
        [OutOfRange]
        public IActionResult Privacy(int number)
        {
            if (number<0 || number >100)
            {
                throw new ArgumentOutOfRangeException(nameof(number), $"number parametresi 1 ile 100 arasında olmalı");
            }
            return View();
        }
        [Stopwatch]
        public IActionResult FindProduct(int id) { 
        
            var product = productService.GetProduct(id);
            if (product != null) {
                return View(product);
            }
            return NotFound();
        }

        [IsExists]
        [HttpPut]
        public IActionResult Update(int id, Product product) { 

            return Ok(product);        
        }

        [IsExists]
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            return Ok(new {message =$"{id} id'li ürün silindi"});
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
