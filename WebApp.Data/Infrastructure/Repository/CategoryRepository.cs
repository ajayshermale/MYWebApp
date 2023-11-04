using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApp.Data.Infrastructure.IRepository;
using WebApp.Models;

namespace WebApp.Data.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbConext _context;
        public CategoryRepository(ApplicationDbConext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            //_context.Categories.Update(category);  

            var categoryDB = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if(categoryDB != null)
            {
                categoryDB.Name = category.Name;
                categoryDB.DisplayOrder= category.DisplayOrder;
            }
        }
    }
}
