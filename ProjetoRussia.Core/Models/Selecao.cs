using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoRussia.Core.Models
{
   public class Selecao
    {
        public int SelecaoId { get; set; }
        public string Nome { get; set; }
        public List<Jogador> Jogadores { get; set; }
    }
}
