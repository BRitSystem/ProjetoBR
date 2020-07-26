using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Contasareceber
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Dtlancamento { get; set; }
        public DateTime Dtpagamento { get; set; }
        public decimal Valorlancamento { get; set; }
        public bool Pago { get; set; }
        public Membros Membros { get; set; }
        public List<Tipodelancamento> LstTipodelancamento { get; set; }
    }
}
