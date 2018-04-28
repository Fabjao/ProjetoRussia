using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoRussia.Core.Models;

namespace ProjetoRussia.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private CopaContext _copaContext;

        public UsuarioController(CopaContext copaContext)
        {
            _copaContext = copaContext;
        }

        [HttpPost]
        [Route("Inserir")]
        public IActionResult Inserir([FromBody] Usuario usuario)
        {
            try
            {
                _copaContext.Usuarios.Add(usuario);
                _copaContext.SaveChanges();
                return Ok("Usuario inserido com sucesso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("ValidaLogin")]
        public IActionResult ValidaLogin([FromBody] Usuario usuario)
        {
            var login = _copaContext.Usuarios.Where(us => us.Login == usuario.Login && us.Senha == usuario.Senha).FirstOrDefault();
            if (login != null)
                return Ok(true);

            return NotFound();
        }

    }
}