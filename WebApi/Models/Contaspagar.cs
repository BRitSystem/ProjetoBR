using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Contaspagar
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Dtpagamento { get; set; }
        public decimal Valorpagamento { get; set; }
        public bool Pago { get; set; }
        public List<Tipodepagamento> LstTipodepagamento { get; set; }
        public List<Produto> LstProduto { get; set; }
    }
}
