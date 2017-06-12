using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Checkout.TechnicalTest.WebApi.Filters
{
    public class ControllerExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage();

            if (context.Exception is ArgumentException)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "BadRequest"
                };
            }

            if (context.Exception is KeyNotFoundException)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "ItemNotFound"
                };
            }

            throw new HttpResponseException(response);
        }
    }
}