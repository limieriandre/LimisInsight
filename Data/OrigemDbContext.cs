using LimisInsight.Models;
using Microsoft.EntityFrameworkCore;

public class OrigemDbContext : DbContext
{
    public OrigemDbContext(DbContextOptions<OrigemDbContext> options) : base(options) { }

    // Adicione DbSet para suas tabelas ou views aqui conforme necessário.
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
