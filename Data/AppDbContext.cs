using Microsoft.EntityFrameworkCore;
using ApiEmpresarial.Models;

namespace ApiEmpresarial.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações opcionais: índices, relacionamentos explícitos, etc.
        modelBuilder.Entity<Empresa>()
            .HasIndex(e => e.Cnpj)
            .IsUnique();

        modelBuilder.Entity<Funcionario>()
            .HasIndex(f => f.Cpf)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}