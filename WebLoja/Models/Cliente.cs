using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLoja.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime Nascimento { get; set; }

        public string Sexo { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string CEP { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Endereco { get; set; }

        public string Complemento { get; set; }

        public string Numero { get; set; }

        public string Senha { get; set; }

        public string ConfirmacaoSenha { get; set; }

        public string Situacao { get; set; }

        public virtual ICollection<EnderecoEntrega> EnderecosEntrega { get; set; }

        //public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
