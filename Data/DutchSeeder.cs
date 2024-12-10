using DutchTreat.Data.Entities;
using System.Text.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    { 
   
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;

    public DutchSeeder(ApplicationDbContext context, IWebHostEnvironment hosting)
    {
        _db = context;
        _hosting = hosting;
    }

        public void Seed()
        {

            _db.Database.EnsureCreated();


            if (!_db.Products.Any())
            {

                var file = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);


                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                _db.Products.AddRange(products);


                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _db.Orders.Add(order);
                _db.SaveChanges();
            }
        }

    }
}