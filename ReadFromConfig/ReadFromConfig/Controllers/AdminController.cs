using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReadFromConfig.Models;

namespace ReadFromConfig.Controllers
{
    public class AdminController : Controller
    {
        //private readonly EmailOptions emailOptions;

        private readonly IOptionsMonitor<EmailOptions> optionsMonitor;


        /*
         * IOptions<T> kullanırsanız eğer, uygulama çalışırken değişiklik algılamaz.
         * IOptionsSnapshot<T> : Her request'de konfigürasyonun yeniden değerlendirilmesini istiyorsak bu interface'i tercih ediyoruz.
         * IOptionsMonitor<T> : T instance'leri için birden fazla option almak ve ya option üzerindeki değişikleri yöntebilirsini
         *

         * 
         */
        public AdminController(IOptionsMonitor<EmailOptions> options)
        {
           // emailOptions = options.CurrentValue;
            optionsMonitor = options;
        }

        public IActionResult Index()
        {

            return Content($"{optionsMonitor.CurrentValue.Email}, {optionsMonitor.CurrentValue.Host}");
        }
    }
}
