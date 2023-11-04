using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data.Infrastructure.IRepository;
using WebApp.Models.Models;
using WebApp.Models.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Drawing;

namespace MYWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitofWork _unitofWork;
        private IWebHostEnvironment _hostingEnvironment;
        public ProductController(IUnitofWork UnitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = UnitofWork;
            _hostingEnvironment = webHostEnvironment;
        }

        #region APICALL
        [HttpGet]
        public IActionResult AllProduct()
        {
            var products = _unitofWork.Product.GetAll(includePropties: "Category");
            return Json(new { data = products });
        }

        #endregion

        public IActionResult Index()
        {
            ProductVM productVM = new ProductVM();
            productVM.products = _unitofWork.Product.GetAll();
          
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? Id)
        {
            ProductVM productVM = new ProductVM()
            {
                product = new(),
                Categories = _unitofWork.Category.GetAll().Select(
                    x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
            };

            if (Id == null || Id == 0)
            {
                return View(productVM);

            }
            else
            {
                productVM.product = _unitofWork.Product.Get(x => x.Id == Id);

                if (productVM.product == null)
                {
                    return NotFound();
                }
                else
                {
                    productVM.product.ImageURL = productVM.product.ImageURL = Path.Combine($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + productVM.product.ImageURL);
                    return View(productVM);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM productVM, [ValidateNever]IFormFile file)
        {
            //if (ModelState.IsValid)
            //{
                string Filename = string.Empty;
                if (file != null)
                {
                    string UploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImage");
                    Filename = Guid.NewGuid().ToString() + file.FileName;
                    string FilePath = Path.Combine(UploadDir, Filename);

                    if (productVM.product.ImageURL != null)
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, productVM.product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var Filestream = new FileStream(FilePath, FileMode.Create))
                    {
                        file.CopyTo(Filestream);
                    }
                    productVM.product.ImageURL = @"\ProductImage\" + Filename;
                }

                if (productVM.product.Id == 0)
                {
                    _unitofWork.Product.Add(productVM.product);
                    TempData["Success"] = "Product Created Successfully!";
                }
                else
                {
                productVM.product.ImageURL = productVM.product.ImageURL.Replace($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" ,string.Empty);
                    _unitofWork.Product.Update(productVM.product);
                    TempData["Success"] = "Product Updated Successfully!";
                }
                _unitofWork.save();

                return RedirectToAction("Index");
           // }
            return RedirectToAction("Index");


        }
        //[HttpGet]

        //public IActionResult Delete(int? id)
        //{ 
        //    if(id == null || id ==0)
        //    {
        //        return NotFound();
        //    }

        //    var product = _unitofWork.Product.Get(x => x.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);   
        //}

        #region DeleteAPICALL

        [HttpDelete]

        public IActionResult Delete(int? id)
        {
            var product = _unitofWork.Product.Get(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error in Fetching Data" });
            }
            else
            {
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, product.ImageURL.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitofWork.Product.Delete(product);
                _unitofWork.save();
                return Json(new { success = true, message = "Data deleted successfully " });
            }

        }

        #endregion
    }
}
