using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Models;

namespace Checkout.TechnicalTest.DataAccessLayer.Extentions
{
    public static class ShoppingItemEx
    {
        public static ShoppingItem ToEntity(this ShoppingItemDto shoppingItemDto)
        {
            return new ShoppingItem
            {
                Name = shoppingItemDto.Name,
                ProductType = shoppingItemDto.ProductType,
                Quantity = shoppingItemDto.Quantity
            };
        }

        public static ShoppingItemDto ToDto(this ShoppingItem shoppingItem)
        {
            return new ShoppingItemDto
            {
                ShoppingItemId = shoppingItem.Id,
                Name = shoppingItem.Name,
                ProductType = shoppingItem.ProductType,
                Quantity = shoppingItem.Quantity
            };
        }
    }
}
