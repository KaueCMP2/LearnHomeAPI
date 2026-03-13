using System;
using System.Collections.Generic;

namespace LearnHomeAPI.Domains;

public partial class Curso
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;

    public DateTime CargaHoraria { get; set; }
    public virtual ICollection<CursoAluno> CursoAluno { get; set; } = new List<CursoAluno>();
    public virtual ICollection<Instrutor> Instrutor { get; set; } = new List<Instrutor>();
}
