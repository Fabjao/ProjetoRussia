﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoRussia.Core.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool IsPesistent { get; set; }
    }
}
