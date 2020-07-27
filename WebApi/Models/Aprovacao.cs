using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Aprovacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Dtevento { get; set; }
        public bool BoolAprovacao { get; set; }
    }
}
