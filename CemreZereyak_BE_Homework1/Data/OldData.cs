using FirstApplication.Model;
using System.Collections.Generic;

namespace FirstApplication.Data
{

    // singleton desing pattern
    public class OldData
    {
        private static volatile OldData _instance = null;
        private static readonly object padLock = new object();

        public static OldData Instance
        {
            get
            {
                lock (padLock)
                {
                    if(_instance == null)
                    {
                        _instance = new OldData();
                    }
                }
                return _instance;
            }
        }


        private OldData()
        {
            FillData();
        }

        private void FillData()
        {
            for (int i = 1; i <= 10; i++)
            {
                Products.Add(new ProductModel { Id = i, Name = "Product_" + i, Price = 100 + (i * 10) });
            }
        }

        public List<ProductModel> Products = new List<ProductModel>();

    }
}
