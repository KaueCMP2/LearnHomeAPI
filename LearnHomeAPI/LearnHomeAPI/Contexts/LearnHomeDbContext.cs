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

    public virtual DbSet<AreaExpecializacao> AreaExpecializacao { get; set; }

    public virtual DbSet<Curso> Curso { get; set; }

    public virtual DbSet<CursoAluno> CursoAluno { get; set; }

    public virtual DbSet<Instrutor> Instrutor { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LearnHomeDb;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aluno__3214EC07CD7FBFEA");

            entity.HasIndex(e => e.Email, "UQ__Aluno__A9D10534FDAD5CDA").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
        });

        modelBuilder.Entity<AreaExpecializacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AreaExpe__3214EC07B10963E5");

            entity.Property(e => e.Area).HasMaxLength(40);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curso__3214EC070BE1F4D4");

            entity.HasIndex(e => e.Nome, "UQ__Curso__7D8FE3B296900DCB").IsUnique();

            entity.Property(e => e.CargaHoraria).HasColumnType("datetime");
            entity.Property(e => e.Nome)
                .HasMaxLength(80)
                .IsUnicode(false);
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
            entity.HasKey(e => e.Id).HasName("PK__Instruto__3214EC0762DCC8F1");

            entity.HasIndex(e => e.Email, "UQ__Instruto__A9D10534D9E42B32").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.AreaExpecializacao).WithMany(p => p.Instrutor)
                .HasForeignKey(d => d.AreaExpecializacaoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Instrtutor_AreaExpecializacao_AreaExpecializacaoId");

            entity.HasMany(d => d.Curso).WithMany(p => p.Intrutor)
                .UsingEntity<Dictionary<string, object>>(
                    "InstrutorCurso",
                    r => r.HasOne<Curso>().WithMany()
                        .HasForeignKey("CursoId")
                        .HasConstraintName("FK_IntrutorCurso_CursoId"),
                    l => l.HasOne<Instrutor>().WithMany()
                        .HasForeignKey("IntrutorId")
                        .HasConstraintName("FK_IntrutorCurso_InstrutorId"),
                    j =>
                    {
                        j.HasKey("IntrutorId", "CursoId").HasName("PK_IntrutorCurso_InstrutorId_CursoId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
