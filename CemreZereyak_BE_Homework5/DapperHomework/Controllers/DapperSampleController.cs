using Dapper;
using DapperHomework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperHomework.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DapperSampleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DapperSampleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public IActionResult DapperSelect()
        {
            /*
            Databasemdeki ilgili tablolardaki bütün verileri listelemek için bir  method oluşturdum ve Query methoduyla 
           Databasede sql komutunu çalıştırdım Geriye dönen datayı  classıma mapledim ve tekrar çektim.

            */
            IEnumerable<Product> products;
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();
                {

                    string sql = "select * from [Production].[Product]";

                    products = db.Query<Product>(sql);
                }



            }
            return Ok();
        }
        public IActionResult DapperInsert()
        {
            /*
               Database'de ProductTest adında bir tablo oluşturdum.
           insert into komutuyla bu tabloma data ekleyecek methodu oluşturdum  göndereceğim parametleri yazdım.
            Dapperın kendi execute methoduna  sorgumu ve parametre olarak oluşturduğum nesnemi gönderdim,  dapper otomatik olarak nesnemin parametleriyle sql tarafındaki parametleri eşleyip 
           databasede insert işlemi yaptı.
           
            */
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }
                string sql = @"insert into dbo.ProductTest (Name, Color) values (@Name, @Color);";

                ProductTest testProduct = new ProductTest { Name = "Shoe", Color = "Black" };
                var affected = db.Execute(sql, testProduct);

            }
            return Ok();
        }

        public IActionResult DapperUpdate()
        {
            /*
             Yazmış olduğum  sql methodlarıyla update islemi yapacağım.
             Dapperın Execute methoduna yazdığım sql sorgusunu ve parametrelerimi gönderiyorum.
            
             */
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }

                string sql = @"Update dbo.ProducTest Set Name = @Name where Id=@Id ";

                var updateProducts = new[] {
                    new {Id =1 , Name="T-shirt"},

                };

                var affected = db.Execute(sql, updateProducts);
            }
            return Ok();
        }

        public IActionResult DapperDelete()
        {
            /*
               Adından da anlaşılacağı gibi silme işlemi yapacapım. dapperın execute methoduna gonderiyorum ve paremetre olarakda Id beklediğim için bir obje
            gönderiyorum ve dapper otomatik olarak databasede sorgumuzda gönderdiğimiz parametreleri eşliyor ve karşılığında bulduğu datayı üzerinde sorugumuzda olan
            delete komunutu çalıştırıyor.
            
             */
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }
                string sql = @"Delete from dbo.ProductTest where Id=@Id";
                var affected = db.Execute(sql,
                    new { Id = 1 }
                );

            }
                return Ok();
        }

        public IActionResult DapperDeleteInQuery()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }
                string sql = @"Delete from dbo.ProductTest where Id=@Id";
                var data = db.Query(sql,
                    new { Id = 1 }
                );

            }
                return Ok();
        }
        public IActionResult DapperSP()
        {
            /*
                ProductTest tabloma select sorgu atan bir SelectProduct adinda bir StoredProcedure olusturdum. Dapper execute methoduna procedurumu yolladım.
               
             */
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }

                string sql = "dbo.SelectProduct";
                db.Execute(sql, null, commandType: CommandType.StoredProcedure);
            }
                return Ok();
        }

        public IActionResult DapperTransaction()
        {
            /*
             Database'de birden cok islem yapmak istediğimizde herhangi bir işlemdeislemde hata alinca digerlerininde kaydinin iptal olması gerektiği durumlarda  
            transaction kullanıyoruz.2 tane farklı tablolara insert islemi yaptim ve doğru çalışırsa eğer transaction commit edilip kaydediliyor.
            
             */
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }

                using (var transaction = db.BeginTransaction())
                {

                    string sql = @"insert into dbo.TestPerson (Name,Color) values (@Name,@color);";

                    ProductTest testPerson = new ProductTest { Name = "Versace", Color = "Glass" };
                    db.Execute(sql, testPerson, transaction);


                    Location productLocation = new Location { Name = "Tuzla Shop", Availability = "1" };
                    sql = @"Insert into [Production].[Location] (Name, Availability) values (@Name, @Availability);";
                    db.Execute(sql, productLocation, transaction);

                    transaction.Commit();
                }
            }
                return Ok();
        }

        public IActionResult DapperOneToMany()
        {
            /*
             Adından da anlaşılacağı gibi birden çoka giden bir ilişkiyi anlatan ve çağıran bir method yazdım.
            İlgili tablolarda bir property başka bir tablodaki birden çok propertyi kapsadığı için bu yöntem kullanılıyor.
             
             */
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }

            

                string sql = "select * from [Production].[ProductCategory] as Cat Inner Join [Production].[ProductSubcategory] as Sub ON Cat.ProductCategoryID = Sub.ProductCategoryID;";

                var categoryDictionary = new Dictionary<int, ProductCategory>();

                var data = db.Query<ProductCategory, ProductSubcategory, ProductCategory>(sql,
                    (category, subCategory) =>
                    {
                        ProductCategory categoryData;
                        if (!categoryDictionary.TryGetValue(category.ProductCategoryID, out categoryData))
                        {
                            categoryData = category;
                            categoryData.ProductSubcategories = new List<ProductSubcategory>();
                            categoryDictionary.Add(categoryData.ProductCategoryID, categoryData);
                        }
                        categoryData.ProductSubcategories.Add(subCategory);
                        return categoryData;
                    },
                    splitOn: "ProductSubcategoryID"
                ).Distinct().ToList();


            }



            return Ok();
        }
    }
}
