using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Infrastructure.IRepository;
using WebApp.Data.Infrastructure.Repository;
using WebApp.Models.Models;


namespace MYWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IEmployeeRepository _employee;

        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }


        public IActionResult Index()
        {
            _employee.Add();
            return View();
        
        }



    }
}
