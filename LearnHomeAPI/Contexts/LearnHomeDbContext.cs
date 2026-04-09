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
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=LearnHomeDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aluno__3214EC07A826E2FD");

            entity.HasIndex(e => e.Email, "UQ__Aluno__A9D1053450E9141B").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__AreaEspe__3214EC07E0FE778A");

            entity.Property(e => e.Area).HasMaxLength(40);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curso__3214EC0760D0FDCE");

            entity.HasIndex(e => e.Nome, "UQ__Curso__7D8FE3B2A74D7251").IsUnique();

            entity.Property(e => e.Nome).HasMaxLength(80);

            entity.HasOne(d => d.Instrutor).WithMany(p => p.Curso)
                .HasForeignKey(d => d.InstrutorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CursoAluno>(entity =>
        {
            entity.HasKey(e => new { e.CursoId, e.AlunoId }).HasName("PK_CursoAluno_CursoId_AlunoId");

            entity.Property(e => e.NumeroMatricula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('CRAL'+right('0000'+CONVERT([varchar](4),NEXT VALUE FOR [SeqMatricula]),(4)))");
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
            entity.HasKey(e => e.Id).HasName("PK__Instruto__3214EC07ACB42FAF");

            entity.HasIndex(e => e.Email, "UQ__Instruto__A9D1053427C9473E").IsUnique();

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

            entity.HasMany(d => d.CursoNavigation).WithMany(p => p.Intrutor)
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
        modelBuilder.HasSequence("SeqMatricula");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
