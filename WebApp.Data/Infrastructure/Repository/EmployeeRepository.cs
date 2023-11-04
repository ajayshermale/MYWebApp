using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.Infrastructure.IRepository;
using WebApp.Models.Models;

namespace WebApp.Data.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        void IEmployeeRepository.Add()
        {
            throw new NotImplementedException();
        }

        Employee IEmployeeRepository.Get()
        {
            throw new NotImplementedException();
        }

        void IEmployeeRepository.Update()
        {
            throw new NotImplementedException();
        }
    }
}
