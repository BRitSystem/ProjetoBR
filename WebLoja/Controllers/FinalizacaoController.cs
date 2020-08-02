using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebLoja.Models;

namespace WebLoja.Controllers
{
    public class FinalizacaoController : Controller
    {
        public IActionResult Index(Boleto boleto)
        {
            ViewBag.BoletoUrl = boleto.BoletoUrl;
            return View();
        }
    }
}
