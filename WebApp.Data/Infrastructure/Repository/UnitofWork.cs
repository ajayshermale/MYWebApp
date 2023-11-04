using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.Infrastructure.IRepository;

namespace WebApp.Data.Infrastructure.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private ApplicationDbConext _context;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }
        public IEmployeeRepository Employee { get; private set; }

        public UnitofWork(ApplicationDbConext context) 
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
           
        }
       
        public void save()
        {
            _context.SaveChanges();
        }
    }
}
