using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Qtproduto { get; set; }
        public decimal Valor { get; set; }
        public List<Fornecedores> LstFornecedores { get; set; }
    }
}
