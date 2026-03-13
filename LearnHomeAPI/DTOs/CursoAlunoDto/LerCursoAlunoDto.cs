namespace LearnHomeAPI.DTOs.AlunoDto
{
    public class LerCursoAlunoDto
    {
        public int CursoId { get; set; }

        public int AlunoId { get; set; }

        public string? NumeroMatricula { get; set; }

        public bool? StatusMatricula { get; set; }
    }
}
