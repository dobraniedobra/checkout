using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Enums;
using Checkout.TechnicalTest.DataAccessLayer.Models;
using Moq;
using NUnit.Framework;

namespace Checkout.TechnicalTest.BusinessLayer.Tests.ShoppingListServiceTests
{
    public class GivenGettingShoppingList : BaseGiven
    {
        protected string ExistingItsemName = "Existing item";

        protected override void Given()
        {
            ShoppingItem = new ShoppingItem {Id = 1, Name = ExistingItsemName, ProductType = ProductType.Drink, Quantity = 10};

            ShoppingItems = new List<ShoppingItem>
            {
                ShoppingItem,
                new ShoppingItem {Id = 2, Name = "Orange juice", ProductType = ProductType.Drink, Quantity = 5},
                new ShoppingItem {Id = 3, Name = "Coca cola", ProductType = ProductType.Drink, Quantity = 17},
                new ShoppingItem {Id = 4, Name = "Water", ProductType = ProductType.Drink, Quantity = 11}
            }.AsQueryable();

            PrepareSut();
        }

        protected class WhenCorrectCallIsMadeAndThereAreItemsOnList : GivenGettingShoppingList
        {
            protected IList<ShoppingItemDto> ShoppingList; 
            protected override void When()
            {
                ShoppingList = SUT.GetShoppingList();
            }

            [Test]
            public void ThenGetShoppingItemslIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Get(), Times.Once);
            }

            [Test]
            public void ThenShoppingListIsNotNull()
            {
                Assert.IsNotNull(ShoppingList);
            }

            [Test]
            public void ThenShoppingListIsNotEmpty()
            {
                Assert.IsTrue(ShoppingList.Any());
            }

            [Test]
            public void ThenShoppingListHasExpectenNumberOfItems()
            {
                const int expected = 4;
                Assert.AreEqual(expected, ShoppingList.Count);
            }
        }

        protected class WhenCorrectCallIsMadeAndNoItemExists : GivenGettingShoppingList
        {
            protected IList<ShoppingItemDto> ShoppingList;
            protected Exception NewException;
            protected override void When()
            {
                ShoppingItems = new List<ShoppingItem>().AsQueryable();
                PrepareSut();

                try
                {
                    ShoppingList = SUT.GetShoppingList();
                }
                catch (KeyNotFoundException exception)
                {
                    NewException = exception;
                }
            }

            [Test]
            public void ThenGetShoppingItemslIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Get(), Times.Once);
            }

            [Test]
            public void ThenShoppingListIsNull()
            {
                Assert.IsNull(ShoppingList);
            }

            [Test]
            public void ThenExcpectedExceptionIsThrown()
            {
                var expected = "Shopping list is empty";

                Assert.AreEqual(expected, NewException.Message);
            }
        }
    }
}
