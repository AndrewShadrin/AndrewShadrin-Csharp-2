using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDBSOA.Models;

namespace EmployeeDBSOA.Controllers
{
    public class EmployeesController : ApiController
    {
        private DataEmployees data = new DataEmployees();

       
        public List<Employee> GetAllEmployees()
        {
            return data.GetList();
        }

        public IHttpActionResult GetEmployee(int id)
        {
            var employee = data.GetEmployeeId(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

    }
}
