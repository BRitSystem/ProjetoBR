using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebLoja.Libraries.Cookie;
using WebLoja.Models;

namespace WebLoja.Controllers
{
    public class CarrinhoController : Controller
    {
        private const string key = "CarrinhoDeCompras";
        private List<Produto> lstProduto = new List<Produto>();

        public IActionResult Index(int? id)
        {
            List<Item> lista = new List<Item>();

            if (Request.Cookies.Count == 0)
            { 
                var option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append(key, id.ToString(), option);
            }
            
            
            List<Produto> lst_produto = new List<Produto>();
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 1", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)5.00, ProdutoId = 1, ProdutoQuantidade = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 2", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/199926-1000-1000/tinta-brilhante-resistente-ao-tempo-100ml.jpg?v=636410808359430000", ProdutoValor = (decimal)27.92, ProdutoId = 2, ProdutoQuantidade = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 3", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/193768-1000-1000/cores-claras-compressed.jpg?v=636258820500630000", ProdutoValor = (decimal)33.23, ProdutoId = 3, ProdutoQuantidade = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 4", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)4.25, ProdutoId = 4, ProdutoQuantidade = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 5", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)5.77, ProdutoId = 5, ProdutoQuantidade = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 6", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)15.00, ProdutoId = 6, ProdutoQuantidade = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 7", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)99.23, ProdutoId = 7, ProdutoQuantidade = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 8", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)100.25, ProdutoId = 8, ProdutoQuantidade = 1 });
            lstProduto.Add(lst_produto.Find(item => item.ProdutoId == id));
            
            
            return View(lstProduto);
        }

        public class Item
        {
            public long Id { get; set; }
            public int? Quantidade { get; set; }
        }
    }
}
