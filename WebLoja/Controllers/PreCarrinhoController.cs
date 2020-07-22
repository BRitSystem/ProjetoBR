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
        private Produto Produto = new Produto();
        public IActionResult Index(int? id)
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

            Produto = lst_produto.Find(item => item.ProdutoId == id);
            return View(Produto);
        }        
    }
}
