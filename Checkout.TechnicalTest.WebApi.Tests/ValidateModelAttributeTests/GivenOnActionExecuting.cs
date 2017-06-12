using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using NUnit.Framework;

namespace Checkout.TechnicalTest.WebApi.Tests.ValidateModelAttributeTests
{
    public class GivenOnActionExecuting : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        protected class WhenModelStateIsValid : GivenOnActionExecuting
        {
            protected HttpActionContext ActionContext;
            protected override void When()
            {
                ActionContext = new HttpActionContext() { ControllerContext = new HttpControllerContext { Request = new HttpRequestMessage() } };
                ActionContext.ModelState.Clear();
                
                SUT.OnActionExecuting(ActionContext);
            }

            [Test]
            public void ThenThereIsNoResponse()
            {
                Assert.IsNull(ActionContext.Response);
            }
        }

        protected class WhenModelStateIsNotValid : GivenOnActionExecuting
        {
            protected HttpActionContext ActionContext;
            protected override void When()
            {
                ActionContext =  new HttpActionContext() {ControllerContext = new HttpControllerContext {Request = new HttpRequestMessage()} };
                ActionContext.ModelState.AddModelError("key", "error");
                
                
                SUT.OnActionExecuting(ActionContext);
            }

            [Test]
            public void ThenExpectedStatusCodeIsReturned()
            {
                var expected = HttpStatusCode.BadRequest;
                Assert.AreEqual(expected, ActionContext.Response.StatusCode);
            }
        }
    }
}
