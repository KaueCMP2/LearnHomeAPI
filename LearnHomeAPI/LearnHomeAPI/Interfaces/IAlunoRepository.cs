using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface IAlunoRepository
    {
        List<Aluno> Listar();
        Aluno ObterPorId(int id);
        Aluno ObterPorNome(string nome);
        void Criar(Aluno aluno);
        void Atualizar(int id, Aluno aluno);
        void Remover(int id);
    }
}
