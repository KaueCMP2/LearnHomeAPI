using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface ICursoRepository
    {
        List<Curso> Listar();
        Curso ObterPorId(int id);
        Curso ObterPorNome(string nome);
        void Adicionar(Curso curso);
        void Atualizar(int cursoId, int instrutorId,Curso curso);
        void Remover(int id);
    }
}
