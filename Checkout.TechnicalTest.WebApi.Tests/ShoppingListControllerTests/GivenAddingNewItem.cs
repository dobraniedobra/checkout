using System.Web.Http;
using System.Web.Http.Results;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.DataAccessLayer.Enums;
using Moq;
using NUnit.Framework;

namespace Checkout.TechnicalTest.WebApi.Tests.ShoppingListControllerTests
{
    public class GivenAddingNewItem : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        protected class WhenCorrectCallIsMade : GivenAddingNewItem
        {
            protected int NewQuantity = 10;
            protected string NewItemName = "New item";

            private IHttpActionResult _result;

            protected override void When()
            {
                var newItem = new ShoppingItemDto { Name = NewItemName, ProductType = ProductType.Drink, Quantity = NewQuantity };
                _result = SUT.Add(newItem);
            }

            [Test]
            public void ThenAddNewItemIsCalledOnce()
            {
                ShoppingListServiceMock.Verify(m => m.AddNewItem(It.IsAny<ShoppingItemDto>()), Times.Once);
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
