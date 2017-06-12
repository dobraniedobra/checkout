using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Checkout.TechnicalTest.BusinessLayer.Interfaces;
using Checkout.TechnicalTest.BusinessLayer.Services;
using Checkout.TechnicalTest.DataAccessLayer.Context;
using Checkout.TechnicalTest.DataAccessLayer.Interfaces;
using Checkout.TechnicalTest.DataAccessLayer.Repositories;

namespace Checkout.TechnicalTest.WebApi
{
    public class AutofacWebApiConfig
    {
        public static void ConfigureAutoFac(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();


            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DatabaseContext>();
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>));
            builder.RegisterType<ShoppingListService>().As<IShoppingListService>();

            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}