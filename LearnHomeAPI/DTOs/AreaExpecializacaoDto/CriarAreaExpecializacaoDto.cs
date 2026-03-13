using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AdicionarAreaEspecializacaoDto
    {
        public int Id { get; set; }

        public string? Area { get; set; }

        public int instrutorId { get; set; }
    }
}
