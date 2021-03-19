using FirstApplication.Data;
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

        private readonly OldData _oldData;

        private readonly NewData _newData;


        public ProductsController(NewData newData)
        {
            // _dummyDataOld = DummyDataOld.Instance;

            _newData = newData;
        }

        [HttpGet]
        public List<ProductModel> Get()
        {

            return _newData.Products;
        }

        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            var data = _newData.Products.FirstOrDefault(c => c.Id == id);
            return data;
        }

        [HttpPost]
        public void Post([FromBody] ProductModel product)
        {
            _newData.Products.Add(product);
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<ProductModel>> Put(int id, ProductModel updatedProductModel)
        {
            ProductModel productModel = _newData.Products.FirstOrDefault(c => c.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            productModel.Name = updatedProductModel.Name;
            productModel.Price = updatedProductModel.Price;


            return _newData.Products;
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<ProductModel>> Delete(int id)
        {
            ProductModel productModel = _newData.Products.FirstOrDefault(c => c.Id == id);
            if(productModel == null)
            {
                return NotFound();
            }

            _newData.Products.Remove(productModel);

            return _newData.Products;
        }

        
    }
    }

