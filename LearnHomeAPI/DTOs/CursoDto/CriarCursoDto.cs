using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AdicionarCursoDto
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;

        public int CargaHoraria { get; set; }
        public int InstrutorId { get; set; } 
    }
}
