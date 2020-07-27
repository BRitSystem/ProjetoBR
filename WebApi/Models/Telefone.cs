using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public int Ddd { get; set; }
        public string Numero { get; set; }
        public bool Whatsapp { get; set; }
        public List<Membros> LstMembros { get; set; }
        public List<Fornecedores> LstFornecedores { get; set; }
    }
}
