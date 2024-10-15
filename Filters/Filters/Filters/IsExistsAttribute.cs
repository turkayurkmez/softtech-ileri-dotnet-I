using Microsoft.AspNetCore.Mvc;

namespace Filters.Filters
{
    public class IsExistsAttribute : TypeFilterAttribute
    {
        /*
         * Eğer, kullanmak istediğiniz filter, dependency injection ile oluşacaksa; bu işlem için TypeFilterAttribute 
         * kullanıyoruz!
         */
        public IsExistsAttribute() : base(typeof(IsExistsFilter))
        {
        }

        
    }
}
