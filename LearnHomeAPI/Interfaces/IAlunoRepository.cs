using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface IAlunoRepository
    {
        List<Aluno> Listar();
        Aluno ObterPorId(int id);
        List<Aluno> ObterPorNome(string nome);
        List<Aluno> ObterPorEmail (string email);
        bool AlunoExiste (string email);
        void Adicionar(Aluno aluno);
        void Atualizar(int id, Aluno aluno);
        void Remover(int id);
    }
}
