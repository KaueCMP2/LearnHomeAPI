using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface ICursoRepository
    {
        List<Curso> Listar();
        Curso ObterPorId(int id);
        List<Curso> ObterPorNome(string nome);
        bool CursoExiste(string nome);
        void Adicionar(Curso curso);
        void Atualizar(int cursoId,Curso curso);
        void Remover(int id);
    }
}
