using System;
using System.Collections.Generic;

namespace LearnHomeAPI.Domains;

public partial class Aluno
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[]? Senha { get; set; }

    public virtual ICollection<CursoAluno> CursoAluno { get; set; } = new List<CursoAluno>();
}
