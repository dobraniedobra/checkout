using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Enums;
using Moq;
using NUnit.Framework;

namespace Checkout.TechnicalTest.WebApi.Tests.ShoppingListControllerTests
{
    public class GivenDeleting : BaseGiven
    {
        protected string ExistingItemName = "Existing item";

        protected override void Given()
        {
            PrepareSut();
        }

        protected class WhenCorrectCallIsMade : GivenDeleting
        {
            private IHttpActionResult _result;

            protected override void When()
            {
                _result = SUT.Delete(ExistingItemName);
            }

            [Test]
            public void ThenDeleteItemIsCalledOnce()
            {
                ShoppingListServiceMock.Verify(m => m.RemoveItemFromList(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void ThenDeleteItemWithCorrectParameterIsCalledOnce()
            {
                ShoppingListServiceMock.Verify(m => m.RemoveItemFromList(It.Is<string>(s=>s== ExistingItemName)), Times.Once);
            }

            [Test]
            public void ThenResponseIsExpected()
            {
                var expected = typeof(OkResult);
                Assert.AreEqual(expected, _result.GetType());
            }
        }
    }
}
