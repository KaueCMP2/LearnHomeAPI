namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AdicionarAlunoDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[]? Senha { get; set; }
    }
}
