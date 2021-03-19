using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ModeData;
using WebApi.Model;

namespace WebApi.Context
{
    public class DataSend
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProductDbContext>>()))
            {
                if (context.Products.Any() && context.Customers.Any())
                {
                    return;
                }

                context.Products.Add(new ProductModel
                {
                    Id = 1,
                    Name = "Shoe",

                    Price = 200,
                    

                });
                context.Customers.Add(new CustomerModel
                {
                    Id = 2,
                    Name = "Cemre",
                    LastName = "Zereyak",
                    

                });
                context.SaveChanges();
            }

        }
    }
}
