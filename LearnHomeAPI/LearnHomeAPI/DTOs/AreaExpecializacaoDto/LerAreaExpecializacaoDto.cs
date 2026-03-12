using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class LerAreaExpecializacaoDto
    {
        public int Id { get; set; }

        public string? Area { get; set; }

        public virtual ICollection<Instrutor> Instrutor { get; set; } = new List<Instrutor>();
    }
}
