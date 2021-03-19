using WebApi.Model;
using System.Collections.Generic;
using WebApi.Model;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace  WebApi.ModeData
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
    }
}