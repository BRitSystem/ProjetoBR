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
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 1", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = 5.00, ProdutoId = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 2", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/199926-1000-1000/tinta-brilhante-resistente-ao-tempo-100ml.jpg?v=636410808359430000", ProdutoValor = 27.92, ProdutoId = 2 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 3", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/193768-1000-1000/cores-claras-compressed.jpg?v=636258820500630000", ProdutoValor = 33.23, ProdutoId = 3 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 4", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = 4.25, ProdutoId = 4 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 5", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = 5.77, ProdutoId = 5 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 6", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = 15.00, ProdutoId = 6 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 7", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = 99.23, ProdutoId = 7 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 8", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = 100.25, ProdutoId = 8 });
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
