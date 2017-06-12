using System.ComponentModel.DataAnnotations;
using Checkout.TechnicalTest.DataAccessLayer.Enums;

namespace Checkout.TechnicalTest.DataAccessLayer.Dto
{
    public class ShoppingItemDto
    {
        public int ShoppingItemId { get; set; }
        [Required]
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
