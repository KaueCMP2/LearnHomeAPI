using LearnHomeAPI.Domains;

namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class CriarCursoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;

        public DateTime CargaHoraria { get; set; }
        public virtual ICollection<Instrutor> Instrutor { get; set; } = new List<Instrutor>();
    }
}
