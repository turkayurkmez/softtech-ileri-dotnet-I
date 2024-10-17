using eshop.Catalog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Contracts
{
    public interface IRepository<T> where T : class,IEntity,new()
    {
        //YAGNI: You ain't gonna need it!
        Task<IEnumerable<T>> GetAllAsync();
        
    }
}
