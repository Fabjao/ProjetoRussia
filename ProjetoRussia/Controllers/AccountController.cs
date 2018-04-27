using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjetoRussia.Core.Models;

namespace ProjetoRussia.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Selecao");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Usuario usuario)
        {
            if (usuario.Login == "Fabio" && usuario.Senha == "123")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usuario.Login));
                var id = new ClaimsIdentity(claims, "password");
                var principal = new ClaimsPrincipal(id);

                await HttpContext.SignInAsync("app", principal, new AuthenticationProperties() { IsPersistent = usuario.IsPesistent});

                return RedirectToAction("Index", "Selecao");
            }
            return View();
        }

        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}