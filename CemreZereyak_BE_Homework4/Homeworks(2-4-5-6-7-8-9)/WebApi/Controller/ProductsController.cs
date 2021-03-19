//using WebApi.Data;
using WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.ModeData;


namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private ProductDbContext _context;
        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<ProductModel> datas = _context.Products.ToList();


            return Ok(datas);
        }
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            var data = _context.Products.FirstOrDefault(c => c.Id == id);
            return data;
        }
        [HttpPut("{id}")]
        public ProductModel Put([FromBody] ProductModel product)
        {
            var editProducts = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            editProducts.Id = product.Id;
            editProducts.Name = product.Name;
            editProducts.Price = product.Price;


            return product;
        }

        [HttpPost]
        public void Post([FromBody] ProductModel books)
        {
            _context.Products.Add(books);

        }
        [HttpDelete]
        public void Delete(int id)
        {
            var deleteProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(deleteProduct);
        }


    }
}
