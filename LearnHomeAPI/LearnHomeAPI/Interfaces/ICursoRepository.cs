using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface ICursoRepository
    {
        List<Curso> Listar();
        Curso ObterPorId(int id);
        Curso ObterPorNome(string nome);
        void Criar(Curso curso);
        void Atualizar(int id, Curso curso);
        void Remover(int id);
    }
}
