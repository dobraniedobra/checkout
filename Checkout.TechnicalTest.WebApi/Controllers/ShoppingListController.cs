using System.Web.Http;
using Checkout.TechnicalTest.BusinessLayer.Interfaces;
using Checkout.TechnicalTest.DataAccessLayer.Dto;
using Checkout.TechnicalTest.WebApi.Filters;

namespace Checkout.TechnicalTest.WebApi.Controllers
{
    [RoutePrefix("api/shoppingList")]
    public class ShoppingListController : ApiController
    {

        private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpPost]
        [Route("")]
        [ValidateModel]
        [ControllerException]
        public IHttpActionResult Add(ShoppingItemDto shoppintItemDto)
        {
            _shoppingListService.AddNewItem(shoppintItemDto);

            return Ok();
        }

        [HttpDelete]
        [Route("{name}")]
        [ControllerException]
        public IHttpActionResult Delete(string name)
        {
            _shoppingListService.RemoveItemFromList(name);

            return Ok();
        }

        [HttpPut]
        [Route("{name}")]
        [ControllerException]
        public IHttpActionResult Update(string name, int quantity)
        {
            _shoppingListService.UpdateQuantity(name, quantity);

            return Ok();
        }

        [HttpGet]
        [Route("{name}")]
        [ControllerException]
        public IHttpActionResult Get(string name)
        {
            var shoppingItem = _shoppingListService.GetShoppingItem(name);

            return Ok(shoppingItem);
        }

        [HttpGet]
        [Route("")]
        [ControllerException]
        public IHttpActionResult Get()
        {
            var shoppingList = _shoppingListService.GetShoppingList();

            return Ok(shoppingList);
        }
    }
}
