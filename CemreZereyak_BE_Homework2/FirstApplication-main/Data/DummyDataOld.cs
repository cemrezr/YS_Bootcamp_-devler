using FirstApplication.Model;
using System.Collections.Generic;

namespace FirstApplication.Data
{
    // singleton desing pattern
    public class DummyDataOld
    {
        private static volatile DummyDataOld _instance = null;
        private static readonly object padLock = new object();

        public static DummyDataOld Instance
        {
            get
            {
                lock (padLock)
                {
                    if (_instance == null)
                    {
                        _instance = new DummyDataOld();
                    }
                }
                return _instance;
            }
        }

        private DummyDataOld()
        {
            FillData();
        }

        private void FillData()
        {
            for (int i = 1; i <= 10; i++)
            {
                Customers.Add(new CustomerModel { Id = i, Name = "Customer_" + i, Age= 22, Address= i + ".Adress", Salary = 2080 + (i * 10), RequestedLimit=4000, OfferedLimit=4500 });
            }
        }

        public List<CustomerModel> Customers = new List<CustomerModel>();
    }
}