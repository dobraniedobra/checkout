using Checkout.TechnicalTest.WebApi.Filters;
using SpecsFor;

namespace Checkout.TechnicalTest.WebApi.Tests.ControllerExceptionAttributeTests
{
    public class BaseGiven : SpecsFor<ControllerExceptionAttribute>
    {
        protected void PrepareSut()
        {
            SUT = new ControllerExceptionAttribute();
        }
    }
}
