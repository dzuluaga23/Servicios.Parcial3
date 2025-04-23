using Servicios.Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios.Parcial3.Controllers
{
    [RoutePrefix("api/Torneos")]
    public class TorneosController : ApiController
    {
        private DBExamenEntities1 db = new DBExamenEntities1();
        [HttpPost]
        [Route("Crear")]
        public IHttpActionResult CrearTorneo(Torneo torneo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Torneos.Add(torneo);
            db.SaveChanges();

            return Ok("Se registró correctamente el torneo");
        }

        [HttpGet]
        [Route("BuscarPorTipo/{tipo}")]
        public IHttpActionResult BuscarPorTipo(string tipo)
        {
            var torneos = db.Torneos.Where(t => t.TipoTorneo == tipo).ToList();
            return Ok(torneos);
        }

        [HttpGet]
        [Route("BuscarPorNombre/{nombre}")]
        public IHttpActionResult BuscarPorNombre(string nombre)
        {
            var torneos = db.Torneos.Where(t => t.NombreTorneo == nombre).ToList();
            return Ok(torneos);
        }

        [HttpGet]
        [Route("BuscarPorFecha/{fecha}")]
        public IHttpActionResult BuscarPorFecha(DateTime fecha)
        {
            var torneos = db.Torneos.Where(t => t.FechaTorneo == fecha).ToList();
            return Ok(torneos);
        }

        [HttpGet]
        [Route("BuscarPorTipoNombreFecha")]
        public IHttpActionResult BuscarPorTipoNombreFecha(string tipo, string nombre, DateTime fecha)
        {
            var torneos = db.Torneos.Where(t =>
                t.TipoTorneo == tipo &&
                t.NombreTorneo == nombre &&
                t.FechaTorneo == fecha).ToList();

            return Ok(torneos);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult ActualizarTorneo(int id, Torneo torneo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var torneoExistente = db.Torneos.Find(id);
            if (torneoExistente == null)
                return NotFound();

            torneoExistente.TipoTorneo = torneo.TipoTorneo;
            torneoExistente.NombreTorneo = torneo.NombreTorneo;
            torneoExistente.NombreEquipo = torneo.NombreEquipo;
            torneoExistente.ValorInscripcion = torneo.ValorInscripcion;
            torneoExistente.FechaTorneo = torneo.FechaTorneo;
            torneoExistente.Integrantes = torneo.Integrantes;
            torneoExistente.idAdministradorITM = torneo.idAdministradorITM;

            db.Entry(torneoExistente).State = EntityState.Modified;
            db.SaveChanges();

            return Ok("Se actualizó correctamente el torneo");
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult EliminarTorneo(int id)
        {
            var torneo = db.Torneos.Find(id);
            if (torneo == null)
                return NotFound();

            db.Torneos.Remove(torneo);
            db.SaveChanges();

            return Ok("Torneo eliminado correctamente.");
        }
    }
}