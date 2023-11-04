using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.Infrastructure.IRepository;
using WebApp.Models.Models;

namespace WebApp.Data.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ApplicationDbConext _context;
        public ProductRepository(ApplicationDbConext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            // _context.Products.Update(product);
            var productDB = _context.Products.FirstOrDefault(x => x.Id == product.Id);

            if(productDB != null)
            {
                productDB.Name = product.Name;
                productDB.Description = product.Description;
                productDB.Price = product.Price;
                if(product.ImageURL != null)
                {
                    productDB.ImageURL = product.ImageURL;
                }               
                
                
            }

        }
    }
}
