using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Enums;
using Moq;
using NUnit.Framework;

namespace Checkout.TechnicalTest.WebApi.Tests.ShoppingListControllerTests
{
    public class GivenGetting : BaseGiven
    {
        protected string ExistingItemName = "Existing item";

        protected override void Given()
        {
           PrepareSut();
        }

        protected class WhenCorrectCallIsMadeWithNullParameter : GivenGetting
        {
            private IHttpActionResult _result;

            protected override void When()
            {
                _result = SUT.Get();
            }

            [Test]
            public void ThenGetShoppingListIsCalledOnce()
            {
                ShoppingListServiceMock.Verify(m => m.GetShoppingList(), Times.Once);
            }


            [Test]
            public void ThenGetShoppingItemIsNeverCalled()
            {
                ShoppingListServiceMock.Verify(m => m.GetShoppingItem(It.IsAny<string>()), Times.Never);
            }

            [Test]
            public void ThenResponseIsExpected()
            {
                var expected = typeof(OkNegotiatedContentResult<ShoppingListDto>);
                Assert.AreEqual(expected, _result.GetType());
            }
        }

        protected class WhenCorrectCallIsMadeWithNameParameter : GivenGetting
        {
            private IHttpActionResult _result;

            protected override void When()
            {
                _result = SUT.Get(ExistingItemName);
            }

            [Test]
            public void ThenGetShoppingListIsNeverCalled()
            {
                ShoppingListServiceMock.Verify(m => m.GetShoppingList(), Times.Never);
            }

            [Test]
            public void ThenGetShoppingItemIsCalledOnce()
            {
                ShoppingListServiceMock.Verify(m => m.GetShoppingItem(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void ThenGetShoppingItemIsCalledOnceWithCorrectParameter()
            {
                ShoppingListServiceMock.Verify(m => m.GetShoppingItem(It.Is<string>(s=>s==ExistingItemName)), Times.Once);
            }

            [Test]
            public void ThenResponseIsExpected()
            {
                var expected = typeof(OkNegotiatedContentResult<ShoppingItemDto>);
                Assert.AreEqual(expected, _result.GetType());
            }
        }
    }
}
