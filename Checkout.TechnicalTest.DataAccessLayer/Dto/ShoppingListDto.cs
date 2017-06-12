using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checkout.TechnicalTest.DataAccessLayer.Enums;

namespace Checkout.TechnicalTest.DataAccessLayer.Dto
{
    public class ShoppingListDto
    {
        public IList<ShoppingItemDto> ShoppingItems { get; set; }
    }
}
