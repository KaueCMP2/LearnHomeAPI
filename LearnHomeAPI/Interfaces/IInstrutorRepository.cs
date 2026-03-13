using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Instrutor> Listar();
        Instrutor ObterPorId(int id);
        List<Instrutor> ObterPorNome(string nome);
        List<Instrutor> ObterPorEmail(string email);
        bool EmailExiste(string email);
        bool InstrutorExiste(string email);
        void Adicionar(Instrutor Instrutor);
        void Atualizar(int id, Instrutor Instrutor);
        void Remover(int id);
    }
}
