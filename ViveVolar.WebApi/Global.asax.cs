using System.Web.Http;
using ViveVolar.WebApi.App_Start;
using ViveVolar.WebApi.Mapper;

namespace ViveVolar.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            Bootstrapper.Run();
            AutoMapperConfiguration.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}
