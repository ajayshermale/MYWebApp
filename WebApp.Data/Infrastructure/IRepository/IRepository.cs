using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Data.Infrastructure.IRepository
{
    //generic interface common function is placed here to avoid duplicate code 
    public  interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includePropties= null);

        //Expression to resolve linq query // func return 2 values class and bool
        T Get(Expression<Func<T, bool>> predicate, string? includePropties = null);

        void Add(T entity);

        //update may be different 
       //  void Update(T entity);  

        void Delete(T entity);


        //multiple data can be deleted class with multiple list can be pass to delete 
        void DeleteRange(IEnumerable<T> entity);

         

    }
}
