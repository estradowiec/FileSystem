#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="ATSI S.A.">
//     Copyright (c) ATSI S.A. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace Test.FlashWebsiteWrapper
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Test.FlashWebsiteWrapper.App_Start;

    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}