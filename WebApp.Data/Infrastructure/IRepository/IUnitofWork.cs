using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Data.Infrastructure.IRepository
{
    public interface IUnitofWork
    {

        //variable // to access IRepository 
        ICategoryRepository Category { get; }
        IProductRepository Product { get; } 
        
       

        void save();

    }
}
