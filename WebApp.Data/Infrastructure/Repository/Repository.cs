using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.Infrastructure.IRepository;

namespace WebApp.Data.Infrastructure.Repository
{
    //generic repository is directly call by dataaccess layer 
    public class Repository<T> : IRepository<T> where T : class
    {
        //Variable 
        private readonly ApplicationDbConext _context;
        private readonly DbSet<T> _dbSet;

        //constructor 
        public Repository(ApplicationDbConext context)
        {
            _context = context;

            //_dbset = _context.Cartgories //here .Set<T>() Categories 
            _dbSet = _context.Set<T>();

        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        //expression lamda will be in predicate form 
        public T Get(Expression<Func<T, bool>> predicate, string? includePropties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(predicate);
            if (includePropties != null)
            {
                foreach (var item in includePropties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            //return _dbSet.Where(predicate).FirstOrDefault();
            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(string? includePropties = null)
        {
            IQueryable<T> query = _dbSet;
            if (includePropties != null)
            {
                foreach (var item in includePropties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            //return _dbSet.ToList();
            return query.ToList();

        }

        //public void Update(T entity)
        //{
        //    _dbSet.Update(entity);
        //}
    }
}
