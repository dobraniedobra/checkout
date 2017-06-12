using Checkout.TechnicalTest.WebApi.Filters;
using SpecsFor;

namespace Checkout.TechnicalTest.WebApi.Tests.ValidateModelAttributeTests
{
    public class BaseGiven : SpecsFor<ValidateModelAttribute>
    {
        protected void PrepareSut()
        {
            SUT = new ValidateModelAttribute();
        }
    }
}
