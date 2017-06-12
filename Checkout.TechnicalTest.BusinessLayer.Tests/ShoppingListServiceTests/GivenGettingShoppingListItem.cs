using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Enums;
using Checkout.TechnicalTest.DataAccessLayer.Models;
using Moq;
using NUnit.Framework;

namespace Checkout.TechnicalTest.BusinessLayer.Tests.ShoppingListServiceTests
{
    public class GivenGettingShoppingListItem : BaseGiven
    {
        protected string ExistingItemName = "Existing item";
        protected int ExistingItemQuantity = 10;

        protected override void Given()
        {
            ShoppingItem = new ShoppingItem {Id = 1, Name = ExistingItemName, ProductType = ProductType.Drink, Quantity = ExistingItemQuantity };

            ShoppingItems = new List<ShoppingItem>
            {
                ShoppingItem,
                new ShoppingItem {Id = 2, Name = "Orange juice", ProductType = ProductType.Drink, Quantity = 5},
                new ShoppingItem {Id = 3, Name = "Coca cola", ProductType = ProductType.Drink, Quantity = 17},
                new ShoppingItem {Id = 4, Name = "Water", ProductType = ProductType.Drink, Quantity = 11}
            }.AsQueryable();

            PrepareSut();
        }

        protected class WhenCorrectCallIsMadeAndThereAreItemsOnList : GivenGettingShoppingListItem
        {
            protected ShoppingItemDto ResultShoppingItem; 
            protected override void When()
            {
                ResultShoppingItem = SUT.GetShoppingItem(ExistingItemName);
            }

            [Test]
            public void ThenGetShoppingItemConditionalIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenShoppingItemIsNotNull()
            {
                Assert.IsNotNull(ResultShoppingItem);
            }

            [Test]
            public void ThenExpectedItemIsReturned()
            {
                var expectedName = ExistingItemName;
                var expectedQuantity = ExistingItemQuantity;

                Assert.AreEqual(expectedName, ResultShoppingItem.Name);
                Assert.AreEqual(expectedQuantity, ResultShoppingItem.Quantity);
                Assert.AreEqual(ProductType.Drink, ResultShoppingItem.ProductType);
            }
        }

        protected class WhenCorrectCallIsMadeAndNoItemExists : GivenGettingShoppingListItem
        {
            protected ShoppingItemDto ResultShoppingItem;
            protected Exception NewException;
            protected override void When()
            {
                ShoppingItems = new List<ShoppingItem>().AsQueryable();
                PrepareSut();

                try
                {
                    ResultShoppingItem = SUT.GetShoppingItem("New item");
                }
                catch (KeyNotFoundException exception)
                {
                    NewException = exception;
                }
            }

            [Test]
            public void ThenGetShoppingItemConditionalIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenShoppingListIsNull()
            {
                Assert.IsNull(ResultShoppingItem);
            }

            [Test]
            public void ThenExcpectedExceptionWIsThrown()
            {
                var expected = "Product does not exist";

                Assert.AreEqual(expected, NewException.Message);
            }
        }

        protected class WhenCorrectCallIsMadeAndItemDoesNotExist : GivenGettingShoppingListItem
        {
            protected ShoppingItemDto ResultShoppingItem;
            protected Exception NewException;
            protected override void When()
            {
                try
                {
                    ResultShoppingItem = SUT.GetShoppingItem("New item");
                }
                catch (KeyNotFoundException exception)
                {
                    NewException = exception;
                }
            }

            [Test]
            public void ThenGetShoppingItemConditionalIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenShoppingListIsNull()
            {
                Assert.IsNull(ResultShoppingItem);
            }

            [Test]
            public void ThenExcpectedExceptionIsThrown()
            {
                var expected = "Product does not exist";

                Assert.AreEqual(expected, NewException.Message);
            }
        }
    }
}
