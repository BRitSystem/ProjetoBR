using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebLoja.Models;

namespace WebLoja.Controllers
{
    public class PreCarrinhoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Informacoes(int? id)
        {

            return RedirectToAction("Index");
        }
    }
}
