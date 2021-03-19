using FirstApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Mapper
{
    public static class MappingExtensions
    {
        public static List<CustomerModel> ToProducts(this List<CustomerDTO> data)
        {
            return data.Select(p => new CustomerModel
            {
                Id=p.Id,
                Name=p.Name,
                Age=p.Age,
                Address=p.Address,
                Salary=p.Salary,
                RequestedLimit=p.RequestedLimit,
                TCKN=p.TCKN,
                Account=p.Account,
                OfferedLimit=p.OfferedLimit,
                CreditScore=p.CreditScore

            }).ToList();
        }


        public static List<CustomerModel> ToProductDTOs(this List<CustomerModel> data)
        {
            return data.Select(p => new CustomerModel
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                Address = p.Address,
                Salary = p.Salary,
                RequestedLimit = p.RequestedLimit,
                TCKN = p.TCKN,
                Account = p.Account,
                OfferedLimit = p.OfferedLimit,
                CreditScore = p.CreditScore

            }).ToList();
        }
    }
}
