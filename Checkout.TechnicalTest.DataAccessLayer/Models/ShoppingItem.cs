using System.ComponentModel.DataAnnotations;
using Checkout.TechnicalTest.DataAccessLayer.Enums;

namespace Checkout.TechnicalTest.DataAccessLayer.Models
{
    public class ShoppingItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }
    }
}
