﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoRussia.Core.Models;

namespace ProjetoRussia.Api.Controllers
{
    [Route("api/[controller]")]
    public class SelecaoController : Controller
    {
        private CopaContext _copaContext;

        public SelecaoController(CopaContext copaContext)
        {
            _copaContext = copaContext;
        }
        // GET: Selecao
        [HttpGet]
        [Route("Lista")]
        public IActionResult Index()
        {

            return Ok(_copaContext.Selecoes.ToList());
        }

        // GET: Selecao/Details/5        
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // POST: Selecao/Create
        [HttpPost]
        [Route("Inserir")]
        public IActionResult Create([FromBody]Selecao selecao)
        {
            try
            {
                // TODO: Add insert logic here
                _copaContext.Selecoes.Add(new Selecao()
                {
                    Nome = selecao.Nome
                });
                _copaContext.SaveChanges();

                return Ok();
            }
            catch
            {
                return NotFound("Falha ao gravar a seleção.");
            }
        }

        [HttpPost]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody]Selecao selecao)
        {
            try
            {
                _copaContext.Selecoes.Update(selecao);
                _copaContext.SaveChanges();

                return Ok();
            }
            catch
            {
                return NotFound("Falha ao gravar a seleção.");
            }
        }

        // GET: Selecao/Edit/5
        [HttpGet("BuscaPorId")]
        public IActionResult BuscaPorId(int id)
        {
            Selecao selecao = _copaContext.Selecoes.Where(s => s.SelecaoId == id).FirstOrDefault();
            selecao.Jogadores = _copaContext.Jogadores.Where(x => x.SelecaoId == id).ToList();

            return Ok(selecao);
        }

        [HttpGet("ExcluirJogador")]
        public IActionResult ExcluirJogador(int id)
        {
            Jogador jogador = _copaContext.Jogadores.Where(s => s.JogadorId == id).FirstOrDefault();
            _copaContext.Jogadores.Remove(jogador);
            _copaContext.SaveChanges();
            return Ok();
        }

        [HttpGet("Deletar")]
        public IActionResult Deletar(int id)
        {
            var jogadores = _copaContext.Jogadores.Where(x => x.SelecaoId == id);
            _copaContext.Jogadores.RemoveRange(jogadores);

            var selecao = _copaContext.Selecoes.Where(x => x.SelecaoId == id).FirstOrDefault();
            _copaContext.Selecoes.Remove(selecao);
            _copaContext.SaveChanges();

            return Ok(selecao);
        }



        // POST: Selecao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        [Route("InserirJogador")]
        public IActionResult InserirJogador([FromBody]Jogador jogador)
        {
            try
            {
                // TODO: Add insert logic here
                _copaContext.Jogadores.Add(jogador);
                _copaContext.SaveChanges();

                return Ok();
            }
            catch
            {
                return NotFound("Falha ao gravar o jogador.");
            }
        }
    }
}