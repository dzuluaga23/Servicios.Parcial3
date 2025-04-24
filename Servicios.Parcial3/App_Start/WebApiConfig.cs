using Servicios.Parcial3.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Servicios.Parcial3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // VALIDACIÓN JWT
            config.MessageHandlers.Add(new JwtValidationHandler());

            // Rutas por defecto
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );    
        }
    }
}
