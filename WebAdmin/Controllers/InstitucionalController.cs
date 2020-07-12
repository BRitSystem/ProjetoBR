using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Controllers
{
    public class InstitucionalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(string titulo, string descricao, string RTEConteudo)
        {
            ViewBag.Name = string.Format("Name: {0} {1}", titulo, descricao, RTEConteudo);
            return View();
        }
    }
}
