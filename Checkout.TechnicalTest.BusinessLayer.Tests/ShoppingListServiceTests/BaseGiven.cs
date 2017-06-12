using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Checkout.TechnicalTest.BusinessLayer.Services;
using Checkout.TechnicalTest.DataAccessLayer.Interfaces;
using Checkout.TechnicalTest.DataAccessLayer.Models;
using Moq;
using SpecsFor;

namespace Checkout.TechnicalTest.BusinessLayer.Tests.ShoppingListServiceTests
{
    public class BaseGiven : SpecsFor<ShoppingListService>
    {
        protected readonly Mock<IGenericRepository<ShoppingItem>> GenericRepositoryMock = new Mock<IGenericRepository<ShoppingItem>>();

        protected ShoppingItem ShoppingItem = new ShoppingItem();
        protected IQueryable<ShoppingItem> ShoppingItems = new List<ShoppingItem>().AsQueryable();

        protected void PrepareSut()
        {
            GenericRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(ShoppingItem);
            GenericRepositoryMock.Setup(m => m.Get()).Returns(ShoppingItems);
            GenericRepositoryMock.Setup(m => m.Add(It.IsAny<ShoppingItem>()));

            GenericRepositoryMock.Setup(m => m.GetConditionalWithIncludes(It.IsAny<Expression<Func<ShoppingItem, bool>>>(),It.IsAny<Expression<Func<ShoppingItem, object>>[]>()))
                .Returns(ShoppingItems);
            GenericRepositoryMock.Setup(m => m.GetWithIncludes(It.IsAny<Expression<Func<ShoppingItem, object>>[]>())).Returns(ShoppingItems);

            GenericRepositoryMock.Setup(m => m.GetConditional(It.IsAny<Expression<Func<ShoppingItem, bool>>>()))
                .Returns<Expression<Func<ShoppingItem, bool>>>(predicate => ShoppingItems.Where(predicate));

            SUT = new ShoppingListService(GenericRepositoryMock.Object);
        }
    }
}
