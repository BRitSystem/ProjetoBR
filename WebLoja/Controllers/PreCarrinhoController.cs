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
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 1", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 5.00, ProdutoId = 1 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 2", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/vidro-retangular-250-ml-envel-senhora-aparecida-5d96544198cc2-3.jpeg", ProdutoValor = 27.92, ProdutoId = 2 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 3", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/vidro-retangular-250-ml-envel-boas-festas-5daa164f87500-1.jpeg", ProdutoValor = 33.23, ProdutoId = 3 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 4", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-dresseauth-100ml-5be026217f6da-2.jpeg", ProdutoValor = 4.25, ProdutoId = 4 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 5", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-lavanda-antiques-100ml-5be03c2391fd7-3.jpeg", ProdutoValor = 5.77, ProdutoId = 5 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 6", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-alecrim-eucalipto-100ml-5bc4dccf913e7-4.jpeg", ProdutoValor = 15.00, ProdutoId = 6 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 7", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/corante-oleo-azul-100ml-5cc1a994e9f5c-2.jpeg", ProdutoValor = 99.23, ProdutoId = 7 });
            lst_produto.Add(new Produto() { ProdutoNome = "Produto 8", ProdutoFoto = "https://casadasessencias.com.br/uploads/essencias/Estoque/essencia-vela-shopping-iguatemi-100ml-5cc20620a647e-2.jpeg", ProdutoValor = 100.25, ProdutoId = 8 });

            Produto = lst_produto.Find(item => item.ProdutoId == id);
            return View(Produto);
        }        
    }
}
