﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Cadastro { get; set; }
        public bool Presenca { get; set; }
        public bool Financeiro { get; set; }
        public Usuario UsuID { get; set; }
    }
}