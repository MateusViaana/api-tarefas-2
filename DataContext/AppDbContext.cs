using ApiServico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServico.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Chamado> Chamados { get; set; }
    }
}
