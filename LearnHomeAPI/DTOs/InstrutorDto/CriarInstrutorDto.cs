using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AdicionarInstrutorDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public int? AreaEspecializacaoId { get; set; }
    }
}
