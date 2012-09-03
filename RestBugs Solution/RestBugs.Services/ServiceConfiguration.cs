using System.Web.Http;
using System.Web.Http.Controllers;
using Ninject;
using RestBugs.Services.Model;
using WebApiContrib.Formatting.Html.Formatters;
using WebApiContrib.Formatting.Razor;
using WebApiContrib.IoC.Ninject;
using WebApiContrib.ModelBinders;

namespace RestBugs.Services
{
    public static class ServiceConfiguration
    {
        public static void Configure(HttpConfiguration config) {

            config.Routes.MapHttpRoute("def", "bugs/{controller}", new {controller = "Index"});

            config.Formatters.Add(new HtmlMediaTypeViewFormatter(
                siteRootPath: "~/Templates",
                viewLocator: new RazorViewLocator(),
                viewParser: new RazorViewParser()
            ));
            //config.MessageHandlers.Add(new EtagMessageHandler());

            var kernel = new StandardKernel();
            kernel.Bind<IBugRepository>().To<StaticBugRepository>();
            kernel.Bind<IActionValueBinder>().To<MvcActionValueBinder>();

            config.DependencyResolver = new NinjectResolver(kernel);

            AutoMapperConfig.Configure();
        }    
    }
}
