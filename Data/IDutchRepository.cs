using DutchTreat.Data.Entities;
using DutchTreat.Models;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();

        void AddContact(ContactModel contact);
    }
}