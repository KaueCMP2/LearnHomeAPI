using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class AtualizarCursoDto
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;

        public int CargaHoraria { get; set; }
        public int InstrutorId { get; set; }
    }
}
