using Servicios.Parcial3.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace Servicios.Parcial3.App_Start
{
    public class JwtValidationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = request.Headers.Authorization?.Parameter;

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var claimsPrincipal = handler.ValidateToken(token, JwtManager.GetValidationParameters(), out _);
                    Thread.CurrentPrincipal = claimsPrincipal;
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = claimsPrincipal;
                    }
                }
                catch
                {
                    return request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Token inválido.");
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}