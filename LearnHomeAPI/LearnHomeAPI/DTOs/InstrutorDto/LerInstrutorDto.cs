using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class LerInstrutorDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public virtual AreaExpecializacao? AreaExpecializacao { get; set; }
    }
}
