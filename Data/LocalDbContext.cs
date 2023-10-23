using LimisInsight.Models;
using Microsoft.EntityFrameworkCore;

public class LocalDbContext : DbContext
{
    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

    // Adicione DbSet para suas tabelas aqui conforme necessário.
    public DbSet<OrganizationUser> OrganizationUsers { get; set; }
    public DbSet<TeamUser> TeamUsers { get; set; }

    public DbSet<ListaApontamento> ListaApontamentos { get; set; }

    public DbSet<ParametrosConfig> ParametrosConfigs { get; set; }

    public DbSet<DataJustificada> DatasJustificadas { get; set; }

    public DbSet<DataFeriado> DatasFeriados { get; set; }   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Se você já tem outras configurações, mantenha-as.

        modelBuilder.Entity<ListaApontamento>()
                .HasKey(la => new { la.Id });

        modelBuilder.Entity<ListaApontamento>()
        .Property(la => la.Data)
        .HasColumnType("date");

        modelBuilder.Entity<TimeEntry>()
        .Property(te => te.DateAt)
        .HasColumnType("date");

        modelBuilder.Entity<TeamUser>()
            .HasKey(t => new { t.UserId, t.TeamId });

        // outras configurações...
    }
    public DbSet<TimeEntry> TimeEntries { get; set; }


}
