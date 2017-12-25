using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using ViveVolar.Repositories.BookingRepository;
using ViveVolar.Services.BookingService;
using ViveVolar.Services.UserService;

namespace ViveVolar.WebApi.App_Start
{
    public static class Bootstrapper
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
           
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Repositories
            builder.RegisterAssemblyTypes(typeof(BookingRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            //Services
            builder.RegisterAssemblyTypes(typeof(BookingService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();
            
            Container = builder.Build();

            return Container;
        }

        public static void Run()
        {
            //Configure AutoFac  
            Initialize(GlobalConfiguration.Configuration);
        }

    }
}