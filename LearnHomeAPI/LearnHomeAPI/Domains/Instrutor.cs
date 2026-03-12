using System;
using System.Collections.Generic;

namespace LearnHomeAPI.Domains;

public partial class Instrutor
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[]? Senha { get; set; }

    public int? AreaExpecializacaoId { get; set; }

    public virtual AreaExpecializacao? AreaExpecializacao { get; set; }

    public virtual ICollection<Curso> Curso { get; set; } = new List<Curso>();
}
