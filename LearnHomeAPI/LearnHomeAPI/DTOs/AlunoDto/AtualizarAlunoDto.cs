namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AtualizarAlunoDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[]? Senha { get; set; }
    }
}
