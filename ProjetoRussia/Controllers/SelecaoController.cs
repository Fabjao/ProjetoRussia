using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjetoRussia.Config;
using ProjetoRussia.Core.Models;
using ProjetoRussia.ViewModel;

namespace ProjetoRussia.Controllers
{
    [Authorize]
    public class SelecaoController : Controller
    {
        private IOptions<ConfigAPI> _appSettings;
        private HttpClient _client;

        public SelecaoController(IOptions<ConfigAPI> appSettings)
        {
            _appSettings = appSettings;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_appSettings.Value.URL);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IActionResult Index()
        {

            string rota = string.Format("Selecao/Lista");
            HttpResponseMessage response = _client.GetAsync(rota).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            List<Selecao> selecaos = JsonConvert.DeserializeObject<List<Selecao>>(json);
            return View(selecaos);
        }

        public IActionResult Editar(int selecaoId)
        {
            Selecao selecao = RetornaSelecao(selecaoId);
            return View(selecao);
        }


        public async Task<IActionResult> Alterar(Selecao selecao)
        {
            string rota = string.Format("Selecao/Alterar");
            var response = await _client.PostAsJsonAsync(rota, selecao);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int selecaoId)
        {
            string rota = string.Format("Selecao/Deletar?id={0}",selecaoId);
            var response = await _client.GetAsync(rota);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExcluirJogador(int jogadorID, int selecaoID)
        {
            
            string rota = string.Format("Selecao/ExcluirJogador?id={0}", jogadorID);
            var response = await _client.GetAsync(rota);
            Selecao selecao = RetornaSelecao(selecaoID);
            ViewModelSelecao viewModelSelecao = new ViewModelSelecao();
            viewModelSelecao.selecao = selecao;
            viewModelSelecao.jogadores = selecao.Jogadores;
            return View("CadastrarJogador", viewModelSelecao);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(Selecao selecao)
        {
            string rota = string.Format("Selecao/Inserir");
            var response = await _client.PostAsJsonAsync(rota, selecao);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int selecaoId)
        {
            Selecao selecao = RetornaSelecao(selecaoId);
            return View(selecao);
        }

        [HttpGet]
        public IActionResult CadastrarJogador(int selecaoId)
        {
            Selecao selecao = RetornaSelecao(selecaoId);
            ViewModelSelecao viewModelSelecao = new ViewModelSelecao();
            viewModelSelecao.selecao = selecao;
            viewModelSelecao.jogadores = selecao.Jogadores;
            return View(viewModelSelecao);
        }

        public async Task<IActionResult> CadastrarJogador(ViewModelSelecao viewModelSelecao)
        {
            string rota = string.Format("Selecao/InserirJogador");            
            viewModelSelecao.jogador.SelecaoId = viewModelSelecao.selecao.SelecaoId;
            var response = await _client.PostAsJsonAsync(rota, viewModelSelecao.jogador);
            Selecao selecao = RetornaSelecao(viewModelSelecao.selecao.SelecaoId);
            viewModelSelecao = new ViewModelSelecao();
            viewModelSelecao.selecao = selecao;
            viewModelSelecao.jogadores = selecao.Jogadores;
            return View(viewModelSelecao);
        }

        private Selecao RetornaSelecao(int selecaoId)
        {
            string rota = string.Format("Selecao/BuscaPorId?id={0}", selecaoId);
            HttpResponseMessage response = _client.GetAsync(rota).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            Selecao selecao = JsonConvert.DeserializeObject<Selecao>(json);
            selecao.Jogadores = selecao.Jogadores ?? new List<Jogador>();
            return selecao;
        }
    }
}