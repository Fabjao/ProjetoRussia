using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjetoRussia.Config;
using ProjetoRussia.Core.Models;

namespace ProjetoRussia.Controllers
{
    public class AccountController : Controller
    {
        
        private HttpClient _client;

        public AccountController(IOptions<ConfigAPI> options)
        {        
            _client = new HttpClient();
            _client.BaseAddress = new Uri(options.Value.URL);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

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
            string rota = string.Format("Usuario/ValidaLogin");
            var response = await _client.PostAsJsonAsync(rota, usuario);

            if (response.IsSuccessStatusCode)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usuario.Login));
                var id = new ClaimsIdentity(claims, "password");
                var principal = new ClaimsPrincipal(id);

                await HttpContext.SignInAsync("appRussia", principal, new AuthenticationProperties() { IsPersistent = usuario.IsPesistent});

                return RedirectToAction("Index", "Selecao");
            }
            else
            {
                ViewData["Login"] = "falhou";
                return View();
            }
            
        }

        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}