using ProjetoRussia.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRussia.ViewModel
{
    public class ViewModelSelecao
    {
        
        public Selecao selecao { get; set; }
        public Jogador jogador { get; set; }
        public IEnumerable<Jogador> jogadores { get; set; }
        public ViewModelSelecao()
        {
            this.jogador = new Jogador();
        }
    }
}
