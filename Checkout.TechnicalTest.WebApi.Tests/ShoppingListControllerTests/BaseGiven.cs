using System.Collections.Generic;
using System.Linq;
using Checkout.TechnicalTest.BusinessLayer.Interfaces;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.WebApi.Controllers;
using Moq;
using SpecsFor;

namespace Checkout.TechnicalTest.WebApi.Tests.ShoppingListControllerTests
{
    public class BaseGiven : SpecsFor<ShoppingListController>
    {
        protected readonly Mock<IShoppingListService> ShoppingListServiceMock = new Mock<IShoppingListService>();

        protected ShoppingItemDto ShoppingItem = new ShoppingItemDto();
        protected IList<ShoppingItemDto> ShoppingItems = new List<ShoppingItemDto>();
        protected ShoppingListDto ShoppingList = new ShoppingListDto();

        protected void PrepareSut()
        {
            ShoppingListServiceMock.Setup(m => m.AddNewItem(It.IsAny<ShoppingItemDto>()));
            ShoppingListServiceMock.Setup(m => m.GetShoppingItems()).Returns(ShoppingItems);
            ShoppingListServiceMock.Setup(m => m.GetShoppingList()).Returns(ShoppingList);
            ShoppingListServiceMock.Setup(m => m.GetShoppingItem(It.IsAny<string>())).Returns(ShoppingItem);

           SUT = new ShoppingListController(ShoppingListServiceMock.Object);
        }
    }
}
