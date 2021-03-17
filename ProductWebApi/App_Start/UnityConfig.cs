using ProductWebApi.Authentication;
using ProductWebApi.Models;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ProductWebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IProductService, ProductService>();

            var service = container.Resolve<ProductService>();

            var helper = new UserHelper(service);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}