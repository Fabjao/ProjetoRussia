using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjetoRussia.Config;
using ProjetoRussia.Core.Models;

namespace ProjetoRussia.Controllers
{
    public class UsuarioController : Controller
    {
        private IOptions<ConfigAPI> _options;
        private HttpClient _client;

        public UsuarioController(IOptions<ConfigAPI> options)
        {
            _options = options;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_options.Value.URL);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Usuario usuario)
        {
            string rota = string.Format("Inserir");
            var response = _client.PostAsJsonAsync(rota, usuario);

            return RedirectToAction("Index", "Account");
        }


        public IActionResult Validar(string login, string senha)
        {
            if (login.Equals("1") & senha.Equals("1"))
            {
                return RedirectToAction("Index", "Selecao");
            }
            return RedirectToAction("Index");
        }
    }
}