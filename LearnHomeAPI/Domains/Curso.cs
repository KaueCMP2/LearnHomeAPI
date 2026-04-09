using System;
using System.Collections.Generic;

namespace LearnHomeAPI.Domains;

public partial class Curso
{
    public int Id { get; set; }
        
    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public int CargaHoraria { get; set; }

    public int InstrutorId { get; set; }

    public virtual ICollection<CursoAluno> CursoAluno { get; set; } = new List<CursoAluno>();

    public virtual Instrutor Instrutor { get; set; } = null!;

    public virtual ICollection<Instrutor> Intrutor { get; set; } = new List<Instrutor>();
}
