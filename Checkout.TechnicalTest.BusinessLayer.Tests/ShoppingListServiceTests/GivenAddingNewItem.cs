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
    public class GivenAddingNewItem : BaseGiven
    {
        protected string ExistingItemName = "Existing item";

        protected override void Given()
        {
            ShoppingItem = new ShoppingItem { Id = 1, Name = ExistingItemName, ProductType = ProductType.Drink, Quantity = 10 };

            ShoppingItems = new List<ShoppingItem>
            {
                ShoppingItem,
                new ShoppingItem {Id = 2, Name = "Orange juice", ProductType = ProductType.Drink, Quantity = 5},
                new ShoppingItem {Id = 3, Name = "Coca cola", ProductType = ProductType.Drink, Quantity = 17}
            }.AsQueryable();

            PrepareSut();
        }
        
        protected class WhenCorrectCallIsMade : GivenAddingNewItem
        {
            protected int NewQuantity = 10;
            protected string NewItemName = "New item";

            protected override void When()
            {
                var newItem = new ShoppingItemDto { Name = NewItemName, ProductType = ProductType.Drink, Quantity = NewQuantity };
                SUT.AddNewItem(newItem);
            }

            [Test]
            public void ThenGetShoppingItemConditionalIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenAddShoppingItemIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Add(It.IsAny<ShoppingItem>()), Times.Once);
            }

            [Test]
            public void ThenAddShoppingItemWithCorrectParametersIsCalledOnce()
            {
                var expectedName = NewItemName;
                var expectedQuantity = NewQuantity;
                GenericRepositoryMock.Verify(m => m.Add(It.Is<ShoppingItem>(s => s.Name == expectedName && s.Quantity == expectedQuantity && s.ProductType == ProductType.Drink)), Times.Once);
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Once);
            }
        }

        protected class WhenCorrectCallIsMadeAndNoItemExists : GivenAddingNewItem
        {
            protected int NewQuantity = 15;
            protected string NewItemName = "Apple juice";

            protected override void When()
            {
                ShoppingItems = new List<ShoppingItem>().AsQueryable();
                PrepareSut();

                var newItem = new ShoppingItemDto { Name = NewItemName, ProductType = ProductType.Drink, Quantity = NewQuantity };
                SUT.AddNewItem(newItem);
            }

            [Test]
            public void ThenGetShoppingItemConditionalIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenAddShoppingItemIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Add(It.IsAny<ShoppingItem>()), Times.Once);
            }

            [Test]
            public void ThenAddShoppingItemWithCorrectParametersIsCalledOnce()
            {
                var expectedName = NewItemName;
                var expectedQuantity = NewQuantity;
                GenericRepositoryMock.Verify(m => m.Add(It.Is<ShoppingItem>(s => s.Name == expectedName && s.Quantity == expectedQuantity && s.ProductType == ProductType.Drink)), Times.Once);
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Once);
            }
        }

        protected class WhenItemWithThatNameAlreadyExists : GivenAddingNewItem
        {
            protected Exception NewException;

            protected override void When()
            {
                var newItem = new ShoppingItemDto { Name = ExistingItemName, ProductType = ProductType.Drink, Quantity = 3 };

                try
                {
                    SUT.AddNewItem(newItem);
                }
                catch (ArgumentException exception)
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
            public void ThenAddShoppingItemIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Add(It.IsAny<ShoppingItem>()), Times.Never);
            }

            [Test]
            public void ThenSaveChangesIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Never);
            }

            [Test]
            public void ThenExcpectedExceptionIsThrown()
            {
                var expected = "Product already exists";
                Assert.AreEqual(expected, NewException.Message);
            }
        }
    }
}
