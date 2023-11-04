using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Models;

namespace WebApp.Data.Infrastructure.IRepository
{
    public interface IProductRepository : IRepository<Product>   
    {
        void Update(Product product);
    }
}
