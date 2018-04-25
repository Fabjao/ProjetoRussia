using System;
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
                _copaContext.Selecoes.Add(new Selecao() {
                    Nome = selecao.Nome
                });
                _copaContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Selecao/Edit/5
        [HttpGet("BuscaPorId")]
        public IActionResult BuscaPorId(int id)
        {
            Selecao selecao = _copaContext.Selecoes.Where(s => s.SelecaoId == id).FirstOrDefault();

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

        // GET: Selecao/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Selecao/Delete/5
        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}