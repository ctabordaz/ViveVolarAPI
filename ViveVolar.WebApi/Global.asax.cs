using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
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
