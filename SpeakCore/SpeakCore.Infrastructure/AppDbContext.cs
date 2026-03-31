using Microsoft.EntityFrameworkCore;
using SpeakCore.Domain.Entities;
using System.Reflection.Emit;

namespace SpeakCore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<AlunoTurma> AlunoTurmas { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoTurma>()
                .HasKey(at => new { at.AlunoId, at.TurmaId });

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Aluno)
                .WithMany(a => a.AlunoTurmas)
                .HasForeignKey(at => at.AlunoId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Turma)
                .WithMany(a => a.AlunoTurmas)
                .HasForeignKey(at => at.TurmaId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Disciplina)
                .WithMany() 
                .HasForeignKey(t => t.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}