using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AtualizarInstrutorDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string senha { get; set; } = null!;

        public int AreaEspecializacaoId { get; set; }
    }
}
