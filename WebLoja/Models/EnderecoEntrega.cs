using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLoja.Models
{
    public class EnderecoEntrega
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CEP { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Endereco { get; set; }

        public string Complemento { get; set; }

        public string Numero { get; set; }

        public int? ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
