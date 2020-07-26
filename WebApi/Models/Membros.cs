using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Membros
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public DateTime Dtnascimento { get; set; }
        public string Foto { get; set; }
        public List<Cidade> LstCidade { get; set; }
        public List<Estado> LstEstado { get; set; }
        public List<Pais> LstPais { get; set; }
        public List<Categoria> LstCategoria { get; set; }
        public List<Condecoracoes> LstCondecoracoes { get; set; }
        public Telefone Telefone { get; set; }
        public List<Contasareceber> LstContasaReceber { get; set; }
    }
}
