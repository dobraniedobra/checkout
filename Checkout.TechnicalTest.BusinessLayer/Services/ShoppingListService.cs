using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.TechnicalTest.BusinessLayer.Interfaces;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Extentions;
using Checkout.TechnicalTest.DataAccessLayer.Interfaces;
using Checkout.TechnicalTest.DataAccessLayer.Models;


namespace Checkout.TechnicalTest.BusinessLayer.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IGenericRepository<ShoppingItem> _shoppingItemRepository;
        public ShoppingListService(IGenericRepository<ShoppingItem> shoppingItemRepository)
        {
            _shoppingItemRepository = shoppingItemRepository;

        }

        public void AddNewItem(ShoppingItemDto shoppingItem)
        {
            var existingItem = GetItemByName(shoppingItem.Name);

            if (existingItem != null) throw new ArgumentException("Product already exists");

            var newShoppingItem = shoppingItem.ToEntity();

            _shoppingItemRepository.Add(newShoppingItem);

            _shoppingItemRepository.Save();
        }

        public void RemoveItemFromList(string itemName)
        {
            var existingItem = GetItemByName(itemName);

            if (existingItem == null) throw new KeyNotFoundException("Product does not exist");

            _shoppingItemRepository.Delete(existingItem);

            _shoppingItemRepository.Save();
        }

        public void UpdateQuantity(string itemName, int quantity)
        {
            var existingItem = GetItemByName(itemName);

            if (existingItem == null) throw new KeyNotFoundException("Product does not exist");

            existingItem.Quantity = quantity;

            _shoppingItemRepository.Update(existingItem);

            _shoppingItemRepository.Save();
        }

        public ShoppingItemDto GetShoppingItem(string itemName)
        {
            var existingItem = GetItemByName(itemName);

            if (existingItem == null) throw new KeyNotFoundException("Product does not exist");

            return existingItem.ToDto();
        }

        public ShoppingListDto GetShoppingList()
        {
            return new ShoppingListDto {ShoppingItems = GetShoppingItems()};
        }

        public IList<ShoppingItemDto> GetShoppingItems()
        {
            var shoppingItems = _shoppingItemRepository.Get();

            if (shoppingItems == null || !shoppingItems.Any()) throw new KeyNotFoundException("Shopping list is empty");

            var shoppingList = new List<ShoppingItemDto>();

            foreach (var shoppingItem in shoppingItems)
            {
                shoppingList.Add(shoppingItem.ToDto());
            }

            return shoppingList;
        }

        private ShoppingItem GetItemByName(string itemName)
        {
            return _shoppingItemRepository.GetConditional(s => s.Name == itemName).FirstOrDefault();
        }
    }
}
