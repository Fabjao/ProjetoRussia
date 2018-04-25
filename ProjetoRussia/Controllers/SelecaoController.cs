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
    public class SelecaoController : Controller
    {
        private IOptions<ConfigAPI> _appSettings;
        private HttpClient client;

        public SelecaoController(IOptions<ConfigAPI> appSettings)
        {
            _appSettings = appSettings;
            client = new HttpClient();
            client.BaseAddress = new Uri(_appSettings.Value.URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IActionResult Index()
        {
            
            string rota = string.Format("Selecao");
            HttpResponseMessage response = client.GetAsync(rota).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            List<Selecao> selecaos = JsonConvert.DeserializeObject<List<Selecao>>(json);
            return View(selecaos);
        }

        public IActionResult Editar(int selecaoId)
        {
            string rota = string.Format("Selecao/BuscaPorId?id={0}",selecaoId);
            HttpResponseMessage response = client.GetAsync(rota).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            Selecao selecao = JsonConvert.DeserializeObject<Selecao>(json);
            return View(selecao);
        }

        public IActionResult Alterar(Selecao selecao)
        {
            return RedirectToAction("Index");
        }
    }
}