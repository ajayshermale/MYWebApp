using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Infrastructure.IRepository;
using WebApp.Data.Infrastructure.Repository;
using WebApp.Models;
using WebApp.Models.ViewModel;

namespace MYWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private ApplicationDbConext _context;

        //public CategoryController(ApplicationDbConext context)
        //{
        //    _context = context;
        //}

        //public IActionResult Index()
        //{
        //    IEnumerable<Category> categories = _context.Categories;
        //    return View(categories);
        //}

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Categories.Add(category);
        //        _context.SaveChanges();
        //        TempData["Success"] = "Category Created Successfully!";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        //[HttpGet]
        //public IActionResult Edit(int? Id)
        //{
        //    if(Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _context.Categories.Find(Id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Category category)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        _context.Categories.Update(category);
        //        _context.SaveChanges();
        //        TempData["Success"] = "Category Updated Successfully!";
        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _context.Categories.Find(Id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);

        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteData(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _context.Categories.Find(Id);
        //    if(category == null)
        //    {
        //        return NotFound();
        //    }
        //       _context.Categories.Remove(category);
        //        _context.SaveChanges();
        //        TempData["Success"] = "Category Deleted Successfully!";
        //     return RedirectToAction("Index");

        //}

        private IUnitofWork _unitofWork;

        public CategoryController(IUnitofWork UnitofWork)
        {
            _unitofWork = UnitofWork;
        }

        public IActionResult Index()
        {
            //ienumerable helps in iteration of any list collection // like for loop 
             CategoryVM categoriesvm = new CategoryVM();
             categoriesvm.categories = _unitofWork.Category.GetAll();
            return View(categoriesvm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitofWork.Category.Add(category);
        //        _unitofWork.save();
        //        TempData["Success"] = "Category Created Successfully!";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        [HttpGet]
        public IActionResult CreateUpdate(int? Id)
        {
            CategoryVM Cvm = new CategoryVM();
            if (Id == null || Id == 0)
            {
                return View(Cvm);
            }
            else 
            {
                // var  Editcategory = _unitofWork.Category.Get(x => x.Id == Id);
                Cvm.category = _unitofWork.Category.Get(x => x.Id == Id);

                if (Cvm.category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(Cvm);
                }
            }           

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM VM)
        {
            if (ModelState.IsValid)
            {
                if (VM.category.Id == 0)
                {
                    _unitofWork.Category.Add(VM.category);
                    
                    TempData["Success"] = "Category Created Successfully!";
                    
                }
                else
                {
                    _unitofWork.Category.Update(VM.category);
                    

                    TempData["Success"] = "Category Updated Successfully!";
                   
                }
                _unitofWork.save();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            // var category = _context.Categories.Find(Id);

            var category = _unitofWork.Category.Get(x => x.Id == Id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            //var category = _context.Categories.Find(Id);

            var category = _unitofWork.Category.Get(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            //_context.Categories.Remove(category);
            //_context.SaveChanges();

            _unitofWork.Category.Delete(category);
            _unitofWork.save();
            TempData["Success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");

        }

    }
}
