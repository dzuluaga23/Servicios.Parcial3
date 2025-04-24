using Servicios.Parcial3.Helpers;
using Servicios.Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios.Parcial3.Controllers
{
    public class LoginController : ApiController
    {
        private DBExamenEntities1 db = new DBExamenEntities1();

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login(LoginRequest login)
        {
            using (var db = new DBExamenEntities1())
            {
                var admin = db.AdministradorITMs
                              .FirstOrDefault(a => a.Usuario == login.Usuario && a.Clave == login.Clave);

                try
                {
                    var token = JwtManager.GenerateToken(admin.Usuario);
                    return Ok(new { Token = token });
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

    }
}