using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Fornecedores
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Telefone Telefone { get; set; }
        public Contatofornecedor ContatoFornecedor { get; set; }

    }
}
