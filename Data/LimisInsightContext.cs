using LimisInsight.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LimisInsight.Data
{
    public class LimisInsightContext : DbContext
    {
        private readonly int? _commandTimeout;

        public LimisInsightContext(DbContextOptions<LimisInsightContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TimeEntries> TimeEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações e relacionamentos adicionais
            modelBuilder.Entity<User>()
                .HasKey(u => new { u.UserId, u.TeamId });
        }
    }
}