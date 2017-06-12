using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Filters;
using NUnit.Framework;

namespace Checkout.TechnicalTest.WebApi.Tests.ControllerExceptionAttributeTests
{
    public class GivenOnException : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        protected class WhenArgumentExceptionIsThrown : GivenOnException
        {
            protected HttpActionExecutedContext ActionContext;
            protected HttpResponseException NewException;
            protected string ExceptionMessage = "this is an argument exception";
            protected override void When()
            {
                ActionContext = new HttpActionExecutedContext() {Exception = new ArgumentException(ExceptionMessage) };
                try
                {
                    SUT.OnException(ActionContext);
                }
                catch (HttpResponseException exception)
                {
                    NewException = exception;
                }
            }

            [Test]
            public void ThenExpectedStatusCodeIsReturned()
            {
                var expected = HttpStatusCode.BadRequest;
                Assert.AreEqual(expected, NewException.Response.StatusCode);
            }
        }

        protected class WhenKeyNotFoundExceptionIsThrown : GivenOnException
        {
            protected HttpActionExecutedContext ActionContext;
            protected HttpResponseException NewException;
            protected string ExceptionMessage = "this is a key null exception";
            protected override void When()
            {
                ActionContext = new HttpActionExecutedContext() { Exception = new KeyNotFoundException(ExceptionMessage) };
                try
                {
                    SUT.OnException(ActionContext);
                }
                catch (HttpResponseException exception)
                {
                    NewException = exception;
                }
            }

            [Test]
            public void ThenExpectedStatusCodeIsReturned()
            {
                var expected = HttpStatusCode.NotFound;
                Assert.AreEqual(expected, NewException.Response.StatusCode);
            }
        }
    }
}
