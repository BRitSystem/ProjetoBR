using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebLoja.Models;

namespace WebLoja.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Produto> lst_produto = new List<Produto>();
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 1", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.23, ProdutoId = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 2", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.25, ProdutoId = 2 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 3", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.23, ProdutoId = 3 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 4", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.25, ProdutoId = 4 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 5", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.23, ProdutoId = 5 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 6", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.25, ProdutoId = 6 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 7", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.23, ProdutoId = 7 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 8", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 15.25, ProdutoId = 8 });
            return View(lst_produto);
        }

        public IActionResult ProdutoA(int? id)
        {

            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
