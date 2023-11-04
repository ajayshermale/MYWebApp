using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

using WebApp.Models.Models;

namespace WebApp.Models.ViewModel
{
    public class ProductVM
    {
        public Product? product { get; set; }  = new Product();

        [ValidateNever]
        public IEnumerable<Product> products { get; set; } = new List<Product>();
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; } //s= new Category();
    }
}
