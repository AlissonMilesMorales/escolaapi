using EscolaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Data;

public class EscolaContext : DbContext
{
    public EscolaContext(DbContextOptions<EscolaContext> opt) : base(opt) { }
    public DbSet<Models.Aluno> Alunos { get; set; }
    public DbSet<Models.Curso> Cursos { get; set; }
    public DbSet<Models.Nota> Notas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>()
            .HasMany(e => e.Alunos)
            .WithOne(e => e.Curso)
            .HasForeignKey(e => e.IdCurso)
            .IsRequired();
    }
}