using System.Data.Entity;
using Checkout.TechnicalTest.DataAccessLayer.Models;

namespace Checkout.TechnicalTest.DataAccessLayer.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
    }
}
