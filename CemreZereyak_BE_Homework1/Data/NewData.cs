using FirstApplication.Model;
using System.Collections.Generic;

namespace FirstApplication.Data
{
    public class NewData
    {
        public List<ProductModel> Products = new List<ProductModel>();

        public NewData()
        {
            for (int i = 1; i <= 10; i++)
            {
                Products.Add(new ProductModel { Id = i, Name = "Product_" + i, Price = 100 + (i * 10) });
            }
        }
    }
}
