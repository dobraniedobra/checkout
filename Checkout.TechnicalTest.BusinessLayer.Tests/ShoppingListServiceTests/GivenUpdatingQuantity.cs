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
    public class GivenUpdatingQuantity : BaseGiven
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
        
        protected class WhenCorrectCallIsMade : GivenUpdatingQuantity
        {
            protected int NewQuantity = 25;
            protected override void When()
            {
                SUT.UpdateQuantity(ExistingItemName, NewQuantity);
            }

            [Test]
            public void ThenGetShoppingItemConditionalIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenUpdateShoppingItemIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Update(It.IsAny<ShoppingItem>()), Times.Once);
            }

            [Test]
            public void ThenUpdateShoppingItemWithCorrectParametersIsCalledOnce()
            {
                var expectedName = ExistingItemName;
                var expectedQuantity = NewQuantity;
                GenericRepositoryMock.Verify(m => m.Update(It.Is<ShoppingItem>(s=>s.Name == expectedName && s.Quantity== expectedQuantity && s.ProductType == ProductType.Drink)), Times.Once);
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Once);
            }
        }

        protected class WhenCorrectCallIsMadeAndNoItemExists : GivenUpdatingQuantity
        {
            protected Exception NewException;

            protected override void When()
            {
                ShoppingItems = new List<ShoppingItem>().AsQueryable();
                PrepareSut();

                try
                {
                    SUT.UpdateQuantity("New item", 3);
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
            public void ThenUpdateShoppingItemIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Update(It.IsAny<ShoppingItem>()), Times.Never);
            }

            [Test]
            public void ThenSaveChangesIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Never);
            }

            [Test]
            public void ThenExcpectedExceptionWasThrown()
            {
                var expected = "Product does not exist";

                Assert.AreEqual(expected, NewException.Message);
            }
        }

        protected class WhenItemDoesNotExist : GivenUpdatingQuantity
        {
            protected Exception NewException;

            protected override void When()
            {
                try
                {
                    SUT.UpdateQuantity("New item", 3);
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
            public void ThenUpdateShoppingItemIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Update(It.IsAny<ShoppingItem>()), Times.Never);
            }

            [Test]
            public void ThenSaveChangesIsNeverCalled()
            {
                GenericRepositoryMock.Verify(m => m.Save(), Times.Never);
            }

            [Test]
            public void ThenExcpectedExceptionWasThrown()
            {
                var expected = "Product does not exist";

                Assert.AreEqual(expected, NewException.Message);
            }
        }
    }
}
