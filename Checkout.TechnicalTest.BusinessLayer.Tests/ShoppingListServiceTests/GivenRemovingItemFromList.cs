using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Checkout.TechnicalTest.DataAccessLayer.Enums;
using Checkout.TechnicalTest.DataAccessLayer.Models;
using Moq;
using NUnit.Framework;

namespace Checkout.TechnicalTest.BusinessLayer.Tests.ShoppingListServiceTests
{
    public class GivenRemovingItemFromList : BaseGiven
    {
        protected string ExistingItemName = "Existing item";

        protected override void Given()
        {
            ShoppingItem = new ShoppingItem {Id = 1, Name = ExistingItemName, ProductType = ProductType.Drink, Quantity = 10};

            ShoppingItems = new List<ShoppingItem>
            {
                ShoppingItem,
                new ShoppingItem {Id = 2, Name = "Orange juice", ProductType = ProductType.Drink, Quantity = 5},
                new ShoppingItem {Id = 3, Name = "Coca cola", ProductType = ProductType.Drink, Quantity = 17}
            }.AsQueryable();

            PrepareSut();
        }
        
        protected class WhenCorrectCallIsMade : GivenRemovingItemFromList
        {
            protected override void When()
            {
                SUT.RemoveItemFromList(ExistingItemName);
            }

            [Test]
            public void ThenGetShoppingItemConditionalIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenDeleteShoppingItemIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Delete(It.IsAny<ShoppingItem>()), Times.Once);
            }

            [Test]
            public void ThenDeleteShoppingItemWithCorrectParametersIsCalledOnce()
            {
                var expectedName = ExistingItemName;
                var expectedId = 1;
                GenericRepositoryMock.Verify(m => m.Delete(It.Is<ShoppingItem>(s=>s.Name == expectedName && s.Id== expectedId && s.ProductType == ProductType.Drink)), Times.Once);
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Once);
            }
        }

        protected class WhenCorrectCallIsMadeAndNoItemExists : GivenRemovingItemFromList
        {
            protected Exception NewException;

            protected override void When()
            {
                ShoppingItems = new List<ShoppingItem>().AsQueryable();
                PrepareSut();

                try
                {
                    SUT.RemoveItemFromList("New item");
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
            public void ThenDeleteShoppingItemIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Delete(It.IsAny<ShoppingItem>()), Times.Never);
            }

            [Test]
            public void ThenSaveChangesIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Never);
            }
        }

        protected class WhenItemDoesNotExist : GivenRemovingItemFromList
        {
            protected Exception NewException;

            protected override void When()
            {
                try
                {
                    SUT.RemoveItemFromList("New item");
                }
                catch (Exception exception)
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
            public void ThenDeleteShoppingItemIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Delete(It.IsAny<ShoppingItem>()), Times.Never);
            }

            [Test]
            public void ThenSaveChangesIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Never);
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
