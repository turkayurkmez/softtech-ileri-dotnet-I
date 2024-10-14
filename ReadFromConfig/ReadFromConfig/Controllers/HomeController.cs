using Microsoft.AspNetCore.Mvc;
using ReadFromConfig.Models;
using System.Diagnostics;

namespace ReadFromConfig.Controllers
{
    public class HomeController : Controller
    {

        /*
         * 1. Bind: Bir abstract sınıfın concrete olarak kullanılmasına izin verir.
         * 2. Get: doğrudan bir instance oluşturur.
         * 
         */
        private readonly ILogger<HomeController> _logger;

        private IConfiguration configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var mailSettings = new EmailOptions("a@b.com");
            //1. Bind fonksiyonu: appsettings dosyasından değeri oku ve -> instance'a bind et
            configuration.GetSection(EmailOptions.MailConfigurationName).Bind(mailSettings);

            string content = $"Host: {mailSettings.Host}, Eposta:{mailSettings.Email}";
            
            return Content(content);
        }

        public IActionResult Privacy()
        {
            EmailOptions mailSettings = configuration.GetSection(EmailOptions.MailConfigurationName).Get<EmailOptions>();
            string content = $"Host: {mailSettings.Host}, Eposta:{mailSettings.Email} ";
            return Content(content);


        }

        public IActionResult Sample()
        {
            var mailSettings = new EmailOptions("x@y.com");
            configuration.GetSection(EmailOptions.MailConfigurationName).Bind(mailSettings);
            var content = $"Host:{mailSettings.Host} Email:{mailSettings.Email}, Pass: {mailSettings.Password}";
            return Content(content);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
