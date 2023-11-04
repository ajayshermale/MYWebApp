using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Infrastructure.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        //non generic method update 
        public void Update(Category category);

    }
}
