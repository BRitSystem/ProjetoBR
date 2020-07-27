using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Eventos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Dtevento { get; set; }
        public bool Restrito { get; set; }
        public bool Inscricao { get; set; }
        public bool Presenca { get; set; }
        public bool Aprovacaomanual { get; set; }
        public List<Participantes> LstParticipantes { get; set; }
        public List<Membros> LstAprovadores { get; set; }

    }
}
