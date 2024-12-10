using DutchTreat.Data.Entities;
using DutchTreat.Models;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(ApplicationDbContext db, ILogger<DutchRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called...");

                return _db.Products
                .OrderBy(p => p.Artist)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _db.Products
            .Where(p => p.Category == category)
            .ToList();
        }

        public bool SaveAll()
        {
            return _db.SaveChanges() > 0;
        }

        public void AddContact(ContactModel contact)
        {
            _db.Contacts.Add(contact);
        }
    }
}


