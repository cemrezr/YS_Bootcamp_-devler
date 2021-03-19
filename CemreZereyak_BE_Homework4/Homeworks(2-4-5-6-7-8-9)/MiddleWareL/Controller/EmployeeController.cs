using Microsoft.AspNetCore.Mvc;
using MiddleWareL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareL.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Employee> GetByID(int id)
        {
            var employee = new Employee()
            {
                ID = id,
                FirstName = "firstName",
                DateOfBirth = DateTime.Now.AddYears(-24)

            };

            return Ok(employee);
        }
    }
}
