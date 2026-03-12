using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface ICursoAlunoRepository
    {
        List<CursoAluno> Listar();
        CursoAluno ObterPorId(int? cursoId = null, int? alunoId = null);
        void Criar(int cursoId, int alunoId);
        void Atualizar(int cursoId, int alunoId);
        void Remover(int cursoId, int alunoId);
    }
}
