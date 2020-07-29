using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebLoja.Libraries.Cookie;
using WebLoja.Libraries.Security;
using WebLoja.Models;

namespace WebLoja.Controllers
{
    public class CarrinhoController : Controller
    {
        private const string key = "CarrinhoDeCompras";
        private List<Produto> lstProduto = new List<Produto>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private Cookie cookie;

        public CarrinhoController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }


        public IActionResult Index(int? id)
        {
            cookie = new Cookie(_httpContextAccessor, _configuration);

            #region Simula API
            List<Produto> lst_produtoAPI = new List<Produto>();
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 1", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)5.00, ProdutoId = 1, ProdutoQuantidade = 1 });
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 2", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/199926-1000-1000/tinta-brilhante-resistente-ao-tempo-100ml.jpg?v=636410808359430000", ProdutoValor = (decimal)27.92, ProdutoId = 2, ProdutoQuantidade = 1 });
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 3", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/193768-1000-1000/cores-claras-compressed.jpg?v=636258820500630000", ProdutoValor = (decimal)33.23, ProdutoId = 3, ProdutoQuantidade = 1 });
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 4", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)4.25, ProdutoId = 4, ProdutoQuantidade = 1 });
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 5", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)5.77, ProdutoId = 5, ProdutoQuantidade = 1 });
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 6", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)15.00, ProdutoId = 6, ProdutoQuantidade = 1 });
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 7", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)99.23, ProdutoId = 7, ProdutoQuantidade = 1 });
            lst_produtoAPI.Add(new Produto() { ProdutoNome = "Produto 8", ProdutoFoto = "https://palaciodaarte.vteximg.com.br/arquivos/ids/188842-1000-1000/tinta-100ml-1.jpg?v=636104965099000000", ProdutoValor = (decimal)100.25, ProdutoId = 8, ProdutoQuantidade = 1 });
            #endregion

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

            //Aqui adiciona o que vem pelo adicionar carrinho
            List<ProdutoCarrinho> lstProdutoCarrinho = new List<ProdutoCarrinho>();
            List<ProdutoCarrinho> lstProdutoCarrinho2 = new List<ProdutoCarrinho>();
            lstProdutoCarrinho.Add(new ProdutoCarrinho() { Id = lstProduto[0].ProdutoId, Quantidade = lstProduto[0].ProdutoQuantidade });

            lstProdutoCarrinho2 = cookie.Consultar(key);
            lstProdutoCarrinho2.Add(new ProdutoCarrinho() { Id = lstProduto[0].ProdutoId, Quantidade = lstProduto[0].ProdutoQuantidade });

            cookie.Salvar(key, lstProdutoCarrinho2);

            if (cookie.Existe(key))
            {
                lstProdutoCarrinho = new List<ProdutoCarrinho>();
                lstProdutoCarrinho = cookie.Consultar(key);
                if (lstProdutoCarrinho.Count > 0)
                {                   
                    foreach (var item in lstProdutoCarrinho)
                    {
                        Produto produto = (lst_produtoAPI.Find(i => i.ProdutoId == item.Id));
                        produto.ProdutoQuantidade = item.Quantidade;
                        lstProduto.Add(produto);
                    }
                }
            }
            else
            {
                lstProdutoCarrinho = new List<ProdutoCarrinho>();
                lstProdutoCarrinho.Add(new ProdutoCarrinho()
                {
                    Id = id ?? default(int),
                    Quantidade = 1
                });

                cookie.Salvar(key, lstProdutoCarrinho);
            }
                        
            return View(lstProduto);
        }

        public IActionResult AlterarQuantidade(int id, int quantidade)
        {
            cookie = new Cookie(_httpContextAccessor, _configuration);
            ProdutoCarrinho produtoCarrinho = new ProdutoCarrinho();
            produtoCarrinho.Quantidade = quantidade;
            produtoCarrinho.Id = id;

            cookie.Atualizar(produtoCarrinho, key);
            return Ok();
        }
    }
}
