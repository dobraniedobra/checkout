using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Enums;
using Moq;
using NUnit.Framework;

namespace Checkout.TechnicalTest.WebApi.Tests.ShoppingListControllerTests
{
    public class GivenUpdating : BaseGiven
    {
        protected string ExistingItemName = "Existing item";

        protected override void Given()
        {
            PrepareSut();
        }

        protected class WhenCorrectCallIsMade : GivenUpdating
        {
            private IHttpActionResult _result;

            protected int newQuantity = 12;

            protected override void When()
            {
                _result = SUT.Update(ExistingItemName, newQuantity);
            }

            [Test]
            public void ThenUpdateItemIsCalledOnce()
            {
                ShoppingListServiceMock.Verify(m => m.UpdateQuantity(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            }

            [Test]
            public void ThenDeleteItemWithCorrectParameterIsCalledOnce()
            {
                ShoppingListServiceMock.Verify(m => m.UpdateQuantity(It.Is<string>(s=>s== ExistingItemName), It.Is<int>(i=>i==newQuantity)), Times.Once);
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
