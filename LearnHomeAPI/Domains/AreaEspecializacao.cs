using System;
using System.Collections.Generic;

namespace LearnHomeAPI.Domains;

public partial class AreaEspecializacao
{
    public int Id { get; set; }

    public string? Area { get; set; }

    public virtual ICollection<Instrutor> Instrutor { get; set; } = new List<Instrutor>();
}
