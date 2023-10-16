using LimisInsight.Models;
using Microsoft.EntityFrameworkCore;

public class LocalDbContext : DbContext
{
    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

    // Adicione DbSet para suas tabelas aqui conforme necessário.
    public DbSet<OrganizationUser> OrganizationUsers { get; set; }
    public DbSet<TeamUser> TeamUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Se você já tem outras configurações, mantenha-as.

        modelBuilder.Entity<TeamUser>()
            .HasKey(t => new { t.UserId, t.TeamId });

        // outras configurações...
    }

}
