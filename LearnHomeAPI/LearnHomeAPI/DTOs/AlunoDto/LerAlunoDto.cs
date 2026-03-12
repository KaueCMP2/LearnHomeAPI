using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class LerAlunoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;
        public virtual ICollection<CursoAluno> CursoAluno { get; set; } = new List<CursoAluno>();

    }
}
