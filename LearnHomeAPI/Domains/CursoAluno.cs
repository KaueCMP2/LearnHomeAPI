using System;
using System.Collections.Generic;

namespace LearnHomeAPI.Domains;

public partial class CursoAluno
{
    public int CursoId { get; set; }

    public int AlunoId { get; set; }

    public string? NumeroMatricula { get; set; }

    public bool? StatusMatricula { get; set; }

    public virtual Aluno Aluno { get; set; } = null!;

    public virtual Curso Curso { get; set; } = null!;
}
