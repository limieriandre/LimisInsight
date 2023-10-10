using LimisInsight.Models;
using Microsoft.EntityFrameworkCore;

namespace LimisInsight.Data
{
    public class LimisInsightContext : DbContext
    {
        public LimisInsightContext(DbContextOptions<LimisInsightContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }

        // Se houver outras configurações de mapeamento, elas virão aqui
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações e relacionamentos adicionais
        }

    }
}
