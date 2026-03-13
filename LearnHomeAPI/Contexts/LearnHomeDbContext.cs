using System;
using System.Collections.Generic;
using LearnHomeAPI.Domains;
using Microsoft.EntityFrameworkCore;

namespace LearnHomeAPI.Contexts;

public partial class LearnHomeDbContext : DbContext
{
    public LearnHomeDbContext()
    {
    }

    public LearnHomeDbContext(DbContextOptions<LearnHomeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Aluno { get; set; }

    public virtual DbSet<AreaEspecializacao> AreaEspecializacao { get; set; }

    public virtual DbSet<Curso> Curso { get; set; }

    public virtual DbSet<CursoAluno> CursoAluno { get; set; }

    public virtual DbSet<Instrutor> Instrutor { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=LearnHomeDb; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aluno__3214EC07F39571BC");

            entity.HasIndex(e => e.Email, "UQ__Aluno__A9D10534CE263A3C").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
        });

        modelBuilder.Entity<AreaEspecializacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AreaEspe__3214EC075DEF8CB1");

            entity.Property(e => e.Area).HasMaxLength(40);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curso__3214EC077B6EAA7B");

            entity.HasIndex(e => e.Nome, "UQ__Curso__7D8FE3B26EA5203A").IsUnique();

            entity.Property(e => e.Nome).HasMaxLength(80);
        });

        modelBuilder.Entity<CursoAluno>(entity =>
        {
            entity.HasKey(e => new { e.CursoId, e.AlunoId }).HasName("PK_CursoAluno_CursoId_AlunoId");

            entity.Property(e => e.NumeroMatricula)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.StatusMatricula).HasDefaultValue(true);

            entity.HasOne(d => d.Aluno).WithMany(p => p.CursoAluno)
                .HasForeignKey(d => d.AlunoId)
                .HasConstraintName("FK_CursoAluno_AlunoId");

            entity.HasOne(d => d.Curso).WithMany(p => p.CursoAluno)
                .HasForeignKey(d => d.CursoId)
                .HasConstraintName("FK_CursoAluno_CursoId");
        });

        modelBuilder.Entity<Instrutor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruto__3214EC077CC47DFD");

            entity.HasIndex(e => e.Email, "UQ__Instruto__A9D10534EB9B00E4").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.AreaEspecializacao).WithMany(p => p.Instrutor)
                .HasForeignKey(d => d.AreaEspecializacaoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Instrtutor_AreaEspecializacao_AreaEspecializacaoId");

            entity.HasMany(d => d.Curso).WithMany(p => p.Instrutor)
                .UsingEntity<Dictionary<string, object>>(
                    "InstrutorCurso",
                    r => r.HasOne<Curso>().WithMany()
                        .HasForeignKey("CursoId")
                        .HasConstraintName("FK_InstrutorCurso_CursoId"),
                    l => l.HasOne<Instrutor>().WithMany()
                        .HasForeignKey("InstrutorId")
                        .HasConstraintName("FK_InstrutorCurso_InstrutorId"),
                    j =>
                    {
                        j.HasKey("InstrutorId", "CursoId").HasName("PK_InstrutorCurso_InstrutorId_CursoId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
