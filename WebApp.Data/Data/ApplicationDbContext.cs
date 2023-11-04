using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Models;

namespace WebApp.Data
{
    public class ApplicationDbConext :DbContext
    {

        public ApplicationDbConext(DbContextOptions<ApplicationDbConext> options )
        : base(options)
        { 
        
        }  
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product>  Products { get; set; }   
    }

}
