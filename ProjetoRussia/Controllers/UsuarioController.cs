using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoRussia.Core.Models;

namespace ProjetoRussia.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return RedirectToAction("Index");
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