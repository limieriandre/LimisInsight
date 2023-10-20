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
    public DbSet<TimeEntry> TimeEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(5, 7, 38)); // Substitua pelo número de versão correto do seu MySQL

        optionsBuilder.UseMySql("server=st.db.artia.com;port=3306;database=artia;user=cliente-53257;password=GuVSqVLVftozCU/9yO1zX6souYE=",
            serverVersion,
            options => options.CommandTimeout(300)); // Define o timeout para 120 segundos
    }




}
