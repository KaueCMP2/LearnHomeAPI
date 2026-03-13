using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface ICursoRepository
    {
        List<Curso> Listar();
        Curso ObterPorId(int id);
        List<Curso> ObterPorNome(string nome);
        void Adicionar(Curso curso);
        void Atualizar(int cursoId, int InstrutorId,Curso curso);
        void Remover(int id);
    }
}
