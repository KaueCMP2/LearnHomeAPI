using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AtualizarInstrutorDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string senha { get; set; } = null!;

        public virtual AreaEspecializacao? AreaEspecializacao { get; set; }
    }
}
