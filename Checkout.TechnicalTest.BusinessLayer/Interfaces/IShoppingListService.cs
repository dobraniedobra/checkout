using System.Collections.Generic;
using Checkout.TechnicalTest.DataAccessLayer.Dto;

namespace Checkout.TechnicalTest.BusinessLayer.Interfaces
{
    public interface IShoppingListService
    {
        void AddNewItem(ShoppingItemDto shoppingItem);
        void RemoveItemFromList(string itemName);
        void UpdateQuantity(string itemName, int quantity);
        ShoppingItemDto GetShoppingItem(string itemName);
        ShoppingListDto GetShoppingList();
        IList<ShoppingItemDto> GetShoppingItems();
    }
}
