using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLoja.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public string ProdutoFoto { get; set; }
        public double ProdutoValor { get; set; }
    }
}
