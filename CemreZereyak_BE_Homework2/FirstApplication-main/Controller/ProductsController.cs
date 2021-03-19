using FirstApplication.Data;
using FirstApplication.Mapper;
using FirstApplication.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FirstApplication.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //private readonly List<ProductModel> _data = new List<ProductModel>();
        //private readonly DummyDataOld _dummyDataOld;

        private readonly IDatabase _database;

        public ProductsController(IDatabase database)
        {
            _database = database;

            List<CustomerModel> customerModel = new List<CustomerModel>();
            customerModel.Add(new CustomerModel { Id = 21, Name = "Cemre", Age = 19, Address = "xxx", Salary = 5000, RequestedLimit = 6000, TCKN = "11953465692", Account = true, OfferedLimit = 4000, CreditScore = 160 });
            var newData = customerModel.ToProductDTOs();
        }

       
           
        

        

        [HttpGet]
        public List<CustomerModel> Get()
        {
            return _database.Products.ToList();
        }

        [HttpGet("{id}")]
        public CustomerModel Get(int id)
        {
            var data = _database.Products.FirstOrDefault(c => c.Id == id);
            return data;
        }

        [HttpPost]
        public void Post([FromBody] CustomerModel product)
        {
            _database.Products.Add(product);

           
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<CustomerModel>> Delete(int id)
        {
            CustomerModel productModel = _database.Products.FirstOrDefault(c => c.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            _database.Products.Remove(productModel);

            return _database.Products;
        }
    }
}