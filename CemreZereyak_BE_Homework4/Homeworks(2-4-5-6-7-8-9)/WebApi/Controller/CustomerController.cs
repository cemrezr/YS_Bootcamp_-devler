using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ModeData;
using WebApi.Model;

namespace WebApi.Controller
{
    public class CustomerController : ControllerBase
    {
        private ProductDbContext _context;
        public CustomerController(ProductDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<CustomerModel> datas = _context.Customers.ToList();


            return Ok(datas);
        }
        [HttpGet("{id}")]
        public CustomerModel Get(int id)
        {
            var data = _context.Customers.FirstOrDefault(c => c.Id == id);
            return data;
        }
        [HttpPut("{id}")]
        public CustomerModel Put([FromBody] CustomerModel customer)
        {
            var editCustomer = _context.Customers.FirstOrDefault(x => x.Id == customer.Id);
            editCustomer.Id = customer.Id;
            editCustomer.Name = customer.Name;
            editCustomer.LastName = customer.LastName;
            return customer;
        }

        [HttpPost]
        public void Post([FromBody] CustomerModel customer)
        {
            _context.Customers.Add(customer);

        }
    }
}
