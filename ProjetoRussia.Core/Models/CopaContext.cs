using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoRussia.Core.Models
{
   public class CopaContext : DbContext
    {
        public CopaContext(DbContextOptions<CopaContext>options):
            base(options)
        {
        }

        public DbSet<Selecao> Selecoes { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
    }
}
